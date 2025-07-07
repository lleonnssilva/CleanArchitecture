using AutoMapper;
using CleanArchitecture.Application.DTOS.Cliente;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Logging;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Validations;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CleanArchitecture.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;
        private readonly IEventPublisher _events;
        private readonly IDistributedCache _redis;
        private readonly ILogService _logService;

        public ClienteService(IClienteRepository repository, IMapper mapper, IEventPublisher events, IDistributedCache redis, ILogService logService)
        {
            _repository = repository;
            _mapper = mapper;
            _events = events;
            _redis = redis;
            _logService = logService;
        }

        public async Task<IEnumerable<ClienteDTO>> GetAllsync()
        {

            var clientes = await _repository.GetAllsync();
            _logService.LogInfo($"Listando todos clientes");

            return _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
        }

        public async Task<ClienteDTO> GetByIdAsync(Guid id)
        {
            var clientCache = await ClientByIdCache(id);
            if (clientCache != null)
                return clientCache;

            return await ClientByIdDB(id);

        }

        public async Task AddAsync(ClienteDTO cliente)
        {
            try
            {
                var clienteMap = _mapper.Map<Cliente>(cliente);
                await _repository.AddAsync(clienteMap);
                _logService.LogInfo($"Cliente com ID - {cliente.Id} adicionado em DB");

                PublicarEvento(clienteMap);

                await AddCache(cliente, clienteMap);

            }
            catch (DomainValidation ex)
            {
                _logService.LogError($"Erro ao adicionar cliente: {ex.Message}", null);
                throw new DomainException($"Erro ao adicionar cliente: {ex.Message}", 400);
            }
            catch (Exception ex)
            {
                _logService.LogError($"Ocorreu um erro inesperado.: {ex.Message}", null);
                throw new Exception($"Ocorreu um erro inesperado.: {ex.Message}", ex);
            }

        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            _logService.LogInfo($"Cliente com ID - {id} removido em DB");

            var clienteCache = await _redis.GetStringAsync($"{id}");
            _logService.LogInfo($"Buscando o cliente com ID - {id} em Cache");

            if (clienteCache is not null)
            {
                await _redis.RemoveAsync($"{id}");
                _logService.LogInfo($"Removendo o cliente com ID - {id} do Cache");
            }

        }

        public async Task UpdateAsync(ClienteDTO cliente)
        {
            var clienteMap = _mapper.Map<Cliente>(cliente);
            await _repository.UpdateAsync(clienteMap);
            _logService.LogInfo($"Cliente com ID - {clienteMap.Id} atualizado em DB");

            var clienteCache = await _redis.GetStringAsync($"{clienteMap.Id}");
            _logService.LogInfo($"Buscando o cliente com ID - {clienteMap.Id} em Cache para atualizar");

            if (clienteCache is not null)
            {
                await _redis.RemoveAsync($"{clienteMap.Id}");
                _logService.LogInfo($"Cliente com ID - {clienteMap.Id} atualizao em Cache");
            }

        }

        private async Task AddCache(ClienteDTO cliente, Cliente clienteMap)
        {
            await _redis.SetStringAsync($"{clienteMap.Id}", JsonSerializer.Serialize(clienteMap));
            _logService.LogInfo($"Cliente com ID - {cliente.Id} dicionando em Cache");
        }
        private void PublicarEvento(Cliente clienteMap)
        {
            var clienteCadastradoEvent = new ClienteCadastradoEvent(clienteMap);
            _events.Publish(clienteCadastradoEvent);
            _logService.LogInfo($"Evento de clienteCadastradoEvent publicado");
        }
        private async Task<ClienteDTO> ClientByIdDB(Guid id)
        {
            var clienteFromDb = await _repository.GetByIdAsync(id);


            if (clienteFromDb != null)
            {
                _logService.LogInfo($"Buscando o cliente com ID - {id} em DB");
                var clienteJson = JsonSerializer.Serialize(clienteFromDb);

                await _redis.SetStringAsync($"{id}", clienteJson,
                    options: new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
                    });

                _logService.LogInfo($"Setando o cliente com ID - {id} em cache");
            }

            return _mapper.Map<ClienteDTO>(clienteFromDb);
        }
        private async Task<ClienteDTO> ClientByIdCache(Guid id)
        {
            var cliente = await _redis.GetStringAsync($"{id}");


            if (cliente != null)
            {
                _logService.LogInfo($"Buscando o cliente com ID - {id} em cache");
                var clienteCache = JsonSerializer.Deserialize<ClienteDTO>(cliente);
                return _mapper.Map<ClienteDTO>(clienteCache);
            }

            return _mapper.Map<ClienteDTO>(cliente);
        }
      
    }
}

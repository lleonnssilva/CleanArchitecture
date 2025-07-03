using AutoMapper;
using CleanArchitecture.Application.DTOS;
using CleanArchitecture.Application.Interfaces;
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
        private readonly IEventPublisher _eventPublisher;
        private readonly IDistributedCache _redisCache;
      
        public ClienteService(IClienteRepository repository, IMapper mapper, IEventPublisher eventPublisher, IDistributedCache redisCache)
        {
            _repository = repository;
            _mapper = mapper;
            _eventPublisher = eventPublisher;
            _redisCache = redisCache;

        }

        public async Task<IEnumerable<ClienteDTO>> GetAllsync()
        {

            var clientes = await _repository.GetAllsync();
            return _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
        }

        public async Task<ClienteDTO> GetByIdAsync(Guid id)
        {
            var cliente = await _redisCache.GetStringAsync($"{id}");


            if (string.IsNullOrEmpty(cliente))
            {
                var memberFromDb = await _repository.GetByIdAsync(id);

                if (memberFromDb != null)
                {
                    var memberJson = JsonSerializer.Serialize(memberFromDb);
                    await _redisCache.SetStringAsync($"{id}", memberJson,
                        options: new DistributedCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
                        });
                }
                return _mapper.Map<ClienteDTO>(memberFromDb);
                 
            }


            var clienteCache = JsonSerializer.Deserialize<ClienteDTO>(cliente);

            if (clienteCache == null)
            {
                var memberFromDb = await _repository.GetByIdAsync(id);
                return _mapper.Map<ClienteDTO>(memberFromDb);
            }

            return _mapper.Map<ClienteDTO>(clienteCache);
        }
        public async Task AddAsync(ClienteDTO cliente)
        {
            try
            {
                var clienteMap = _mapper.Map<Cliente>(cliente);
                await _repository.AddAsync(clienteMap);

                var evento = new ClienteCadastradoEvent(clienteMap);
                _eventPublisher.Publish(evento);
                await _redisCache.SetStringAsync($"{clienteMap.Id}", JsonSerializer.Serialize(clienteMap));
            }
            catch (DomainValidation ex)
            {
                throw new DomainException($"Erro ao adicionar cliente: {ex.Message}",400);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro inesperado.: {ex.Message}", ex);
            }

        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            var clienteCache = await _redisCache.GetStringAsync($"{id}");
            if (clienteCache is not null)
                await _redisCache.RemoveAsync($"{id}");
        }

        public async Task UpdateAsync(ClienteDTO cliente)
        {
            var clienteMap = _mapper.Map<Cliente>(cliente);
            await _repository.UpdateAsync(clienteMap);

            var clienteCache = await _redisCache.GetStringAsync($"{clienteMap.Id}");
            if (clienteCache is not null)
                await _redisCache.RemoveAsync($"{clienteMap.Id}");
        }
    }
}

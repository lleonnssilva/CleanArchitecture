using AutoMapper;
using CleanArchitecture.Application.DTOS;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events;
using CleanArchitecture.Domain.Interfaces.Repositories;

namespace CleanArchitecture.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;
        private readonly IEventPublisher _eventPublisher;

        public ClienteService(IClienteRepository repository, IMapper mapper, IEventPublisher eventPublisher)
        {
            _repository = repository;
            _mapper = mapper;
            _eventPublisher = eventPublisher;
        }

        public async Task<IEnumerable<ClienteDTO>> GetAllsync()
        {
            var clientes = await _repository.GetAllsync();
            return _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
        }

        public async Task<ClienteDTO> GetByIdAsync(Guid id)
        {
            var cliente = await _repository.GetByIdAsync(id);
            return _mapper.Map<ClienteDTO>(cliente); ;
        }
        public async Task AddAsync(ClienteDTO cliente)
        {
            var clienteMap = _mapper.Map<Cliente>(cliente);
            await _repository.AddAsync(clienteMap);

            var evento = new ClienteCadastradoEvent(clienteMap);
            _eventPublisher.Publicar(evento);

        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task UpdateAsync(ClienteDTO cliente)
        {
            var clienteMap = _mapper.Map<Cliente>(cliente);
            await _repository.UpdateAsync(clienteMap);
        }
    }
}

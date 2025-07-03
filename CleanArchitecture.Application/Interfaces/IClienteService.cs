using CleanArchitecture.Application.DTOS.Cliente;

namespace CleanArchitecture.Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDTO>> GetAllsync();
        Task<ClienteDTO> GetByIdAsync(Guid id);
        Task AddAsync(ClienteDTO cliente);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(ClienteDTO cliente);
    }
}

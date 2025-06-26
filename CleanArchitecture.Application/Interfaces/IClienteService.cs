using CleanArchitecture.Application.DTOS;

namespace CleanArchitecture.Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDTO>> GetAllsync();
        Task<ClienteDTO> GetByIdAsync(Guid id);
        Task AddAsync(ClienteDTO product);
        Task DeleteAsync(Guid id);
    }
}

using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Interfaces.Repositories
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetAllsync();
        Task<Cliente> GetByIdAsync(Guid id);
        Task AddAsync(Cliente accountPlain);
        Task DeleteAsync(Guid id);
    }
}

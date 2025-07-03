using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        protected readonly AppDbContext _dbContext;

        public ClienteRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Cliente cliente)
        {
            await _dbContext.AddAsync(cliente);
            _dbContext.SaveChanges();
        }

        public async Task DeleteAsync(Guid id)
        {
            var cliente = await GetByIdAsync(id);
            if (cliente != null)
            {
                _dbContext.Clientes.Remove(cliente);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Cliente>> GetAllsync()
        {
            return await _dbContext.Clientes.ToListAsync();
        }

        public async Task<Cliente> GetByIdAsync(Guid id)
        {
            return await _dbContext.Clientes.FindAsync(id);
        }

        public async Task UpdateAsync(Cliente cliente)
        {
             _dbContext.Clientes.Update(cliente);
            _dbContext.SaveChanges();

        }
    }
}

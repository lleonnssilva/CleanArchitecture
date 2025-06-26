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
            var member = await GetByIdAsync(id);

            _dbContext.Remove(member);
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<Cliente>> GetAllsync()
        {
            return await _dbContext.Clientes.ToListAsync();
        }

        public async Task<Cliente> GetByIdAsync(Guid id)
        {
            var cliente = await _dbContext.Clientes.FindAsync(id);

            return cliente;
        }
    }
}

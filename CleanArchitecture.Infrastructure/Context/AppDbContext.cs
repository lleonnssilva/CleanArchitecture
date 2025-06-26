using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());

            //modelBuilder.Entity<Cliente>().HasKey(p => p.Id);
            //modelBuilder.Entity<Cliente>().Property(p => p.Nome).HasMaxLength(128).HasColumnName("Nome");
            //modelBuilder.Entity<Cliente>().ComplexProperty(p => p.Endereco).Property(p => p.Rua).HasMaxLength(128).HasColumnName("Rua");
            //modelBuilder.Entity<Cliente>().ComplexProperty(p => p.Endereco).Property(p => p.Numero).HasMaxLength(5).HasColumnName("Numero");
            //modelBuilder.Entity<Cliente>().ComplexProperty(p => p.Endereco).Property(p=> p.Bairro).HasMaxLength(100).HasColumnName("Bairro");
            //modelBuilder.Entity<Cliente>().ComplexProperty(p => p.Endereco).Property(p => p.Cidade).HasMaxLength(100).HasColumnName("Cidade");
            //modelBuilder.Entity<Cliente>().ComplexProperty(p => p.Endereco).Property(p => p.Estado).HasMaxLength(100).HasColumnName("Estado");
            
           
            //modelBuilder.Entity<Cliente>().ComplexProperty(p => p.Endereco)
            //  .IsRequired();

          

        }

    }
}

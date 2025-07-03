using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.EntityConfiguration
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {



            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome)
                   .HasMaxLength(200)
                   .HasColumnName("Nome").IsRequired();

            builder.OwnsOne(p => p.Endereco, endereco =>
            {
                endereco.Property(p => p.Rua)
                        .HasMaxLength(200)
                        .HasColumnName("Rua").IsRequired();

                endereco.Property(p => p.Numero)
                        .HasMaxLength(5)
                        .HasColumnName("Numero").IsRequired();

                endereco.Property(p => p.Bairro)
                        .HasMaxLength(100)
                        .HasColumnName("Bairro").IsRequired();

                endereco.Property(p => p.Cidade)
                        .HasMaxLength(100)
                        .HasColumnName("Cidade").IsRequired();

                endereco.Property(p => p.Estado)
                        .HasMaxLength(100)
                        .HasColumnName("Estado").IsRequired();

                endereco.Property(p => p.Cep)
                       .HasMaxLength(9)
                       .HasColumnName("Cep").IsRequired();
            });

            builder.OwnsOne(c => c.Telefone, telefone =>
                {
                    telefone.Property(t => t.DDD)
                            .HasMaxLength(2)
                            .HasColumnName("DDD")
                            .IsRequired(); 

                    telefone.Property(t => t.NumeroTelefone)
                            .HasMaxLength(9)
                            .HasColumnName("NumeroTelefone")
                            .IsRequired();
                });

            builder.OwnsOne(c => c.Email, email =>
                {
                    email.Property(e => e.EnderecoEmail)
                         .HasMaxLength(200)
                         .HasColumnName("EnderecoEmail")
                         .IsRequired(); 
                });

        }
    }
}

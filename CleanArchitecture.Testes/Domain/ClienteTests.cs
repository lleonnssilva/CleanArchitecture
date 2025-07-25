using System;
using Xunit;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;
using CleanArchitecture.Domain.Exceptions;

namespace CleanArchitecture.Testes.Domain
{
    public class ClienteTests
    {
        [Fact]
        public void Cliente_DeveLancarExcecao_QuandoNomeInvalido()
        {
            // Arrange
            var telefone = new Telefone(11, 123456789);
            var email = new Email("cliente@dominio.com");
            var endereco = new Endereco("Rua X", "Bairro Y", "100", "Cidade Z", "Estado W", "12345-678");

            // Act & Assert
            Assert.Throws<DomainValidationException>(() => new Cliente("", telefone, email, endereco));
            Assert.Throws<DomainValidationException>(() => new Cliente("Jo", telefone, email, endereco));
            Assert.Throws<DomainValidationException>(() => new Cliente(new string('A', 201), telefone, email, endereco));
        }

        [Fact]
        public void Cliente_DeveCriarCorretamente_QuandoDadosValidos()
        {
            // Arrange
            var telefone = new Telefone(11, 987654321);
            var email = new Email("cliente@dominio.com");
            var endereco = new Endereco("Rua X", "Bairro Y", "100", "Cidade Z", "Estado W", "12345-678");

            // Act
            var cliente = new Cliente("João da Silva", telefone, email, endereco);

            // Assert
            Assert.NotNull(cliente);
            Assert.Equal("João da Silva", cliente.Nome);
            Assert.Equal("cliente@dominio.com", cliente.GetEmail());
            Assert.Equal("11987654321", cliente.GetTelefone());
        }

        [Fact]
        public void Cliente_DeveRetornarTelefoneCorretamente()
        {
            // Arrange
            var telefone = new Telefone(11, 987654321);
            var email = new Email("cliente@dominio.com");
            var endereco = new Endereco("Rua X", "Bairro Y", "100", "Cidade Z", "Estado W", "12345-678");
            var cliente = new Cliente("João da Silva", telefone, email, endereco);

            // Act
            var telefoneFormatado = cliente.GetTelefone();

            // Assert
            Assert.Equal("11987654321", telefoneFormatado);
        }

        [Fact]
        public void Cliente_DeveRetornarEmailCorretamente()
        {
            // Arrange
            var telefone = new Telefone(11, 987654321);
            var email = new Email("cliente@dominio.com");
            var endereco = new Endereco("Rua X", "Bairro Y", "100", "Cidade Z", "Estado W", "12345-678");
            var cliente = new Cliente("João da Silva", telefone, email, endereco);

            // Act
            var emailFormatado = cliente.GetEmail();

            // Assert
            Assert.Equal("cliente@dominio.com", emailFormatado);
        }
    }
}

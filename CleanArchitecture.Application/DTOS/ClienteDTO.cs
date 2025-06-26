using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.DTOS
{
    public class ClienteDTO
    {
        public string Nome { get; set; }
        public EnderecoDTO Endereco { get; set; }
        public Telefone Telefone { get; set; }
        public Email EnderecoEmail { get; set; }
    }
}

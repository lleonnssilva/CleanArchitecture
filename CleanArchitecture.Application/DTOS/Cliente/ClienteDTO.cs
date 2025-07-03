using CleanArchitecture.Application.DTOS.Email;
using CleanArchitecture.Application.DTOS.Endereco;
using CleanArchitecture.Application.DTOS.Telefone;

namespace CleanArchitecture.Application.DTOS.Cliente
{
    public class ClienteDTO
    {
        public Guid Id { get;  set; }
        public string Nome { get; set; }
        public EnderecoDTO Endereco { get; set; }
        public TelefoneDTO Telefone { get; set; }
        public EmailDTO Email { get; set; }
  
    }
}

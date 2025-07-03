namespace CleanArchitecture.Application.DTOS
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

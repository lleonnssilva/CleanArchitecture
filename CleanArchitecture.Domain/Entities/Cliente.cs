using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Domain.Entities
{

    
    public class Cliente : BaseEntity
    {

        public string Nome { get; private set; }
        public Telefone Telefone { get; private set; }
        public Email Email { get; private set; }
        public Endereco Endereco { get; private set; }

        protected Cliente() { }
        public Cliente(string nome, Telefone telefone, Email email, Endereco endereco)
        {
            this.Validate(nome);
            DataAtualizacao = DateTime.Now;
            DataCriacao = DateTime.Now;
            this.Nome = nome;
            this.Telefone = telefone;
            this.Email = email;
            this.Endereco = endereco;
        }
        private void Validate(string nome)
        {
            if(string.IsNullOrEmpty(nome))
                throw new DomainValidationException("Nome inválido. Nome é obrigatório.");
            if (nome.Length < 5)
                throw new DomainValidationException("Nome inválido. Nome precisa ter pelo menos 5 caracteres.");

            if(nome.Length > 200)
               throw new DomainValidationException("Nome inválido. Nome pode ter no máximo 200 caracteres.");

        }


        public string GetEmail()
        {
            return this.Email.EnderecoEmail.ToString();
        }
        public string GetTelefone()
        {
            return this.Telefone.DDD.ToString() + this.Telefone.NumeroTelefone.ToString();
        }
    }
}

using CleanArchitecture.Domain.Validations;
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
            DomainValidation.When(string.IsNullOrEmpty(nome),
                "Nome inválido. Nome é obrigatório.");

            DomainValidation.When(nome.Length < 5,
                "Nome inválido. Nome precisa ter pelo menos 5 caracteres.");

            DomainValidation.When(nome.Length > 200,
               "Nome inválido. Nome pode ter no máximo 200 caracteres.");

        }


        public string GetEmail()
        {
            return this.Email.EnderecoEmail.ToString();
        }
        public string GetTelefone()
        {
            return this.Telefone.DDD.ToString() + this.Telefone.NumeroTelefone.ToString();
        }
        public string GetEnderecoFormatado()
        {
            var endereco = String.Format("Logradouro:{0}\nBairro: {1}\nNúmero: {2}\nCidade: {3}\nMunicipio: {4}\nEstado: {5}",
                this.Endereco.Rua.ToString(),
                this.Endereco.Bairro.ToString(),
                this.Endereco.Numero.ToString(),
                this.Endereco.Cidade.ToString(),
                this.Endereco.Estado.ToString(),
                this.Endereco.Cep.ToString());
            return endereco;
        }

    }
}

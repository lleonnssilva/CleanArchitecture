using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Domain.Entities
{
    public class Cliente : BaseEntity
    {

        public string Nome { get; private set; }
        public Telefone Telefone { get; private set; }
        public Email EnderecoEmail { get; private  set; }
        public  Endereco Endereco { get; private set; }

        protected Cliente() { }
        public Cliente(string nome,Telefone telefone, Email enderecoEmail, Endereco endereco)
        {
            this.Validate(nome);
            this.Nome = nome;
            this.Telefone = telefone;
            this.EnderecoEmail = enderecoEmail;
            this.Endereco = endereco;
        }
        private void Validate(string nome)
        {
            if (String.IsNullOrEmpty(nome))
                throw new Exception("Nome invalido");
        }
       
        //public string GetNome()
        //{
        //    return this._nome;
        //}
        //public string GetEmail()
        //{
        //    return this._contato.GetEmail();
        //}
        //public string GetTelefone()
        //{
        //    return this._contato.GetTelefone();
        //}
        //public string GetEnderecoFormatado()
        //{
        //    var endereco = String.Format("Logradouro:{0}\nBairro: {1}\nNúmero: {2}\nCidade: {3}\nMunicipio: {4}\nEstado: {5}",
        //        this._endereco.GetRua(),
        //        this._endereco.GetBairro(),
        //        this._endereco.GetNumero(),
        //        this._endereco.GetCidade(),
        //        this._endereco.GetMunicipio(),
        //        this._endereco.GetEstado());
        //    return endereco;
        //}

    }
}

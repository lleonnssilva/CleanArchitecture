namespace CleanArchitecture.Domain.ValueObjects
{
    public class Endereco
    {
        public string Rua { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Numero { get; private set; }
        public string Estado { get; private set; }
        public string Cep { get; private set; }
        protected Endereco() { }
        public Endereco(string rua, string bairro, string cidade, string numero, string estado, string cep)
        {
            Validate(rua,bairro,cidade,numero,estado,cep);
            Rua = rua;
            Bairro = bairro;
            Cidade = cidade;
            Numero = numero;
            Estado = estado;
            Cep = cep;

        }
        private void Validate(string rua, string bairro, string cidade, string numero, string estado, string cep)
        {
            if (String.IsNullOrEmpty(rua))
                throw new Exception("Rua invalido");

            if (String.IsNullOrEmpty(bairro))
                throw new Exception("Bairro invalido");

            if (String.IsNullOrEmpty(cidade))
                throw new Exception("Cidade invalido");

            if (String.IsNullOrEmpty(numero))
                throw new Exception("Número invalido");

            if (String.IsNullOrEmpty(estado))
                throw new Exception("Estado invalido");

            if (String.IsNullOrEmpty(cep))
                throw new Exception("Cep invalido");
        }
    }
}

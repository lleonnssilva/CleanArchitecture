using CleanArchitecture.Domain.Exceptions;
using System.Text.RegularExpressions;

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
            Validate(rua, bairro, cidade, numero, estado, cep);
            Rua = rua;
            Bairro = bairro;
            Cidade = cidade;
            Numero = numero;
            Estado = estado;
            Cep = cep;

        }
        private void Validate(string rua, string bairro, string cidade, string numero, string estado, string cep)
        {
            if (string.IsNullOrEmpty(rua))
                throw new DomainValidationException("Rua invalida. Rua é obrigatório.");

            if (string.IsNullOrEmpty(bairro))
                throw new DomainValidationException("Bairro invalido. Bairro é obrigatório.");

            if (string.IsNullOrEmpty(cidade))
                throw new DomainValidationException("Cidade invalida. Cidade é obrigatório.");

            if (string.IsNullOrEmpty(numero))
                throw new DomainValidationException("Número invalido. Número é obrigatório.");

            if (string.IsNullOrEmpty(estado))
                throw new DomainValidationException("Estado invalido. Estado é obrigatório.");

            string pattern = @"^\d{5}-\d{3}$";
            Regex regex = new Regex(pattern);
            var cepValido = regex.IsMatch(cep);

            if (string.IsNullOrEmpty(cep) || cep.ToString().Length != 9 || !cepValido)  // Exemplo: "12345-678"
                throw new DomainValidationException("Cep inválido. O CEP precisa ter o formato correto.");

        }
    }
}

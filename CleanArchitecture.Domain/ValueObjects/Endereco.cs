using CleanArchitecture.Domain.Validations;
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
            DomainValidation.When(string.IsNullOrEmpty(rua),
               "Rua invalida. Rua é obrigatório.");

            DomainValidation.When(string.IsNullOrEmpty(bairro),
              "Bairro invalido. Bairro é obrigatório.");

            DomainValidation.When(string.IsNullOrEmpty(cidade),
              "Cidade invalida. Cidade é obrigatório.");

            DomainValidation.When(string.IsNullOrEmpty(numero),
              "Número invalido. Número é obrigatório.");

            DomainValidation.When(string.IsNullOrEmpty(estado),
              "Estado invalido. Estado é obrigatório.");

            DomainValidation.When(string.IsNullOrEmpty(cep),
              "Cep invalido. Cep é obrigatório.");

            string pattern = @"^\d{5}-\d{3}$";
            Regex regex = new Regex(pattern);
            var cepValido= regex.IsMatch(cep);

            DomainValidation.When(!cepValido,
              "Cep invalido. Cep é inválido.");

        }
    }
}

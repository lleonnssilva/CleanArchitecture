using CleanArchitecture.Domain.Validations;
using System.Text.RegularExpressions;

namespace CleanArchitecture.Domain.ValueObjects
{
    public class Email
    {
        public string EnderecoEmail { get; private set; }
        protected Email() { }
        public Email(string endereco)
        {

            Validate(endereco);
            EnderecoEmail = endereco;
        }

        private void Validate(string endereco)
        {
            DomainValidation.When(string.IsNullOrEmpty(endereco),
               "Email inválido. Email é obrigatório.");


            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);

            var  emailValid = regex.IsMatch(endereco);
            DomainValidation.When(!emailValid,
                "Nome inválido. Informe um email válido.");


        }
    }

}


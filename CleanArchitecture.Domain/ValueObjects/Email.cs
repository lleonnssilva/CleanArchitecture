using CleanArchitecture.Domain.Exceptions;
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
           if(string.IsNullOrEmpty(endereco))
               throw new DomainValidationException("Email inválido. Email é obrigatório.");


            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);

            var  emailValid = regex.IsMatch(endereco);
           if(!emailValid)
                throw new DomainValidationException("Nome inválido. Informe um email válido.");


        }
    }

}


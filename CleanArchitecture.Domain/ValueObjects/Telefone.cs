using CleanArchitecture.Domain.Validations;

namespace CleanArchitecture.Domain.ValueObjects
{
    public class Telefone
    {
        public string DDD { get; private set; }
        public string NumeroTelefone { get; private set; }
        protected Telefone() { }
        public Telefone(string ddd, string telefone)
        {

            Validate(ddd, telefone);
            DDD = ddd;
            NumeroTelefone = telefone;
        }

        private void Validate(string ddd,string telefone)
        {

            DomainValidation.When(string.IsNullOrEmpty(ddd),
               "DDD invalido. DDD é obrigatório.");

            DomainValidation.When(string.IsNullOrEmpty(ddd),
               "Telefone invalido. Telefone é obrigatório.");
        }
    }
}

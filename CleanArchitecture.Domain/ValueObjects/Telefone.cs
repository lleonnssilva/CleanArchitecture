using CleanArchitecture.Domain.Exceptions;

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

            if(string.IsNullOrEmpty(ddd))
                throw new DomainValidationException("DDD invalido. DDD é obrigatório.");

            if(string.IsNullOrEmpty(ddd))
               throw new DomainValidationException("Telefone invalido. Telefone é obrigatório.");
        }
    }
}

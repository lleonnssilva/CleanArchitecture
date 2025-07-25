using CleanArchitecture.Domain.Exceptions;

namespace CleanArchitecture.Domain.ValueObjects
{
    public class Telefone
    {
        public int DDD { get; private set; }
        public int NumeroTelefone { get; private set; }
        protected Telefone() { }
        public Telefone(int ddd, int telefone)
        {

            Validate(ddd, telefone);
            DDD = ddd;
            NumeroTelefone = telefone;
        }

        private void Validate(int ddd, int telefone)
        {

            if(ddd == null || ddd <= 0)
                throw new DomainValidationException("DDD invalido. DDD é obrigatório.");

            if (telefone == null || telefone <= 0)
                throw new DomainValidationException("Telefone invalido. Telefone é obrigatório.");
        }
    }
}

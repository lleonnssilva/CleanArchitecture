using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CleanArchitecture.Domain.ValueObjects
{
    public class Telefone
    {
        public string DDD { get; private set; }
        public string NumeroTelefone { get; private set; }
        protected Telefone() { }
        public Telefone(string ddd, string numeroTelefone)
        {

            Validate(ddd, numeroTelefone);
            DDD = ddd;
            NumeroTelefone = numeroTelefone;
        }

        private void Validate(string ddd,string telefone)
        {
            if (string.IsNullOrEmpty(ddd))
                throw new Exception("DDD invalido");

            if (string.IsNullOrEmpty(telefone))
                throw new Exception("Telefone invalido");
        }
        public override string ToString()
        {
            return NumeroTelefone;
        }
    }
}

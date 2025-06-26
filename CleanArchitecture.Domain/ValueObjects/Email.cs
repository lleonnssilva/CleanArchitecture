namespace CleanArchitecture.Domain.ValueObjects
{
    public class Email
    {
        public string Endereco { get; private set; }
        protected Email() { }
        public Email(string endereco)
        {

            Validate(endereco);
            Endereco = endereco;
        }

        private void Validate(string endereco)
        {
            if (String.IsNullOrEmpty(endereco))
                throw new Exception("Email invalido");
        }
        public override string ToString()
        {
            return Endereco;
        }
    }

}


namespace CleanArchitecture.Domain.Exceptions
{
    public class DomainValidationException : Exception
    {
        public int StatusCode { get; }

        // Construtor que recebe a mensagem de erro
        public DomainValidationException(string message) : base(message)
        {
            StatusCode = 400; // BadRequest por padrão
        }

        // Você pode também customizar o StatusCode se necessário
        public DomainValidationException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}

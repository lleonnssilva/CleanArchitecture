namespace CleanArchitecture.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public int ErrorCode { get; }
        public string UserMessage { get; }

        public DomainException(string message, int errorCode, string userMessage = null)
            : base(message)
        {
            ErrorCode = errorCode;
            UserMessage = userMessage;
        }
    }
}

namespace CleanArchitecture.Domain.Exceptions
{
    public class InvalidActiveException : Exception
    {
        public InvalidActiveException(string message) : base(message) { }
    }
}

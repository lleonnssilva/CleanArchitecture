namespace CleanArchitecture.Domain.Interfaces.RabbitMQ
{
    public interface IMessageSQSProducer
    {
        void SendMessage<T>(T message, string tipo);
    }
}

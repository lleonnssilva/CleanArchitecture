namespace CleanArchitecture.Domain.Interfaces.RabbitMQ
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message, string tipo);
    }
}

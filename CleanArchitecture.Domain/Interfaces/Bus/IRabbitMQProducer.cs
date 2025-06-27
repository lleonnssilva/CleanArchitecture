namespace CleanArchitecture.Domain.Interfaces.RabbitMQ
{
    public interface IRabbitMQProducer
    {
        void SendMessage<T>(T message, string tipo);
    }
}

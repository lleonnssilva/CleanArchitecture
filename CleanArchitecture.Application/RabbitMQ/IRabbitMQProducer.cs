namespace CleanArchitecture.Application.RabbitMQ
{
    public interface IRabbitMQProducer
    {
        void SendMessage<T>(T message, string tipo);
    }
}

namespace CleanArchitecture.Domain.Interfaces.MessageBus
{
    public interface IMessageRMQProducer
    {
        void SendMessage<T>(T message, string tipo);
    }
}

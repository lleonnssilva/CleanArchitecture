using CleanArchitecture.Domain.Events;
using CleanArchitecture.Domain.Interfaces.MessageBus;
using Newtonsoft.Json;

namespace CleanArchitecture.Infrastructure.Events
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IMessageRMQProducer _messageProducer;

        public EventPublisher(IMessageRMQProducer messageProducer)
        {
            _messageProducer = messageProducer;
        }

        public void Publish<T>(T evento)
        {
            var jsonEvento = JsonConvert.SerializeObject(evento);
            Console.WriteLine($"Evento: {evento.GetType().Name}");
            Console.WriteLine($"Serializado: {jsonEvento}");

            _messageProducer.SendMessage(evento, evento.GetType().Name);
            Console.WriteLine($"Evento publicado: {jsonEvento}");
        }
    }
}

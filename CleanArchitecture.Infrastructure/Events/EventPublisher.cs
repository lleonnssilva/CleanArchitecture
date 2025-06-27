using CleanArchitecture.Domain.Events;
using CleanArchitecture.Domain.Interfaces.RabbitMQ;
using Newtonsoft.Json;

namespace CleanArchitecture.Infrastructure.Events
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IRabbitMQProducer _rabitMQProducer;

        public EventPublisher(IRabbitMQProducer rabitMQProducer)
        {
            _rabitMQProducer = rabitMQProducer;
        }

        public void Publicar<T>(T evento)
        {
            var jsonEvento = JsonConvert.SerializeObject(evento);
            Console.WriteLine($"Evento: {evento.GetType().Name}");
            Console.WriteLine($"Serializado: {jsonEvento}");

            _rabitMQProducer.SendMessage(evento, evento.GetType().Name);
            Console.WriteLine($"Evento publicado: {jsonEvento}");
        }
    }
}

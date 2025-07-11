﻿using CleanArchitecture.Domain.Events;
using CleanArchitecture.Domain.Interfaces.MessageBus;
using CleanArchitecture.Domain.Interfaces.RabbitMQ;
using Newtonsoft.Json;

namespace CleanArchitecture.Infrastructure.Events
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IMessageSQSProducer _messageProducer;

        public EventPublisher(IMessageSQSProducer messageProducer)
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

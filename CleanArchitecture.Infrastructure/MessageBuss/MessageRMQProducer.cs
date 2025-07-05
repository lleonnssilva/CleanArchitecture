using CleanArchitecture.Domain.Interfaces.MessageBus;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace CleanArchitecture.Infrastructure.MessageBuss
{
    public class MessageRMQProducer : IMessageRMQProducer
    {
        private readonly IConfiguration _configuration;

        public MessageRMQProducer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendMessage<T>(T message, string tipo)
        {

            var factory = new ConnectionFactory
            {
                HostName = _configuration.GetConnectionString("RMQConnection")
            };

            var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();

            channel.QueueDeclare(tipo, exclusive: false);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: tipo, body: body);


        }
    }
}

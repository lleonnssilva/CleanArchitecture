using CleanArchitecture.Domain.Interfaces.RabbitMQ;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace CleanArchitecture.Infrastructure.MessageBuss
{
    public class MessageProducer : IMessageProducer
    {
        public void SendMessage<T>(T message, string tipo)
        {

            var factory = new ConnectionFactory
            {
                HostName = "localhost"
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

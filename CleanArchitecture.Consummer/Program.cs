using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Text;

//Definição do servidor Rabbit MQ : usamos uma imagem Docker
var factory = new ConnectionFactory
{
    HostName = "localhost"
};

//Cria uma conexão RabbitMQ usando uma factory
var connection = factory.CreateConnection();

//Cria um channel com sessão e model
using var channel = connection.CreateModel();

//declara a fila(queue) a seguir o nome e propriedades
channel.QueueDeclare("product", exclusive: false);

//Define o objeto Event o qual vai ouvir a mensagem do channel enviado pelo producer
var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Product message received: {message}");
};

//le a mensagem
channel.BasicConsume(queue: "product", autoAck: true, consumer: consumer);

Console.ReadKey();
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Text;


class Program
{

    private static readonly string queueUrl = "https://sqs.us-east-1.amazonaws.com/676206946905/MyQueeCliente";
    private static readonly AmazonSQSClient sqsClient = new AmazonSQSClient("", "", RegionEndpoint.USEast1);
    static async Task Main(string[] args)
    {

        await LoadSQS();
        //LoadRMQ();

    }
    static void LoadRMQ()
    {
        Console.WriteLine("Iniciando o processo de escuta da fila RMQ...");
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        //Cria uma conexão RabbitMQ usando uma factory
        var connection = factory.CreateConnection();

        //Cria um channel com sessão e model
        using var channel = connection.CreateModel();

        //declara a fila(queue) a seguir o nome e propriedades
        channel.QueueDeclare("ClienteCadastradoEvent", exclusive: false);

        //Define o objeto Event o qual vai ouvir a mensagem do channel enviado pelo producer
        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"Clientet message received: {message}");
        };

        //le a mensagem
        channel.BasicConsume(queue: "ClienteCadastradoEvent", autoAck: true, consumer: consumer);

        Console.ReadKey();
    }
    static async Task LoadSQS()
    {
        Console.WriteLine("Iniciando o processo de escuta da fila SQS...");
        while (true)
        {
            try
            {
                var receiveMessageRequest = new ReceiveMessageRequest
                {
                    QueueUrl = queueUrl,
                    MaxNumberOfMessages = 1,  // Quantas mensagens você quer processar de uma vez
                    WaitTimeSeconds = 20,     // Long polling para melhorar a performance
                    VisibilityTimeout = 30,    // O tempo em que a mensagem ficará invisível para outros consumidores
                    MessageAttributeNames = new List<string> { "EventType" }
                };

                var receiveMessageResponse = await sqsClient.ReceiveMessageAsync(receiveMessageRequest);

                if (
                    receiveMessageResponse.Messages != null &&
                    receiveMessageResponse.Messages.Count > 0 &&
                    receiveMessageResponse.Messages[0].MessageAttributes != null &&
                    receiveMessageResponse.Messages[0].MessageAttributes.ContainsKey("EventType") &&
                    receiveMessageResponse.Messages[0].MessageAttributes["EventType"].StringValue == "ClienteCadastradoEvent")
                {
                    var message = receiveMessageResponse.Messages[0];



                    Console.WriteLine($"Mensagem recebida: {message.Body}");

                    await ProcessMessageAsync(message);

                    await DeleteMessageAsync(message);


                }
                else
                {
                    Console.WriteLine("Nenhuma mensagem encontrada. Aguardando...");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar receber mensagens: {ex.Message}");
            }

            await Task.Delay(1000);
        }
    }
    private static async Task ProcessMessageAsync(Message message)
    {
        Console.WriteLine($"Processando mensagem: {message.Body}");
        await Task.Delay(500);
    }

    private static async Task DeleteMessageAsync(Message message)
    {
        var deleteMessageRequest = new DeleteMessageRequest
        {
            QueueUrl = queueUrl,
            ReceiptHandle = message.ReceiptHandle
        };

        await sqsClient.DeleteMessageAsync(deleteMessageRequest);
        Console.WriteLine("Mensagem excluída da fila.");
        await LoadSQS();
    }

}

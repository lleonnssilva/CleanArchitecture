using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using CleanArchitecture.Domain.Interfaces.RabbitMQ;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace CleanArchitecture.Infrastructure.MessageBuss
{
    public class MessageSQSProducer: IMessageSQSProducer
    {
        private readonly IAmazonSQS _sqsClient;
        private readonly string _queueUrl;

        private readonly IConfiguration _configuration;
        public MessageSQSProducer(IConfiguration configuration)
        {
            _configuration = configuration;
            var awsCredentials = new BasicAWSCredentials(_configuration["Aws:keySQS"], _configuration["Aws:SecretsSQS"]);
            _sqsClient = new AmazonSQSClient(awsCredentials,RegionEndpoint.USEast1);

            _queueUrl = _configuration["Aws:UrlSQS"];
        }

        public async void SendMessage<T>(T message, string tipoEvento)
        {
            try
            {
                var json = JsonSerializer.Serialize(message);
                var sendMessageRequest = new SendMessageRequest
                {
                    QueueUrl = _queueUrl,
                    MessageBody = json,
                    MessageAttributes = new Dictionary<string, MessageAttributeValue>
                    {
                        { "EventType", new MessageAttributeValue { StringValue = tipoEvento, DataType = "String" } }
                    }
                };

                var response = await _sqsClient.SendMessageAsync(sendMessageRequest);
                Console.WriteLine($"Message sent! MessageId: {response.MessageId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
            }
        }
    }
}

using Azure.Storage.Queues;

namespace DemoQueueStorage.Services
{
    public class QueueStorageService
    {
        private readonly string _connectionString;
        private const string QueueName = "orders";

        public QueueStorageService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task SendMessageAsync(string message)
        {
            QueueClient queueClient =
                new QueueClient(_connectionString, QueueName);

            await queueClient.CreateIfNotExistsAsync();

            await queueClient.SendMessageAsync(message);

            Console.WriteLine($"Message Sent: {message}");
        }

        public async Task ReceiveMessageAsync()
        {
            QueueClient queueClient =
                new QueueClient(_connectionString, QueueName);

            var response =
                await queueClient.ReceiveMessageAsync();

            if (response.Value != null)
            {
                Console.WriteLine(
                    $"Received Message: {response.Value.MessageText}");

                await queueClient.DeleteMessageAsync(
                    response.Value.MessageId,
                    response.Value.PopReceipt);
            }
        }
    }
}

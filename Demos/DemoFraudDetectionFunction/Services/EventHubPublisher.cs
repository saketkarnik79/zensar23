using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using DemoFraudDetectionFunction.Models;

namespace DemoFraudDetectionFunction.Services
{
    public class EventHubPublisher
    {
        private readonly EventHubProducerClient _producer;

        public EventHubPublisher(
            IConfiguration configuration)
        {
            _producer = new EventHubProducerClient(
                configuration["EventHubConnection"]);
        }

        public async Task PublishAsync(
            TransactionEvent transaction)
        {
            using EventDataBatch batch =
                await _producer.CreateBatchAsync();

            batch.TryAdd(
                new EventData(
                    JsonSerializer.Serialize(transaction)));

            await _producer.SendAsync(batch);
        }
    }
}
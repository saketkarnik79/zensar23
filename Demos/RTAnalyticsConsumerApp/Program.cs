using Azure.Messaging.EventHubs.Consumer;
using System.Text;

string connectionString =
    "Endpoint=sb://skzenehns.servicebus.windows.net/;SharedAccessKeyName=analytics-policy;SharedAccessKey=E95jT2xofr8bsTh4RK/O9LEnXcRj3xQaa+AEhNYFzx4=;EntityPath=bank-transactions-stream";

string consumerGroup =
    "fraud-analytics";

await using var consumer =
    new EventHubConsumerClient(
        consumerGroup,
        connectionString);

await foreach (PartitionEvent evt
                 in consumer.ReadEventsAsync())
{
    string data =
        Encoding.UTF8.GetString(
            evt.Data.Body.ToArray());

    Console.WriteLine(data);
}
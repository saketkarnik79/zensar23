using Azure.Messaging.ServiceBus;
using SharedModels;
using System.Text.Json;

string connectionString =
    "Endpoint=sb://skzensb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=+ef5zBuztB877ia2osnw5Sww9Gm1cUW8m+ASbNBhz64=";

string topicName =
    "transactiontopic";

await using ServiceBusClient client =
    new ServiceBusClient(connectionString);

ServiceBusSender sender =
    client.CreateSender(topicName);

var transaction =
    new TransactionEvent
    {
        TransactionId = Guid.NewGuid().ToString(),

        CustomerId = "CUST1001",

        FromAccount = "SBI100001",

        ToAccount = "ICICI200001",

        Amount = 275000,

        Channel = "InternetBanking",

        Status = "SUCCESS",

        TransactionTime = DateTime.UtcNow
    };

string messageBody =
    JsonSerializer.Serialize(transaction);

ServiceBusMessage message =
    new ServiceBusMessage(messageBody);

message.MessageId =
    transaction.TransactionId;

message.ApplicationProperties.Add(
    "Amount",
    transaction.Amount);

message.ApplicationProperties.Add(
    "Channel",
    transaction.Channel);

await sender.SendMessageAsync(message);

Console.WriteLine(
    $"Transaction Published : {transaction.TransactionId}");
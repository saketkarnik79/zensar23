using Azure.Messaging.ServiceBus;
using System.Text.Json;
using TransferConsumer.Models;

string connectionString = "Endpoint=sb://skzensb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=+ef5zBuztB877ia2osnw5Sww9Gm1cUW8m+ASbNBhz64=";
string queueName = "transferq";

await using var client = new ServiceBusClient(connectionString);
ServiceBusProcessor processor = client.CreateProcessor(queueName,
    new ServiceBusProcessorOptions()
    {
        AutoCompleteMessages = false,
    });
processor.ProcessMessageAsync += MessageHandler;
processor.ProcessErrorAsync += ErrorHandler;

await processor.StartProcessingAsync();
Console.WriteLine("Listening for transfer requests. Press any key to exit...");
Console.ReadKey();
await processor.StopProcessingAsync();

async Task MessageHandler(ProcessMessageEventArgs args)
{
    string json = args.Message.Body.ToString();
    var transferRequest = JsonSerializer.Deserialize<TransferRequest>(json);
    Console.WriteLine($"Received transfer request.\nTransaction ID: {transferRequest.TransactionId}\nFrom: {transferRequest.AccountFrom}\nTo: {transferRequest.AccountTo}\nAmount: {transferRequest.Amount}");

    //await args.CompleteMessageAsync(args.Message);
}

Task ErrorHandler(ProcessErrorEventArgs args)
{
    Console.WriteLine($"Error occurred: {args.Exception.Message}");
    return Task.CompletedTask;
}
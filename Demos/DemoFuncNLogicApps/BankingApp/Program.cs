using Azure.Messaging.ServiceBus;
using System.Text.Json;

var client = new ServiceBusClient("Endpoint=sb://skzensbns.servicebus.windows.net/;SharedAccessKeyName=WriterKey;SharedAccessKey=ijHWzLOJOp+6opGsd2pwkXzB2+b5pAUQe+ASbLkEUEU=");
var sender = client.CreateSender("payment-events");

var paymentEvent = new
{
    PaymentId = Guid.NewGuid(),
    CustomerId = "CUST12345",
    CustomerName = "John Doe",
    Email = "john@oizen.com",
    PhoneNumber = "+91-555-123-4567",
    Amount = 50000.00m,
    Status = "Completed",
    TransactionDate = DateTime.UtcNow
};

var message = new ServiceBusMessage(JsonSerializer.Serialize(paymentEvent));
await sender.SendMessageAsync(message);

Console.WriteLine("Payment event sent successfully.");
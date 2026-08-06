using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using PaymentNotificationFunction.Models;
using PaymentNotificationFunction.Services;

namespace BankingFunctions;

public class PaymentEventProcessor
{
    private readonly ILogger<PaymentEventProcessor> _logger;
    private readonly INotificationService _notificationService;


    public PaymentEventProcessor(ILogger<PaymentEventProcessor> logger, INotificationService notificationService)
    {
        _logger = logger;
        _notificationService = notificationService;
    }

    [Function(nameof(PaymentEventProcessor))]
    public async Task Run(
        [ServiceBusTrigger("payment-events", "payement-notification-sub", Connection = "ServiceBusConnection")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        _logger.LogInformation("Message ID: {id}", message.MessageId);
        _logger.LogInformation("Message Body: {body}", message.Body);
        _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

        var paymentEvent = JsonSerializer.Deserialize<PaymentEvent>(message.Body)!;

        await _notificationService.SendNotificationAsync(paymentEvent);

        // Complete the message
        await messageActions.CompleteMessageAsync(message);
    }
}
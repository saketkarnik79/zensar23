// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}

using System;
using Azure.Messaging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Azure.Messaging.EventGrid;
using DemoFraudDetectionFunction.Models;
using DemoFraudDetectionFunction.Services;
using System.Text.Json;

namespace DemoFraudDetectionFunction;

public class FraudDetectionFunction
{
    private readonly ILogger<FraudDetectionFunction> _logger;
    private readonly EventHubPublisher _eventHubPublisher;

    public FraudDetectionFunction(ILogger<FraudDetectionFunction> logger, EventHubPublisher eventHubPublisher)
    {
        _logger = logger;
        _eventHubPublisher = eventHubPublisher;
    }

    [Function(nameof(FraudDetectionFunction))]
    public async Task Run([EventGridTrigger] EventGridEvent cloudEvent)
    {
        _logger.LogInformation("Transactions Event Received with Event type: {type}, Event subject: {subject}", cloudEvent.EventType, cloudEvent.Subject);

        string? json = cloudEvent.Data?.ToString();
        _logger.LogInformation(
            "JSON Payload: {json}",
            json ?? "{}");

        // var transaction =
        //     cloudEvent.Data?.ToObjectFromJson<TransactionEvent>();

        var transaction = JsonSerializer.Deserialize<TransactionEvent>(
        json,
        new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (transaction == null)
        {
            _logger.LogWarning("Transaction data missing.");
            return;
        }

        _logger.LogInformation(
            "TransactionId : {TransactionId}",
            transaction.TransactionId);

        await DetectFraud(transaction);
        await _eventHubPublisher.PublishAsync(transaction);
    }

    private async Task DetectFraud(
        TransactionEvent transaction)
    {
        bool fraudDetected = false;

        string alertType = "";

        string riskLevel = "";

        if (transaction.Amount > 100000)
        {
            fraudDetected = true;

            alertType =
                "High Value Transaction";

            riskLevel =
                "HIGH";
        }

        if (fraudDetected)
        {
            var alert = new FraudAlert
            {
                AlertId = Guid.NewGuid().ToString(),
                TransactionId =
                    transaction.TransactionId,
                CustomerId =
                    transaction.CustomerId,
                AlertType =
                    alertType,
                RiskLevel =
                    riskLevel,
                GeneratedOn =
                    DateTime.UtcNow
            };

            _logger.LogWarning(
                "FRAUD ALERT GENERATED : {AlertId}",
                alert.AlertId);

            _logger.LogWarning(
                "Transaction {TransactionId} flagged as suspicious",
                alert.TransactionId);

            await SaveFraudAlert(alert);
        }
        else
        {
            _logger.LogInformation(
                "Transaction passed fraud validation.");
        }
    }

    private async Task SaveFraudAlert(
        FraudAlert alert)
    {
        await Task.CompletedTask;

        _logger.LogInformation(
            "Fraud alert stored successfully.");
    }
}
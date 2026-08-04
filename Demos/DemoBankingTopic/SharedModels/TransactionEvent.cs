namespace SharedModels;

public class TransactionEvent
{
    public string? TransactionId { get; set; }

    public string? CustomerId { get; set; }

    public string? FromAccount { get; set; }

    public string? ToAccount { get; set; }

    public decimal Amount { get; set; }

    public string? Channel { get; set; }

    public string? Status { get; set; }

    public DateTime TransactionTime { get; set; }
}

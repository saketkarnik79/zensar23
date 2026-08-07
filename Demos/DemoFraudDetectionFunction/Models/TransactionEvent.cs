namespace DemoFraudDetectionFunction.Models
{
    public class TransactionEvent
    {
        public string TransactionId { get; set; }

        public string CustomerId { get; set; }

        public string AccountNumber { get; set; }

        public decimal Amount { get; set; }

        public string TransactionType { get; set; }

        public string Channel { get; set; }

        public string Status { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
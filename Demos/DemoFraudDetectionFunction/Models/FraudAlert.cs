namespace DemoFraudDetectionFunction.Models
{
    public class FraudAlert
    {
        public string AlertId { get; set; }

        public string TransactionId { get; set; }

        public string CustomerId { get; set; }

        public string AlertType { get; set; }

        public string RiskLevel { get; set; }

        public DateTime GeneratedOn { get; set; }
    }
}

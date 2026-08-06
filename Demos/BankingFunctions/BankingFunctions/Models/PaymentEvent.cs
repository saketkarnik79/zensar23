namespace PaymentNotificationFunction.Models
{
    public class PaymentEvent
    {
        public Guid PaymentId { get; set; }
        public string CustomerId { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public decimal Amount { get; set; }
        public string Status { get; set; } = null!;
        public DateTime TransactionDate { get; set; }
    }
}

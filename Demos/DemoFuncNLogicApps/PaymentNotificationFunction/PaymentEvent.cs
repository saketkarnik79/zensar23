namespace PaymentNotificationFunction
{
    public class PaymentEvent
    {
        public string PaymentId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}

namespace TransferProducer.Models
{
    public class TransferRequest
    {
        public string? TransactionId { get; set; }
        public string? AccountFrom { get; set; }
        public string? AccountTo { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
namespace BankingApis.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public string? SourceAccount { get; set; }

        public string? DestinationAccount { get; set; }

        public decimal Amount { get; set; }

        public string? TransactionType { get; set; }

        public DateTime TransactionDate { get; set; }

        public string? Status { get; set; }
    }
}
namespace BankingApis.DTOs
{
    public class TransferRequest
    {
        public string? FromAccount { get; set; }

        public string? ToAccount { get; set; }

        public decimal Amount { get; set; }
    }
}
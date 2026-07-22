namespace BankingApis.Entities
{
    public class Account
    {
        public Guid Id { get; set; }

        public string? AccountNumber { get; set; }

        public decimal Balance { get; set; }

        public string? AccountType { get; set; }

        public Guid CustomerId { get; set; }

        public Customer? Customer { get; set; }
    }
}
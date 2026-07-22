namespace BankingApis.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? MobileNumber { get; set; }

        public string? PANNumber { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<Account>? Accounts { get; set; }
    }
}
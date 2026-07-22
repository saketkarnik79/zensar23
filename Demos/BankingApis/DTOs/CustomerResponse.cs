namespace BankingApis.DTOs
{
    public class CustomerResponse
    {
        public Guid CustomerId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string MobileNumber { get; set; } = string.Empty;

        public string PanNumber { get; set; } = string.Empty;

        public string KycStatus { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
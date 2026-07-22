namespace BankingApis.DTOs
{
    public class CreateCustomerRequest
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string MobileNumber { get; set; } = string.Empty;

        public string PanNumber { get; set; } = string.Empty;

        public string AadhaarNumber { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; } = string.Empty;

        public string Occupation { get; set; } = string.Empty;
    }
}
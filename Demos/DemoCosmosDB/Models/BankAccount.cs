using Newtonsoft.Json;

namespace DemoCosmosDB.Models
{
    public class BankAccount
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("customerId")]
        public string? CustomerId { get; set; }

        public string? AccountHolderName { get; set; }

        public string? AccountType { get; set; }

        public decimal Balance { get; set; }

        public string? BranchCode { get; set; }
    }
}
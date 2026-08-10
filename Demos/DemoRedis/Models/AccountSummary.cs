namespace DemoRedis.Models;

public class AccountSummary
{
    public int AccountId { get; set; }

    public string? CustomerName { get; set; }

    public decimal Balance { get; set; }

    public decimal AvailableBalance { get; set; }

    public int RewardPoints { get; set; }

    public string? AccountStatus { get; set; }
}
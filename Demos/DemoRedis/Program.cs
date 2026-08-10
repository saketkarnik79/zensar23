using Microsoft.Data.SqlClient;
using StackExchange.Redis;
using System.Text.Json;
using DemoRedis.Models;

string? dbcs = "Server=tcp:skoizensql.database.windows.net,1433;Database=SKOIZENDemoDb;Persist Security Info=False;User ID=zenadmin;Password=P@ssw0rd;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";

string? rediscs = "skoizenredis.southeastasia.redis.azure.net:10000,password=qvSXumXqT6j6wCpHAtFeyYY0mZZA_wFERAZCAGmvxOo=,ssl=True";

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(rediscs);
IDatabase cache = redis.GetDatabase();

AccountSummary? account =
    await GetAccountSummaryAsync(
        10001,
        cache,
        dbcs);

if (account != null)
{
    DisplayAccount(account);
}

// await TransferMoneyAsync(10001, 5000, cache, dbcs);

static async Task<AccountSummary?> GetAccountSummaryAsync(
    int accountId,
    IDatabase cache,
    string sqlConnectionString)
{
    string cacheKey = $"account:{accountId}";

    Console.WriteLine("Checking Redis Cache...");

    string? cachedData =
        await cache.StringGetAsync(cacheKey);

    if (!string.IsNullOrEmpty(cachedData))
    {
        Console.ForegroundColor = ConsoleColor.Green;

        Console.WriteLine("Cache HIT");

        Console.ResetColor();

        return JsonSerializer.Deserialize<AccountSummary>(cachedData);
    }

    Console.ForegroundColor = ConsoleColor.Yellow;

    Console.WriteLine("Cache MISS");

    Console.ResetColor();

    AccountSummary? account = null;

    using SqlConnection connection =
        new SqlConnection(sqlConnectionString);

    await connection.OpenAsync();

    string query = @"
        SELECT *
        FROM Accounts
        WHERE AccountId = @AccountId";

    SqlCommand command =
        new SqlCommand(query, connection);

    command.Parameters.AddWithValue("@AccountId", accountId);

    SqlDataReader reader =
        await command.ExecuteReaderAsync();

    if (await reader.ReadAsync())
    {
        account = new AccountSummary
        {
            AccountId = Convert.ToInt32(reader["AccountId"]),
            CustomerName = reader["CustomerName"].ToString(),
            Balance = Convert.ToDecimal(reader["Balance"]),
            AvailableBalance = Convert.ToDecimal(reader["AvailableBalance"]),
            RewardPoints = Convert.ToInt32(reader["RewardPoints"]),
            AccountStatus = reader["AccountStatus"].ToString()
        };

        await cache.StringSetAsync(
            cacheKey,
            JsonSerializer.Serialize(account),
            TimeSpan.FromMinutes(10));

        Console.WriteLine("Data stored in Redis");
    }

    return account;
}

static void DisplayAccount(AccountSummary account)
{
    Console.WriteLine();
    Console.WriteLine("----- Account Summary -----");
    Console.WriteLine($"Account Id       : {account.AccountId}");
    Console.WriteLine($"Customer Name    : {account.CustomerName}");
    Console.WriteLine($"Balance          : {account.Balance:C}");
    Console.WriteLine($"Available Balance: {account.AvailableBalance:C}");
    Console.WriteLine($"Reward Points    : {account.RewardPoints}");
    Console.WriteLine($"Status           : {account.AccountStatus}");
}

static async Task TransferMoneyAsync(
    int accountId,
    decimal amount,
    IDatabase cache,
    string sqlConnectionString)
{
    using SqlConnection connection =
        new SqlConnection(sqlConnectionString);

    await connection.OpenAsync();

    string updateQuery = @"
            UPDATE Accounts
            SET Balance = Balance - @Amount,
                AvailableBalance = AvailableBalance - @Amount
            WHERE AccountId = @AccountId";

    SqlCommand cmd =
        new SqlCommand(updateQuery, connection);

    cmd.Parameters.AddWithValue("@Amount", amount);
    cmd.Parameters.AddWithValue("@AccountId", accountId);

    await cmd.ExecuteNonQueryAsync();

    await cache.KeyDeleteAsync($"account:{accountId}");

    Console.WriteLine("Cache Invalidated");
}
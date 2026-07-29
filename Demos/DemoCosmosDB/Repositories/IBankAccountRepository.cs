using DemoCosmosDB.Models;

namespace DemoCosmosDB.Repositories
{
    public interface IBankAccountRepository
    {
        Task<BankAccount> CreateAsync(BankAccount account);

        Task<BankAccount> GetAsync(string accountId, string customerId);

        Task<IEnumerable<BankAccount>> GetAllByCustomerAsync(string customerId);

        Task<BankAccount> UpdateAsync(BankAccount account);

        Task DeleteAsync(string accountId, string customerId);

    }
}
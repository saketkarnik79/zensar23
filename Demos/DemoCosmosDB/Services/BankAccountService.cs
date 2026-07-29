using DemoCosmosDB.Models;
using DemoCosmosDB.Repositories;

namespace DemoCosmosDB.Services
{
    public class BankAccountService
    {
        private readonly IBankAccountRepository _repository;

        public BankAccountService(
            IBankAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task DepositAsync(
            string accountId,
            string customerId,
            decimal amount)
        {
            var account =
                await _repository.GetAsync(
                    accountId,
                    customerId);

            if (account == null)
                throw new Exception("Account not found");

            account.Balance += amount;

            await _repository.UpdateAsync(account);
        }

        public async Task WithdrawAsync(
            string accountId,
            string customerId,
            decimal amount)
        {
            var account =
                await _repository.GetAsync(
                    accountId,
                    customerId);

            if (account == null)
                throw new Exception("Account not found");

            if (account.Balance < amount)
                throw new Exception("Insufficient funds");

            account.Balance -= amount;

            await _repository.UpdateAsync(account);
        }
    }
}
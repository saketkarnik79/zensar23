using BankingApis.DTOs;
using BankingApis.Data;
using Microsoft.EntityFrameworkCore;
using BankingApis.Entities;

namespace BankingApis.Services
{
    public class AccountService : IAccountService
    {
        private readonly BankingDbContext _context;

        public AccountService(BankingDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetBalance(string accountNumber)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);

            return account?.Balance ?? 0;
        }

        public async Task<bool> TransferFunds(TransferRequest request)
        {
            using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                var source = await _context.Accounts
                    .FirstOrDefaultAsync(x =>
                        x.AccountNumber == request.FromAccount);

                var destination = await _context.Accounts
                    .FirstOrDefaultAsync(x =>
                        x.AccountNumber == request.ToAccount);

                if (source == null || destination == null)
                    return false;

                if (source.Balance < request.Amount)
                    return false;

                source.Balance -= request.Amount;
                destination.Balance += request.Amount;

                _context.Transactions.Add(new Transaction
                {
                    Id = Guid.NewGuid(),
                    SourceAccount = source.AccountNumber,
                    DestinationAccount = destination.AccountNumber,
                    Amount = request.Amount,
                    TransactionDate = DateTime.UtcNow,
                    Status = "SUCCESS",
                    TransactionType = "TRANSFER"
                });

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}
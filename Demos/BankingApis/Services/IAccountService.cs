using BankingApis.DTOs;

namespace BankingApis.Services
{
    public interface IAccountService
    {
        Task<decimal> GetBalance(string accountNumber);

        Task<bool> TransferFunds(TransferRequest request);
    }
}
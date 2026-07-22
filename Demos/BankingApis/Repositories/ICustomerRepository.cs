using BankingApis.Entities;

namespace BankingApis.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(Guid id);

        Task<List<Customer>> GetAllAsync();

        Task AddAsync(Customer customer);

        Task UpdateAsync(Customer customer);

        Task<bool> ExistsByPanAsync(string pan);

        Task SaveChangesAsync();
    }
}
using FinanceService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceService.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Transaction> AddAsync(Transaction transaction);
        Task<IEnumerable<Transaction>> GetByUserIdAsync(string userId);
    }

    public interface IBudgetRepository
    {
        Task<Budget> UpsertAsync(Budget budget);
        Task<Budget?> GetByUserAndMonthAsync(string userId, string month);
    }
}

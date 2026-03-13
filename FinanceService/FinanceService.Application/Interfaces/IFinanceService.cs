using FinanceService.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceService.Application.Interfaces
{
    public interface IFinanceService
    {
        Task<TransactionDto> AddTransactionAsync(string userId, CreateTransactionDto dto);
        Task<IEnumerable<TransactionDto>> GetTransactionsAsync(string userId);
        Task<BudgetDto> UpsertBudgetAsync(string userId, CreateBudgetDto dto);
        Task<BudgetDto?> GetBudgetAsync(string userId, string month);
    }
}

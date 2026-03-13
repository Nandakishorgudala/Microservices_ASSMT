using InsightsService.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsightsService.Application.Interfaces
{
    public interface IFinanceClient
    {
        Task<IEnumerable<ExternalTransactionDto>> GetTransactionsAsync(string token);
        Task<ExternalBudgetDto?> GetBudgetAsync(string token, string month);
    }

    public interface IInsightsService
    {
        Task<FinancialHealthDto> GetFinancialHealthAsync(string userId, string token);
    }
}

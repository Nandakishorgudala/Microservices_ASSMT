using InsightsService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsightsService.Domain.Interfaces
{
    public interface IFinancialSummaryRepository
    {
        Task<FinancialSummary> AddAsync(FinancialSummary summary);
        Task<FinancialSummary?> GetLatestByUserIdAsync(string userId);
    }

    public interface ISpendingCategorySummaryRepository
    {
        Task AddRangeAsync(IEnumerable<SpendingCategorySummary> summaries);
        Task<IEnumerable<SpendingCategorySummary>> GetByUserIdAsync(string userId);
    }
}

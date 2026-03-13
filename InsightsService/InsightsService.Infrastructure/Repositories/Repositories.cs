using InsightsService.Domain.Entities;
using InsightsService.Domain.Interfaces;
using InsightsService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsightsService.Infrastructure.Repositories
{
    public class FinancialSummaryRepository : IFinancialSummaryRepository
    {
        private readonly InsightsDbContext _context;

        public FinancialSummaryRepository(InsightsDbContext context)
        {
            _context = context;
        }

        public async Task<FinancialSummary> AddAsync(FinancialSummary summary)
        {
            _context.FinancialSummaries.Add(summary);
            await _context.SaveChangesAsync();
            return summary;
        }

        public async Task<FinancialSummary?> GetLatestByUserIdAsync(string userId)
        {
            return await _context.FinancialSummaries
                .Where(f => f.UserId == userId)
                .OrderByDescending(f => f.GeneratedDate)
                .FirstOrDefaultAsync();
        }
    }

    public class SpendingCategorySummaryRepository : ISpendingCategorySummaryRepository
    {
        private readonly InsightsDbContext _context;

        public SpendingCategorySummaryRepository(InsightsDbContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(IEnumerable<SpendingCategorySummary> summaries)
        {
            _context.CategorySummaries.AddRange(summaries);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SpendingCategorySummary>> GetByUserIdAsync(string userId)
        {
            return await _context.CategorySummaries
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.GeneratedDate)
                .ToListAsync();
        }
    }
}

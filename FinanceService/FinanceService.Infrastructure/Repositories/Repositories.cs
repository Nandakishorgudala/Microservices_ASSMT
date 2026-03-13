using FinanceService.Domain.Entities;
using FinanceService.Domain.Interfaces;
using FinanceService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceService.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly FinanceDbContext _context;

        public TransactionRepository(FinanceDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction> AddAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<IEnumerable<Transaction>> GetByUserIdAsync(string userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }
    }

    public class BudgetRepository : IBudgetRepository
    {
        private readonly FinanceDbContext _context;

        public BudgetRepository(FinanceDbContext context)
        {
            _context = context;
        }

        public async Task<Budget> UpsertAsync(Budget budget)
        {
            var existing = await _context.Budgets
                .FirstOrDefaultAsync(b => b.UserId == budget.UserId && b.Month == budget.Month);

            if (existing != null)
            {
                existing.BudgetAmount = budget.BudgetAmount;
                _context.Budgets.Update(existing);
            }
            else
            {
                _context.Budgets.Add(budget);
            }

            await _context.SaveChangesAsync();
            return existing ?? budget;
        }

        public async Task<Budget?> GetByUserAndMonthAsync(string userId, string month)
        {
            return await _context.Budgets
                .FirstOrDefaultAsync(b => b.UserId == userId && b.Month == month);
        }
    }
}

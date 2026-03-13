using InsightsService.Application.Dtos;
using InsightsService.Application.Interfaces;
using InsightsService.Domain.Entities;
using InsightsService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsightsService.Application.Services
{
    public class InsightsService : IInsightsService
    {
        private readonly IFinanceClient _financeClient;
        private readonly IFinancialSummaryRepository _summaryRepository;
        private readonly ISpendingCategorySummaryRepository _categoryRepository;

        public InsightsService(
            IFinanceClient financeClient,
            IFinancialSummaryRepository summaryRepository,
            ISpendingCategorySummaryRepository categoryRepository)
        {
            _financeClient = financeClient;
            _summaryRepository = summaryRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<FinancialHealthDto> GetFinancialHealthAsync(string userId, string token)
        {
            var month = DateTime.UtcNow.ToString("yyyy-MM");
            var transactions = await _financeClient.GetTransactionsAsync(token);
            var budget = await _financeClient.GetBudgetAsync(token, month);

            var totalIncome = transactions.Where(t => t.Type == "Income").Sum(t => t.Amount);
            var totalExpenses = transactions.Where(t => t.Type == "Expense").Sum(t => t.Amount);
            var budgetAmount = budget?.BudgetAmount ?? 0;

            var categorySpending = transactions
                .Where(t => t.Type == "Expense")
                .GroupBy(t => t.Category)
                .Select(g => new SpendingTrendDto
                {
                    Category = g.Key,
                    TotalAmount = g.Sum(t => t.Amount)
                })
                .OrderByDescending(x => x.TotalAmount)
                .ToList();

            var topCategory = categorySpending.FirstOrDefault()?.Category ?? "N/A";
            var remainingBudget = budgetAmount - totalExpenses;
            var spendingPercentage = budgetAmount > 0 ? (double)(totalExpenses / budgetAmount) * 100 : 0;

            // Simple health score logic: 100 - (spending/budget * 100), capped at 0-100
            var healthScore = budgetAmount > 0 ? Math.Max(0, 100 - spendingPercentage) : 0;

            // Store summary in background (optional, but requested by tables)
            var summary = new FinancialSummary
            {
                UserId = userId,
                TotalIncome = totalIncome,
                TotalExpense = totalExpenses,
                RemainingBudget = remainingBudget,
                HealthScore = healthScore,
                GeneratedDate = DateTime.UtcNow
            };
            await _summaryRepository.AddAsync(summary);

            var catSummaries = categorySpending.Select(c => new SpendingCategorySummary
            {
                UserId = userId,
                Category = c.Category,
                TotalAmount = c.TotalAmount,
                GeneratedDate = DateTime.UtcNow
            });
            await _categoryRepository.AddRangeAsync(catSummaries);

            return new FinancialHealthDto
            {
                TotalIncome = totalIncome,
                TotalExpenses = totalExpenses,
                RemainingBudget = remainingBudget,
                SpendingPercentage = spendingPercentage,
                TopSpendingCategory = topCategory,
                FinancialHealthScore = healthScore
            };
        }
    }
}

using System;
using System.Collections.Generic;

namespace InsightsService.Application.Dtos
{
    public class FinancialHealthDto
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal RemainingBudget { get; set; }
        public double SpendingPercentage { get; set; }
        public string TopSpendingCategory { get; set; } = string.Empty;
        public double FinancialHealthScore { get; set; }
    }

    public class SpendingTrendDto
    {
        public string Category { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
    }

    // DTOs to map responses from Finance Service
    public class ExternalTransactionDto
    {
        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }

    public class ExternalBudgetDto
    {
        public decimal BudgetAmount { get; set; }
    }
}

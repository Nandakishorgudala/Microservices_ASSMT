using System;

namespace InsightsService.Domain.Entities
{
    public class FinancialSummary
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal RemainingBudget { get; set; }
        public double HealthScore { get; set; }
        public DateTime GeneratedDate { get; set; }
    }

    public class SpendingCategorySummary
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public DateTime GeneratedDate { get; set; }
    }
}

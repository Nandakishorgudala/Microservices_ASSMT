using System;

namespace FinanceService.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty; // Income/Expense
        public string Category { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class Budget
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Month { get; set; } = string.Empty; // e.g., "2024-03"
        public decimal BudgetAmount { get; set; }
    }
}

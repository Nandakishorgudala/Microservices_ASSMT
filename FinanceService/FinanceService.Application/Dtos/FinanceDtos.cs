using System;

namespace FinanceService.Application.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class CreateTransactionDto
    {
        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class BudgetDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Month { get; set; } = string.Empty;
        public decimal BudgetAmount { get; set; }
    }

    public class CreateBudgetDto
    {
        public string Month { get; set; } = string.Empty;
        public decimal BudgetAmount { get; set; }
    }
}

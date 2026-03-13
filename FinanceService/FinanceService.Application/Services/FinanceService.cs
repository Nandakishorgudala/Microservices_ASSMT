using FinanceService.Application.Dtos;
using FinanceService.Application.Interfaces;
using FinanceService.Domain.Entities;
using FinanceService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceService.Application.Services
{
    public class FinanceService : IFinanceService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IBudgetRepository _budgetRepository;

        public FinanceService(ITransactionRepository transactionRepository, IBudgetRepository budgetRepository)
        {
            _transactionRepository = transactionRepository;
            _budgetRepository = budgetRepository;
        }

        public async Task<TransactionDto> AddTransactionAsync(string userId, CreateTransactionDto dto)
        {
            var transaction = new Transaction
            {
                UserId = userId,
                Amount = dto.Amount,
                Type = dto.Type,
                Category = dto.Category,
                Description = dto.Description,
                Date = DateTime.UtcNow
            };

            var result = await _transactionRepository.AddAsync(transaction);

            return MapToDto(result);
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactionsAsync(string userId)
        {
            var transactions = await _transactionRepository.GetByUserIdAsync(userId);
            return transactions.Select(MapToDto);
        }

        public async Task<BudgetDto> UpsertBudgetAsync(string userId, CreateBudgetDto dto)
        {
            var budget = new Budget
            {
                UserId = userId,
                Month = dto.Month,
                BudgetAmount = dto.BudgetAmount
            };

            var result = await _budgetRepository.UpsertAsync(budget);

            return new BudgetDto
            {
                Id = result.Id,
                UserId = result.UserId,
                Month = result.Month,
                BudgetAmount = result.BudgetAmount
            };
        }

        public async Task<BudgetDto?> GetBudgetAsync(string userId, string month)
        {
            var budget = await _budgetRepository.GetByUserAndMonthAsync(userId, month);
            if (budget == null) return null;

            return new BudgetDto
            {
                Id = budget.Id,
                UserId = budget.UserId,
                Month = budget.Month,
                BudgetAmount = budget.BudgetAmount
            };
        }

        private TransactionDto MapToDto(Transaction t) => new TransactionDto
        {
            Id = t.Id,
            UserId = t.UserId,
            Amount = t.Amount,
            Type = t.Type,
            Category = t.Category,
            Description = t.Description,
            Date = t.Date
        };
    }
}

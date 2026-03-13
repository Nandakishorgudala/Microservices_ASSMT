using FinanceService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceService.Infrastructure.Data
{
    public class FinanceDbContext : DbContext
    {
        public FinanceDbContext(DbContextOptions<FinanceDbContext> options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Budget> Budgets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>().Property(t => t.Amount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Budget>().Property(b => b.BudgetAmount).HasColumnType("decimal(18,2)");
        }
    }
}

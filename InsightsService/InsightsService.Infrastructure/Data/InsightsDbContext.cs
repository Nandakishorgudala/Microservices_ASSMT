using InsightsService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsightsService.Infrastructure.Data
{
    public class InsightsDbContext : DbContext
    {
        public InsightsDbContext(DbContextOptions<InsightsDbContext> options) : base(options) { }

        public DbSet<FinancialSummary> FinancialSummaries { get; set; }
        public DbSet<SpendingCategorySummary> CategorySummaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FinancialSummary>().Property(f => f.TotalIncome).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<FinancialSummary>().Property(f => f.TotalExpense).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<FinancialSummary>().Property(f => f.RemainingBudget).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<SpendingCategorySummary>().Property(s => s.TotalAmount).HasColumnType("decimal(18,2)");
        }
    }
}

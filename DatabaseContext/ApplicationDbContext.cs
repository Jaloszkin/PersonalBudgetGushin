using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalBudgetGushin.Entities;

namespace PersonalBudgetGushin.DatabaseContext
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<TransactionEntity> Transactions { get; set; } = null!;

        public ApplicationDbContext() 
        { 
            Database.EnsureCreated();
        }

        public async Task<int> GetTransactionCountAsync()
        {
            return await Transactions.CountAsync();
        }

        public async Task<decimal> GetTotalTransactionAmountAsync()
        {
            return await Transactions.SumAsync(t => t.Amount);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.Current.AppDataDirectory, "PersonalBudgetDb.db");
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
    }
}

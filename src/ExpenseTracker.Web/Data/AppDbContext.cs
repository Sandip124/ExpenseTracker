using ExpenseTracker.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Data
{
    internal class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Workspace>()
                .HasOne(s => s.User)
                .WithMany(g => g.Workspaces)
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<User>()
                .HasMany(s => s.Workspaces);

            modelBuilder.Entity<Transaction>()
                .HasOne(s => s.TransactionCategory)
                .WithMany(g => g.Transactions)
                .HasForeignKey(s => s.TransactionCategoryId);

            modelBuilder.Entity<TransactionCategory>()
                .HasMany(s => s.Transactions);

            modelBuilder.Entity<Transaction>()
                .HasOne(s => s.Workspace);
                
                
                
            base.OnModelCreating(modelBuilder);
        }
    }
}
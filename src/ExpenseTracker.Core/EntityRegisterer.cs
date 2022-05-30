using ExpenseTracker.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Core
{
    public static class EntityRegisterer
    {
        public static ModelBuilder AddCore(this ModelBuilder builder)
        {
            builder.Entity<Transaction>();
            builder.Entity<TransactionCategory>();
            builder.Entity<User>();
            builder.Entity<Workspace>();
            return builder;
        }
    }
}
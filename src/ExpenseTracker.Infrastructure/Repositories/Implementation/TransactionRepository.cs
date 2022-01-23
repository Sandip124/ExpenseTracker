using ExpenseTracker.Core.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Repositories.Implementation
{
    internal class TransactionRepository : GenericRepository<Core.Entities.Transaction>,ITransactionRepository
    {
        public TransactionRepository(DbContext context) : base(context)
        {
        }
    }
}

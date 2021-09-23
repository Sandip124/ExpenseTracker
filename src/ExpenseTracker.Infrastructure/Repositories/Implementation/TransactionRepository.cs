using ExpenseTracker.Core.Repositories.Interface;

namespace ExpenseTracker.Infrastructure.Repositories.Implementation
{
    public class TransactionRepository : GenericRepository<Core.Entities.Transaction>,ITransactionRepository
    {
    }
}

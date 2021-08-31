using ExpenseTracker.Core.Repositories.Interface;

namespace ExpenseTracker.Infrastructure.Repository.Implementation
{
    public class TransactionRepository : GenericRepository<Core.Entities.Transaction>,ITransactionRepository
    {
    }
}

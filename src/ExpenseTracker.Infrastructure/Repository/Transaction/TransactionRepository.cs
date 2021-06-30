using System.Threading.Tasks;
using ExpenseTracker.Core.Repositories.Transaction;

namespace ExpenseTracker.Infrastructure.Repository.Transaction
{
    public class TransactionRepository : ITransactionRepository
    {
        public async Task InsertAsync(Core.Entities.Transaction transaction)
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateAsync(Core.Entities.Transaction transaction)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteAsync(Core.Entities.Transaction transaction)
        {
            throw new System.NotImplementedException();
        }
    }
}
using ExpenseTracker.Common.Repositories.Interface;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Core.Repositories.Interface
{
   public interface ITransactionRepository: IGenericRepository<Transaction>
    {
    }
}

using ExpenseTracker.Common.Repositories.Interface;
using ExpenseTracker.Core.Entities;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Repositories.Interface
{
   public interface ITransactionRepository: IGenericRepository<Transaction>
    {
        Task<decimal> GetTotalTransactionAmountByWorkSpaceId(int workSpaceId);
    }
}

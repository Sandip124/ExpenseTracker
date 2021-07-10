
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Repositories.Interface
{
   public interface ITransactionRepository
    {
        Task InsertAsync(Entities.Transaction transaction);
        Task UpdateAsync(Entities.Transaction transaction);
        Task DeleteAsync(Entities.Transaction transaction);
    }
}

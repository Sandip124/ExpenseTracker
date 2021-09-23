using ExpenseTracker.Core.Dto.Transaction;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Services.Interface
{
   public interface ITransactionService
    {
        Task Create(TransactionCreateDto transactionCreateDto);
        Task Update(TransactionUpdateDto transactionUpdateDto);
        Task Delete(int transactionId);
    }
}

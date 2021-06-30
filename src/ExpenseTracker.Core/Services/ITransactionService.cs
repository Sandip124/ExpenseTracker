using System.Threading.Tasks;
using ExpenseTracker.Core.Dto.Transaction;

namespace ExpenseTracker.Core.Services
{
    public interface ITransactionService
    {
        Task Create(TransactionCreateDto transactionCreateDto);
        Task Update(TransactionUpdateDto transactionUpdateDto);
        Task Delete(long transactionId, string remarks);
    }
}
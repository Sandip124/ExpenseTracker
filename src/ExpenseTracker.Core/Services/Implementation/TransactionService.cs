using ExpenseTracker.Core.Dto.Transaction;
using ExpenseTracker.Core.Helper;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;
using System.Threading.Tasks;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Exceptions;

namespace ExpenseTracker.Core.Services.Implementation
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
            
        }
        public async Task Create(TransactionCreateDto transactionCreateDto)
        {
            using var Tx = TransactionScopeHelper.GetInstance();

            var transaction = Transaction.Create(transactionCreateDto.Amount, transactionCreateDto.TransactionDate,
                transactionCreateDto.Type);
            transaction.Description = transactionCreateDto.Description;

            await _transactionRepository.InsertAsync(transaction).ConfigureAwait(false);

            Tx.Complete();
        }

        public  async Task Delete(long transactionId)
        {
            var transactionExists = await _transactionRepository.CheckIfExistAsync(a=>a.Id == transactionId).ConfigureAwait(false);
            if (!transactionExists) throw new TransactionNotFoundException(transactionId);
            
            using var Tx = TransactionScopeHelper.GetInstance();
            
            var transaction = await _transactionRepository.GetByIdAsync(transactionId).ConfigureAwait(false) ?? throw new TransactionNotFoundException();

            await _transactionRepository.DeleteAsync(transaction).ConfigureAwait(false);

            Tx.Complete();
        }

        public async Task Update(TransactionUpdateDto transactionUpdateDto)
        {
            var transactionExists = await _transactionRepository.CheckIfExistAsync(a=>a.Id == transactionUpdateDto.Id).ConfigureAwait(false);
            if (!transactionExists) throw new TransactionNotFoundException(transactionUpdateDto.Id);
            
            using var Tx = TransactionScopeHelper.GetInstance();

            var transaction = await _transactionRepository.GetByIdAsync(transactionUpdateDto.Id).ConfigureAwait(false) ?? throw new TransactionNotFoundException();
            transaction.UpdateAmount(transactionUpdateDto.Amount);
            transaction.UpdateTransactionDate(transactionUpdateDto.TransactionDate);

            await _transactionRepository.UpdateAsync(transaction).ConfigureAwait(false);

            Tx.Complete();
        }
    }
}

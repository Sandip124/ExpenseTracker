using System.Threading.Tasks;
using ExpenseTracker.Core.Dto.TransactionCategory;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Exceptions;
using ExpenseTracker.Core.Helper;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;

namespace ExpenseTracker.Core.Services.Implementation
{
    public class TransactionCategoryService : ITransactionCategoryService
    {
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;

        public TransactionCategoryService(ITransactionCategoryRepository transactionCategoryRepository)
        {
            _transactionCategoryRepository = transactionCategoryRepository;
        }
        public async Task Create(TransactionCategoryCreateDto transactionCategoryCreateDto)
        {
            using var Tx = TransactionScopeHelper.GetInstance();

            var transaction = new TransactionCategory(transactionCategoryCreateDto.Type, transactionCategoryCreateDto.Color,
                transactionCategoryCreateDto.Icon);
            await _transactionCategoryRepository.InsertAsync(transaction).ConfigureAwait(false);

            Tx.Complete();
        }

        public async Task Update(TransactionCategoryUpdateDto transactionCategoryUpdateDto)
        {
            var transactionCategoryExists = await _transactionCategoryRepository.CheckIfExistAsync(a=>a.TransactionCategoryId == transactionCategoryUpdateDto.TransactionCategoryId).ConfigureAwait(false);
            if (!transactionCategoryExists) throw new TransactionCategoryNotFoundException(transactionCategoryUpdateDto.TransactionCategoryId);
            
            using var Tx = TransactionScopeHelper.GetInstance();

            var transaction = await _transactionCategoryRepository.GetByIdAsync(transactionCategoryUpdateDto.TransactionCategoryId).ConfigureAwait(false) ?? throw new TransactionCategoryNotFoundException();
            transaction.UpdateName(transactionCategoryUpdateDto.Name);
            transaction.UpdateColor(transactionCategoryUpdateDto.Color);
            transaction.UpdateIcon(transactionCategoryUpdateDto.Icon);

            await _transactionCategoryRepository.UpdateAsync(transaction).ConfigureAwait(false);

            Tx.Complete();
        }

        public async Task Delete(long transactionCategoryId)
        {
            var transactionCategoryExists = await _transactionCategoryRepository.CheckIfExistAsync(a=>a.TransactionCategoryId == transactionCategoryId).ConfigureAwait(false);
            if (!transactionCategoryExists) throw new TransactionCategoryNotFoundException(transactionCategoryId);
            
            using var Tx = TransactionScopeHelper.GetInstance();
            
            var transaction = await _transactionCategoryRepository.GetByIdAsync(transactionCategoryId).ConfigureAwait(false) ?? throw new TransactionCategoryNotFoundException();

            await _transactionCategoryRepository.DeleteAsync(transaction).ConfigureAwait(false);

            Tx.Complete();
        }
    }
}
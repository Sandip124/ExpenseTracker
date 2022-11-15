using System.Threading.Tasks;
using ExpenseTracker.Common.Helpers;
using ExpenseTracker.Core.Dto.TransactionCategory;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Exceptions;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;

namespace ExpenseTracker.Core.Services
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
            using var tx = TransactionScopeHelper.GetInstance();

            var transaction = TransactionCategory.Create(transactionCategoryCreateDto.Type,transactionCategoryCreateDto.Name, transactionCategoryCreateDto.Color,
                transactionCategoryCreateDto.Icon);
            await _transactionCategoryRepository.InsertAsync(transaction);
            await _transactionCategoryRepository.CommitAsync();
            tx.Complete();
        }

        public async Task Update(TransactionCategoryUpdateDto transactionCategoryUpdateDto)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var transaction = await _transactionCategoryRepository
                                  .FindAsync(transactionCategoryUpdateDto.TransactionCategoryId)
                                   ??
                              throw new TransactionCategoryNotFoundException();
            transaction.UpdateName(transactionCategoryUpdateDto.Name);
            transaction.UpdateColor(transactionCategoryUpdateDto.Color);
            transaction.UpdateIcon(transactionCategoryUpdateDto.Icon);
            transaction.Type = transactionCategoryUpdateDto.Type;

            await _transactionCategoryRepository.UpdateAsync(transaction);
            await _transactionCategoryRepository.CommitAsync();
            tx.Complete();
        }

        public async Task Delete(int transactionCategoryId)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            
            var transaction = await _transactionCategoryRepository.FindAsync(transactionCategoryId)
                                   ??
                              throw new TransactionCategoryNotFoundException();

            await _transactionCategoryRepository.DeleteAsync(transaction);
            await _transactionCategoryRepository.CommitAsync();
            tx.Complete();
        }
    }
}
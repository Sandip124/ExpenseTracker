using System.Threading.Tasks;
using ExpenseTracker.Common.DBAL;
using ExpenseTracker.Core.Dto.TransactionCategory;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Exceptions;
using ExpenseTracker.Common.Helpers;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;

namespace ExpenseTracker.Core.Services.Implementation
{
    public class TransactionCategoryService : ITransactionCategoryService
    {
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;
        private readonly IUnitofWork _unitOfWork;

        public TransactionCategoryService(ITransactionCategoryRepository transactionCategoryRepository,IUnitofWork unitOfWork)
        {
            _transactionCategoryRepository = transactionCategoryRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Create(TransactionCategoryCreateDto transactionCategoryCreateDto)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var transaction = TransactionCategory.Create(transactionCategoryCreateDto.Type,transactionCategoryCreateDto.Name, transactionCategoryCreateDto.Color,
                transactionCategoryCreateDto.Icon);
            await _transactionCategoryRepository.InsertAsync(transaction).ConfigureAwait(false);
            
            await _unitOfWork.CommitAsync();
            
            tx.Complete();
        }

        public async Task Update(TransactionCategoryUpdateDto transactionCategoryUpdateDto)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var transaction = await _transactionCategoryRepository.GetByIdAsync(transactionCategoryUpdateDto.TransactionCategoryId).ConfigureAwait(false) ?? throw new TransactionCategoryNotFoundException();
            transaction.UpdateName(transactionCategoryUpdateDto.Name);
            transaction.UpdateColor(transactionCategoryUpdateDto.Color);
            transaction.UpdateIcon(transactionCategoryUpdateDto.Icon);

            await _transactionCategoryRepository.UpdateAsync(transaction).ConfigureAwait(false);

            await _unitOfWork.CommitAsync();
            
            tx.Complete();
        }

        public async Task Delete(int transactionCategoryId)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            
            var transaction = await _transactionCategoryRepository.GetByIdAsync(transactionCategoryId).ConfigureAwait(false) ?? throw new TransactionCategoryNotFoundException();

            await _transactionCategoryRepository.DeleteAsync(transaction).ConfigureAwait(false);
            
            await _unitOfWork.CommitAsync();
            
            tx.Complete();
        }
    }
}
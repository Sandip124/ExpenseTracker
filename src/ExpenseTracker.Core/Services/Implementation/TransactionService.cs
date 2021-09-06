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
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;
        private readonly IWorkspaceRepository _workspaceRepository;

        public TransactionService(ITransactionRepository transactionRepository,ITransactionCategoryRepository transactionCategoryRepository,IWorkspaceRepository workspaceRepository)
        {
            _transactionRepository = transactionRepository;
            _transactionCategoryRepository = transactionCategoryRepository;
            _workspaceRepository = workspaceRepository;
        }
        public async Task Create(TransactionCreateDto transactionCreateDto)
        {
            using var Tx = TransactionScopeHelper.GetInstance();
            
            var workspace = await  _workspaceRepository.GetByIdAsync(transactionCreateDto.WorkspaceId).ConfigureAwait(false) ?? throw new TransactionCategoryNotFoundException();

            var transactionCategory = await _transactionCategoryRepository.GetByIdAsync(transactionCreateDto.TransactionCategoryId).ConfigureAwait(false) ?? throw new TransactionCategoryNotFoundException();

            var transaction = Transaction.Create(workspace,transactionCategory,transactionCreateDto.Amount, transactionCreateDto.TransactionDate,
                transactionCreateDto.Type);
            transaction.Description = transactionCreateDto.Description;

            await _transactionRepository.InsertAsync(transaction).ConfigureAwait(false);

            Tx.Complete();
        }

        public  async Task Delete(int transactionId)
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

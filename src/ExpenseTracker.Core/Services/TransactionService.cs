using System.Threading.Tasks;
using ExpenseTracker.Common.Helpers;
using ExpenseTracker.Core.Dto.Transaction;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Exceptions;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;

namespace ExpenseTracker.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;
        private readonly IWorkspaceRepository _workspaceRepository;

        public TransactionService(ITransactionRepository transactionRepository,
            ITransactionCategoryRepository transactionCategoryRepository,
            IWorkspaceRepository workspaceRepository)
        {
            _transactionRepository = transactionRepository;
            _transactionCategoryRepository = transactionCategoryRepository;
            _workspaceRepository = workspaceRepository;
        }
        public async Task Create(TransactionCreateDto transactionCreateDto)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            
            var workspace = await _workspaceRepository.GetByToken(transactionCreateDto.WorkspaceToken)
                                .ConfigureAwait(false) ??
                            throw new WorkspaceNotFoundException();

            var transactionCategory = await _transactionCategoryRepository
                                          .FindAsync(transactionCreateDto.TransactionCategoryId)
                                          .ConfigureAwait(false) ??
                                      throw new TransactionCategoryNotFoundException();

            var transaction = Transaction.Create(workspace,transactionCategory,transactionCreateDto.Amount, transactionCreateDto.TransactionDate,
                transactionCreateDto.Type);
            transaction.Description = transactionCreateDto.Description;

            await _transactionRepository.InsertAsync(transaction).ConfigureAwait(false);
            await _transactionRepository.CommitAsync().ConfigureAwait(false);
            
            tx.Complete();
        }

        public  async Task Delete(int transactionId)
        {
            var transactionExists = await _transactionRepository.CheckIfExistAsync(a=>a.Id == transactionId).ConfigureAwait(false);
            if (!transactionExists) throw new TransactionNotFoundException(transactionId);
            
            using var tx = TransactionScopeHelper.GetInstance();
            
            var transaction = await _transactionRepository.FindAsync(transactionId).ConfigureAwait(false) ?? throw new TransactionNotFoundException();

            await _transactionRepository.DeleteAsync(transaction).ConfigureAwait(false);
            await _transactionRepository.CommitAsync().ConfigureAwait(false);

            tx.Complete();
        }

        public async Task Update(TransactionUpdateDto transactionUpdateDto)
        {
            var transactionExists = await _transactionRepository.CheckIfExistAsync(a=>a.Id == transactionUpdateDto.Id).ConfigureAwait(false);
            if (!transactionExists) throw new TransactionNotFoundException(transactionUpdateDto.Id);
            
            using var tx = TransactionScopeHelper.GetInstance();

            var transaction = await _transactionRepository.FindAsync(transactionUpdateDto.Id).ConfigureAwait(false) ?? throw new TransactionNotFoundException();
            transaction.UpdateAmount(transactionUpdateDto.Amount);
            transaction.UpdateTransactionDate(transactionUpdateDto.TransactionDate);

            await _transactionRepository.UpdateAsync(transaction).ConfigureAwait(false);
            await _transactionRepository.CommitAsync().ConfigureAwait(false);

            tx.Complete();
        }
    }
}

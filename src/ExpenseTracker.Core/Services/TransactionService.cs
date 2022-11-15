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
            var transactionCategory = await _transactionCategoryRepository
                                          .FindAsync(transactionCreateDto.TransactionCategoryId)
                                      ??
                                      throw new TransactionCategoryNotFoundException();

            var transaction = Transaction.Create(transactionCreateDto.Workspace, transactionCategory,
                transactionCreateDto.Amount, transactionCreateDto.TransactionDate,
                transactionCreateDto.Type);
            transaction.Description = transactionCreateDto.Description;

            await _transactionRepository.InsertAsync(transaction);
            await _transactionRepository.CommitAsync();

            tx.Complete();
        }

        public async Task Delete(int transactionId)
        {
            var transactionExists = await _transactionRepository.CheckIfExistAsync(a => a.Id == transactionId);
            if (!transactionExists) throw new TransactionNotFoundException(transactionId);

            using var tx = TransactionScopeHelper.GetInstance();

            var transaction = await _transactionRepository.FindAsync(transactionId) ??
                              throw new TransactionNotFoundException();

            await _transactionRepository.DeleteAsync(transaction);
            await _transactionRepository.CommitAsync();

            tx.Complete();
        }

        public async Task Update(TransactionUpdateDto transactionUpdateDto)
        {
            var transactionExists =
                await _transactionRepository.CheckIfExistAsync(a => a.Id == transactionUpdateDto.Id);
            if (!transactionExists) throw new TransactionNotFoundException(transactionUpdateDto.Id);

            using var tx = TransactionScopeHelper.GetInstance();

            var transaction = await _transactionRepository.FindAsync(transactionUpdateDto.Id) ??
                              throw new TransactionNotFoundException();
            transaction.UpdateAmount(transactionUpdateDto.Amount);
            transaction.UpdateTransactionDate(transactionUpdateDto.TransactionDate);

            await _transactionRepository.UpdateAsync(transaction);
            await _transactionRepository.CommitAsync();

            tx.Complete();
        }
    }
}
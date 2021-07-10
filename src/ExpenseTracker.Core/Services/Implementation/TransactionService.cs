using ExpenseTracker.Core.Dto.Transaction;
using ExpenseTracker.Core.Helper;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;
using System;
using System.Threading.Tasks;

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


            Tx.Complete();
        }

        public  async Task Delete(long transactionId, string remarks)
        {
            using var Tx = TransactionScopeHelper.GetInstance();


            Tx.Complete();
        }

        public async Task Update(TransactionUpdateDto transactionUpdateDto)
        {
            using var Tx = TransactionScopeHelper.GetInstance();


            Tx.Complete();
        }
    }
}

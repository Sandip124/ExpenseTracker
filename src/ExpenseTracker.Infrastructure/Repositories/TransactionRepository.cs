using ExpenseTracker.Core.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Repositories
{
    internal class TransactionRepository : GenericRepository<Core.Entities.Transaction>,ITransactionRepository
    {
        public TransactionRepository(DbContext context) : base(context)
        {
        }

        public async Task<decimal> GetTotalTransactionAmountByWorkSpaceId(int workSpaceId)
        {
             var totalAmount = await GetPredicatedQueryable(t => t.WorkspaceId == workSpaceId &&t.Type == "Expense")
                .SumAsync(t => t.Amount);
            return totalAmount;
        }
    }
}

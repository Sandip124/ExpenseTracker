using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseTracker.Common.Repositories.Interface;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Core.Repositories.Interface
{
    public interface ITransactionCategoryRepository : IGenericRepository<TransactionCategory>
    {
        Task<IList<TransactionCategory>> GetByType(string type);
    }
}
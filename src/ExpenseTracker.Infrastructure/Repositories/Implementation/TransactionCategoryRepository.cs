using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Repositories.Interface;
using NHibernate.Linq;

namespace ExpenseTracker.Infrastructure.Repositories.Implementation
{
    public class TransactionCategoryRepository : GenericRepository<TransactionCategory>,ITransactionCategoryRepository
    {
        public async Task<IList<TransactionCategory>> GetByType(string type)
        {
            return await GetPredicatedQueryable(a => a.Type == type).ToListAsync();
        }
    }
}
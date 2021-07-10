using ExpenseTracker.Core.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Repository.Implementation
{
    public class TransactionRepository : ITransactionRepository
    {
        public async Task InsertAsync(Core.Entities.Transaction transaction)
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateAsync(Core.Entities.Transaction transaction)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteAsync(Core.Entities.Transaction transaction)
        {
            throw new System.NotImplementedException();
        }
    }
}

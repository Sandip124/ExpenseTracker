using ExpenseTracker.Core.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Core.Data;

namespace ExpenseTracker.Infrastructure.Repository.Implementation
{
    public class TransactionRepository : GenericRepository<Core.Entities.Transaction>,ITransactionRepository
    {
        public TransactionRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}

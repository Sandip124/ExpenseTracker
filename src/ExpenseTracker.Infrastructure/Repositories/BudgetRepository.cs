using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Repositories
{
    public class BudgetRepository : GenericRepository<Budget>, IBudgetRepository
    {
        public BudgetRepository(DbContext context) : base(context)
        {
        }

        public async Task<decimal> getBudgetByWorkSpackeId(int workSpaceId)
        {
            return await GetPredicatedQueryable(b => b.Workspace.Id == workSpaceId)
                .Select(b => b.Amount)
                .FirstOrDefaultAsync();
        }
    }
}
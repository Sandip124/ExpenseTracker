using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Repositories
{
    public class BudgetRepository : GenericRepository<Budget>,IBudgetRepository
    {
        protected BudgetRepository(DbContext context) : base(context)
        {
        }
    }
}
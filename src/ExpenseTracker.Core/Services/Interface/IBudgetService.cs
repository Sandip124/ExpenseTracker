using System;
using System.Threading.Tasks;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Core.Services.Interface
{
    public interface IBudgetService
    {
        Task Create(BudgetCreateDto dto);
        Task Delete(long budgetId);
    }

    public class BudgetCreateDto
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public virtual Workspace Workspace { get; set; }
        public User RecBy { get; set; }
    }
}
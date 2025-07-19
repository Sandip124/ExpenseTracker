using System;
using System.Threading.Tasks;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Core.Services.Interface
{
    public interface IBudgetService
    {
        Task Create(BudgetCreateDto dto);
        Task Delete(long budgetId);
        Task Update(BudgetUpdateDto dto);
    }

    public class BudgetCreateDto
    {
        public decimal Amount { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public virtual Workspace Workspace { get; set; }
        public User RecBy { get; set; }
        public string? Description { get; set; }
    }
    public class BudgetUpdateDto
    {
        public decimal Amount { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? Description { get; set; }
        public long Id { get; set; }
    }
}
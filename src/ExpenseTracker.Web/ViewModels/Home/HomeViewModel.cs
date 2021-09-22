using System.Collections.Generic;

namespace ExpenseTracker.Web.ViewModels.Home
{
    public class HomeViewModel
    {
        public decimal DailyExpenseAmount { get; set; }
        public decimal DailyIncomeAmount { get; set; }

        public decimal WeeklyExpenseAmount { get; set; }
        public decimal WeeklyIncomeAmount { get; set; }

        public decimal MonthlyExpenseAmount { get; set; }
        public decimal MonthlyIncomeAmount { get; set; }

        public IList<Core.Entities.Transaction> Transactions { get; set; } = new List<Core.Entities.Transaction>();

        public IList<TopCategory> TopExpendingCategories { get; set; } = new List<TopCategory>();
        
        public IList<Core.Entities.TransactionCategory> AllCategories { get; set; } = new List<Core.Entities.TransactionCategory>();
    }

    public class TopCategory
    {
        public string? CategoryName { get; set; }
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
    }
}
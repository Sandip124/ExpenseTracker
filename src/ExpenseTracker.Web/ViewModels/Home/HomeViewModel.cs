using System.Collections.Generic;

namespace ExpenseTracker.Web.ViewModels.Home
{
    public class HomeViewModel
    {
        public IList<Core.Entities.Transaction> Transactions { get; set; } = new List<Core.Entities.Transaction>();

        public IList<TopCategory> TopExpendingCategories { get; set; } = new List<TopCategory>();
        public IList<TransactionSummary> TransactionSummary { get; set; } = new List<TransactionSummary>();
        
        public IList<Core.Entities.TransactionCategory> AllCategories { get; set; } = new List<Core.Entities.TransactionCategory>();

        public decimal ExpensesToday { get; set; }
        public decimal ExpensesOfTheMonth { get; set; }
        public decimal IncomeToday { get; set; }
    }

    public class TopCategory
    {
        public string? CategoryName { get; set; }
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public string  Color { get; set; }
    }
    
    public class TransactionSummary
    {
        public string? Type { get; set; }
        public decimal Amount { get; set; }
        public string  Color { get; set; }
    }
}
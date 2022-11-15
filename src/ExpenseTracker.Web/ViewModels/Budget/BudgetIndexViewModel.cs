using System.Collections.Generic;

namespace ExpenseTracker.Web.ViewModels.Budget
{
    public class BudgetIndexViewModel
    {
        public IList<Core.Entities.Budget> Budgets { get; set; } = new List<Core.Entities.Budget>();
    }
}
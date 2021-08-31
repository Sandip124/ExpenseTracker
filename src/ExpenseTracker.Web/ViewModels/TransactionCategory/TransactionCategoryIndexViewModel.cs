using System.Collections.Generic;

namespace ExpenseTracker.Web.ViewModels.TransactionCategory
{
    public class TransactionCategoryIndexViewModel
    {
        public IList<Core.Entities.TransactionCategory> TransactionCategories { get; set; }
    }
}
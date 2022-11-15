using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Web.ViewModels.TransactionCategory
{
    public class TransactionCategoryIndexViewModel
    {
        public IOrderedEnumerable<Core.Entities.TransactionCategory> TransactionCategories { get; set; }
    }
}
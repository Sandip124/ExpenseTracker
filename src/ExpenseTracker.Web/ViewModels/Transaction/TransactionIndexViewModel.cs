using System.Collections.Generic;

namespace ExpenseTracker.Web.ViewModels.Transaction
{
    public class TransactionIndexViewModel
    {
        public IList<Core.Entities.Transaction> Transactions { get; set; } = new List<Core.Entities.Transaction>();
    }
}
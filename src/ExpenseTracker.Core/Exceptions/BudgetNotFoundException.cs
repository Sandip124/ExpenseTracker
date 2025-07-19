using System;

namespace ExpenseTracker.Core.Exceptions
{
    public class BudgetNotFoundException : Exception
    {
        public BudgetNotFoundException(string message = "Budget Not Found.") : base(message)
        {
        }
    }
}

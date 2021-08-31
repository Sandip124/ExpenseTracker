using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Core.Entities.Common
{
    public class TransactionType
    {
        public const string Income = "Income";
        public const string Expense = "Expense";
        
        public static readonly IReadOnlyList<string> ValidTypes = new List<string>
        {
            Income,
            Expense
        };
        public static bool IsValidType(string type)
        {
            return ValidTypes.Contains(type);
        }

    }
}
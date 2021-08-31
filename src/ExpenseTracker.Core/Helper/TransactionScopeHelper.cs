using System.Transactions;

namespace ExpenseTracker.Core.Helper
{
    internal static class TransactionScopeHelper
    {
        public static TransactionScope GetInstance()
        {
            return new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
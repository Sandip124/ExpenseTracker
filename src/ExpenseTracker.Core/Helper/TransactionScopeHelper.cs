using System.Transactions;

namespace ExpenseTracker.Core.Helper
{
    public static class TransactionScopeHelper
    {
        public static TransactionScope GetInstance()
        {
            return new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
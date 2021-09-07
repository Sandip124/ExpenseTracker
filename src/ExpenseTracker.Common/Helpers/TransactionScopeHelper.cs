using System.Transactions;

namespace ExpenseTracker.Common.Helpers
{
    public static class TransactionScopeHelper
    {
        public static TransactionScope GetInstance()
        {
            return new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
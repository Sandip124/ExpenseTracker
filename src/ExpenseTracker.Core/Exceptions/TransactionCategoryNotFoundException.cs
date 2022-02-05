using ExpenseTracker.Core.Exceptions.BaseException;

namespace ExpenseTracker.Core.Exceptions
{
    public class TransactionCategoryNotFoundException: ApplicationExceptionBase
    {
        public TransactionCategoryNotFoundException(): base("Transaction Category Not Found.")
        {
            
        }
        
        public TransactionCategoryNotFoundException(long transactionCategoryId,string message ="") : base(string.IsNullOrEmpty(message) ? $"Transaction Category with id : {transactionCategoryId} not found.": message)
        {
        }
    }
}
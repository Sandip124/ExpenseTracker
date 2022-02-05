using System;
using ExpenseTracker.Core.Exceptions.BaseException;

namespace ExpenseTracker.Core.Exceptions
{
    [Serializable]
    public class TransactionNotFoundException : ApplicationExceptionBase
    {
        public TransactionNotFoundException() : base("Transaction not found.")
        {
        }
        
        public TransactionNotFoundException(long transactionId,string message ="") : base(string.IsNullOrEmpty(message) ? $"Transaction with id : {transactionId} not found.": message)
        {
        }

    }
}
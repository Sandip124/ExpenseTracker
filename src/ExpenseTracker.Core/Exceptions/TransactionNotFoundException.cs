using System;
using System.Runtime.Serialization;

namespace ExpenseTracker.Core.Exceptions
{
    [Serializable]
    public class TransactionNotFoundException : ApplicationException
    {
        public TransactionNotFoundException() : base("Transaction not found.")
        {
        }
        
        public TransactionNotFoundException(long transactionId,string message ="") : base(string.IsNullOrEmpty(message) ? $"Transaction with id : {transactionId} not found.": message)
        {
        }

    }
}
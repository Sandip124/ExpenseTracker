using System;
using ExpenseTracker.Core.Exceptions.BaseException;

namespace ExpenseTracker.Core.Exceptions
{
    [Serializable]
    public class InvalidTransactionAmountException : ApplicationExceptionBase
    {
        public InvalidTransactionAmountException() : base("Invalid Transaction Amount.")
        {
        }

        public InvalidTransactionAmountException(string message) : base(message)
        {
        }
    }
}
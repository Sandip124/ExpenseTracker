using System;
using ExpenseTracker.Core.Exceptions.BaseException;

namespace ExpenseTracker.Core.Exceptions
{
    [Serializable]
    public class InvalidTransactionTypeException : ApplicationExceptionBase
    {
        public InvalidTransactionTypeException() : base("Invalid Transaction Type")
        {
        }

        public InvalidTransactionTypeException(string type, string message = "") : base(string.IsNullOrEmpty(message)? $"Invalid Transaction Type {type}": message)
        {
        }
    }
}
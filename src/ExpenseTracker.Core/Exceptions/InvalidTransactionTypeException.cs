using System;
using System.Runtime.Serialization;

namespace ExpenseTracker.Core.Exceptions
{
    [Serializable]
    public class InvalidTransactionTypeException : Exception
    {
        public InvalidTransactionTypeException() : base("Invalid Transaction Type")
        {
        }

        public InvalidTransactionTypeException(string type, string message = "") : base(string.IsNullOrEmpty(message)? $"Invalid Transaction Type {type}": message)
        {
        }

        public InvalidTransactionTypeException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidTransactionTypeException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
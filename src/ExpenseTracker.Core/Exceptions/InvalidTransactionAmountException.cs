using System;
using System.Runtime.Serialization;

namespace ExpenseTracker.Core.Exceptions
{
    [Serializable]
    public class InvalidTransactionAmountException : Exception
    {
        public InvalidTransactionAmountException() : base("Invalid Transaction Amount.")
        {
        }

        public InvalidTransactionAmountException(string message) : base(message)
        {
        }

        public InvalidTransactionAmountException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidTransactionAmountException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
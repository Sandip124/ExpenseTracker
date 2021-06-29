using System;
using ExpenseTracker.Core.Exceptions;

namespace ExpenseTracker.Core.Entities
{
    public class Transaction
    {
        public static Transaction CreateIncomeTransaction(decimal amount,DateTime transactionDate)
        {
            return new(amount,transactionDate)
            {
                Type = TransactionType.Income
            };
        }
        
        public static Transaction CreateExpensesTransaction(decimal amount,DateTime transactionDate)
        {
            return new(amount,transactionDate)
            {
                Type = TransactionType.Expenses
            };
        }

        private Transaction(decimal amount,DateTime transactionDate)
        {
            if (amount <= 0) throw new InvalidTransactionAmountException();
            Amount = amount;
            EntryDate = DateTime.Now;
            TransactionDate = transactionDate;
        }
        
        public int Id { get; protected set; }

        public decimal Amount { get; protected set; }
        
        public DateTime TransactionDate { get; protected set; }

        public DateTime EntryDate { get; protected set; }
        public string? Description { get; set; }
        
        public TransactionType Type { get; protected set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseTracker.Core.Exceptions;

namespace ExpenseTracker.Core.Entities
{
    public class Transaction
    {
        public static Transaction Create(decimal amount,DateTime transactionDate,string type)
        {
            return new(amount, transactionDate, type);
        }

        private Transaction(decimal amount,DateTime transactionDate,string type)
        {
            if (amount <= 0) throw new InvalidTransactionAmountException();
            Amount = amount;
            EntryDate = DateTime.Now;
            TransactionDate = transactionDate;
            if (!TransactionType.IsValidType(type)) throw new InvalidTransactionTypeException(type);
            Type = type;
        }

        public void UpdateAmount(decimal amount)
        {
            if (amount <= 0) throw new InvalidTransactionAmountException();
            Amount = amount;
        }

        public void UpdateTransactionDate(DateTime transactionDate)
        {
            TransactionDate = transactionDate;
        }
        
        public int Id { get; protected set; }

        public decimal Amount { get; protected set; }
        
        public DateTime TransactionDate { get; protected set; }

        public DateTime EntryDate { get; protected set; }
        public string? Description { get; set; }
        
        public string Type { get; protected set; }
    }
    
    public class TransactionType
    {
        public const string Income = "Income";
        public const string Expense = "Expense";
        
        public static readonly IReadOnlyList<string> ValidTypes = new List<string>
        {
            TransactionType.Income,
            TransactionType.Expense
        };
        public static bool IsValidType(string type)
        {
            return ValidTypes.Contains(type);
        }

    }
}
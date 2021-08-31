using System;
using ExpenseTracker.Core.Entities.Common;
using ExpenseTracker.Core.Exceptions;

namespace ExpenseTracker.Core.Entities
{
    public class Transaction
    {
        protected Transaction() { }

        public Transaction(Workspace workspace,TransactionCategory transactionCategory,decimal amount,DateTime transactionDate,string type)
        {
            SetWorkspace(workspace);
            SetTransactionCategory(transactionCategory);
            if (amount <= 0) throw new InvalidTransactionAmountException();
            Amount = amount;
            EntryDate = DateTime.Now;
            TransactionDate = transactionDate;
            if (!TransactionType.IsValidType(type)) throw new InvalidTransactionTypeException(type);
            Type = type;
        }
        
        public static Transaction Create(Workspace workspace,TransactionCategory transactionCategory,decimal amount,DateTime transactionDate,string type)
        {
            return new(workspace,transactionCategory,amount, transactionDate, type);
        }

        public virtual void UpdateAmount(decimal amount)
        {
            if (amount <= 0) throw new InvalidTransactionAmountException();
            Amount = amount;
        }

        public virtual void UpdateTransactionDate(DateTime transactionDate)
        {
            TransactionDate = transactionDate;
        }
        
        public virtual int Id { get; protected set; }

        public virtual decimal Amount { get; protected set; }
        
        public virtual DateTime TransactionDate { get; protected set; }

        public virtual DateTime EntryDate { get; protected set; }
        public virtual string? Description { get; set; }
        
        public virtual string Type { get; protected set; }
        
        public virtual int TransactionCategoryId { get; protected set; }
        public virtual TransactionCategory TransactionCategory { get; protected set; }

        protected virtual void SetTransactionCategory(TransactionCategory transactionCategory)
        {
            TransactionCategory = transactionCategory;
            TransactionCategoryId = transactionCategory.TransactionCategoryId;
        }
        
        
        public virtual int WorkspaceId { get; protected set; }
        public virtual Workspace Workspace { get; protected set; }

        protected virtual void SetWorkspace(Workspace workspace)
        {
            Workspace = workspace;
            WorkspaceId = workspace.WorkspaceId;
        }

    }
}
using System;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Core.Dto.Transaction
{
    public class TransactionCreateDto
    {
        public decimal Amount { get; protected set; }

        public DateTime TransactionDate { get; protected set; }

        public DateTime EntryDate { get; protected set; }
        public string? Description { get; set; }

        public TransactionType Type { get; protected set; }
    }
}
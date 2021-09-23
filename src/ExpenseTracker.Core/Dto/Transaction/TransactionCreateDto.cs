using System;

namespace ExpenseTracker.Core.Dto.Transaction
{
    public class TransactionCreateDto
    {
        public int TransactionCategoryId { get; set; }

        public string WorkspaceToken { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get;  set; }
        public DateTime TransactionDate { get;  set; }
        public string? Description { get; set; }
        public string Type { get;  set; }
    }
}
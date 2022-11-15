using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Core.Entities
{
    [Table("budget")]
    public class Budget
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public virtual Workspace Workspace { get; set; }
        public long WorkspaceId { get; set; }

        public string? Description { get; set; }

        public virtual User RecBy { get; set; }
        public long RecById { get; set; }
    }
}
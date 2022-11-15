using System;
using System.ComponentModel.DataAnnotations;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Web.ViewModels.Budget
{
    public class BudgetViewModel
    {
        [Display(Name = "From Date")] public DateTime FromDate { get; set; }

        [Display(Name = "To Date")] public DateTime ToDate { get; set; }

        [Display(Name = "Amount")] public decimal Amount { get; set; }

        public Core.Entities.Workspace Workspace { get; set; }

        [Display(Name = "Workspace")] public long WorkspaceId { get; set; }

        public User User { get; set; }

        [Display(Name = "User")] public long UserId { get; set; }
        public string? Description { get; set; }
    }
}
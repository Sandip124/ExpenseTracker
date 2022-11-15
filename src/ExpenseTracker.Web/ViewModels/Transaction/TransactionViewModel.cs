using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ExpenseTracker.Core.Entities.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpenseTracker.Web.ViewModels.Transaction
{
    public class TransactionViewModel
    {
        [Display(Name = "Entry Date")]
        public DateTime TransactionEntryDate { get; set; }
        [Display(Name = "Transaction Category")]
        public int TransactionCategoryId { get; set; }
        public Core.Entities.TransactionCategory? TransactionCategory { get; set; }
        public string Type { get; set; }
        public virtual int Id { get; set; }

        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        public SelectList TransactionTypes => new SelectList(TransactionType.ValidTypes);

        public IList<Core.Entities.TransactionCategory> TransactionCategories { get; set; } =
            new List<Core.Entities.TransactionCategory>();

        public SelectList TransactionCategoriesSelectList => new(TransactionCategories,"Id","CategoryName",TransactionCategoryId);
        
        public string? Description { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ExpenseTracker.Core.Entities.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpenseTracker.Web.ViewModels
{
    public class TransactionViewModel
    {
        public decimal TransactionAmount { get; set; }
        public DateTime TransactionEntryDate { get; set; }
        [Display(Name = "Transaction Category")]
        public int TransactionCategoryId { get; set; }
        public string Type { get; set; }

        public SelectList TransactionTypes => new SelectList(TransactionType.ValidTypes);

        public IList<Core.Entities.TransactionCategory> TransactionCategories { get; set; } =
            new List<Core.Entities.TransactionCategory>();

        public SelectList TransactionCategoriesSelectList => new SelectList(TransactionCategories,"TransactionCategoryId","CategoryName",TransactionCategoryId);
        
        public string? Description { get; set; }
    }
}
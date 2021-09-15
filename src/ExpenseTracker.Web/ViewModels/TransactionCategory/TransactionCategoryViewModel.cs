using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ExpenseTracker.Core.Entities.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpenseTracker.Web.ViewModels.TransactionCategory
{
    public class TransactionCategoryViewModel
    {
        public int TransactionCategoryId { get; set; }
        [Required]
        [MinLength(3)]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [Display(Name = "Color")]
        public string Color { get; set; }

        public readonly Dictionary<string, string> ColorList = Colors.GetColors;


        [Display(Name = "Icon")]
        public string Icon { get; set; }
        
        public IEnumerable<SelectListItem> IconSelectList => CategoryIcon.Icons.Select(a => new SelectListItem()
        {
            Text = $"{a.Value} {a.Key}",
            Value = a.Value,
            Selected = Icon == a.Value
        }).ToList();
        
        [Display(Name = "Type")]
        public string Type { get; set; }

        public static SelectList TypeSelectList => new SelectList(TransactionType.ValidTypes);

    }
}
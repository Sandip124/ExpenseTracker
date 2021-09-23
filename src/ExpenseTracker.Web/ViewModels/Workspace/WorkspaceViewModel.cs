using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ExpenseTracker.Core.Entities.Common;

namespace ExpenseTracker.Web.ViewModels.Workspace
{
    public class WorkspaceViewModel
    {
        [Display(Name = "Workspace Name")]
        [Required]
        [MinLength(3)]
        public string WorkspaceName { get; set; }

        [Display(Name="Color")]
        public string Color { get; set; }
        
        public readonly Dictionary<string, string> ColorList = Colors.GetColors;

        [Display(Name = "Description")]
        public string? Description { get; set; }
        
    }
}
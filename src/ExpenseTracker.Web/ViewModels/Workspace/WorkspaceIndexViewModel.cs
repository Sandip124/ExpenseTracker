using System.Collections.Generic;

namespace ExpenseTracker.Web.ViewModels.Workspace
{
    public class WorkspaceIndexViewModel
    {
        public string Name { get; set; }
        public IEnumerable<Core.Entities.Workspace> Workspaces { get; set; } = new List<Core.Entities.Workspace>();
    }
}
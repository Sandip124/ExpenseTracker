using System;

namespace ExpenseTracker.Core.Exceptions
{
    public class WorkspaceNotFoundException : ApplicationException
    {
        public WorkspaceNotFoundException() : base("Workspace not found.")
        {
        }
        
        public WorkspaceNotFoundException(long workspaceId,string message ="") : base(string.IsNullOrEmpty(message) ? $"Workspace with id : {workspaceId} not found.": message)
        {
        }
    }
}
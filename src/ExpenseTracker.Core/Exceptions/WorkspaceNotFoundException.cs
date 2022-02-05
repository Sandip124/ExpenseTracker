using System;
using ExpenseTracker.Core.Exceptions.BaseException;

namespace ExpenseTracker.Core.Exceptions
{
    public class WorkspaceNotFoundException : ApplicationExceptionBase
    {
        public WorkspaceNotFoundException() : base("Workspace not found.")
        {
        }
        
        public WorkspaceNotFoundException(long workspaceId,string message ="") : base(string.IsNullOrEmpty(message) ? $"Workspace with id : {workspaceId} not found.": message)
        {
        }
    }
}
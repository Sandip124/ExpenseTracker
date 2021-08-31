using System;

namespace ExpenseTracker.Core.Entities
{
    public class Workspace
    {
        protected Workspace() { }

        public static Workspace Create(string workspaceName,string color)
        {
            return new Workspace(workspaceName,color);
        }
        private Workspace(string workSpaceName,string color)
        {
            WorkSpaceName = workSpaceName;
            Color = color;
        }

        public virtual int WorkspaceId { get; protected set; }

        public virtual string Token { get; protected set; } = Guid.NewGuid().ToString();

        public virtual string WorkSpaceName { get; protected set; }
        
        public virtual string Color { get; set; }

        public virtual string? BackgroundImage { get; set; }

        public virtual string?  Description { get; set; }
        
        
        
    }
}
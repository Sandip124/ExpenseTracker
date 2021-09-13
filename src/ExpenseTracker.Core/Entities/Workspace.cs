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
            ChangeName(workSpaceName);
            ChangeColor(color);
        }

        public virtual int WorkspaceId { get; protected set; }

        public virtual string Token { get; protected set; } = Guid.NewGuid().ToString();

        public virtual string WorkSpaceName { get; protected set; }

        public virtual void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new Exception("Invalid Workspace name.");
            WorkSpaceName = name;
        }
        
        public virtual string Color { get; protected set; }

        public virtual void ChangeColor(string color)
        {
            if (string.IsNullOrWhiteSpace(color)) throw new Exception("Invalid Workspace color.");
            // todo more validation for color
            Color = color;
        }
        

        public virtual string? BackgroundImage { get; set; }

        public virtual string?  Description { get; set; }

        public virtual User User { get; protected set; }
        public virtual int UserId { get; protected set; }

        public virtual void AssignUser(User user)
        {
            User = user;
            UserId = user.UserId;
        }
        
        
        
    }
}
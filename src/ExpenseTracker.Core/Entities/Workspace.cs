using System;

namespace ExpenseTracker.Core.Entities
{
    public class Workspace
    {
        public const string TypeDefaultWorkspace = "DEFAULT_WORKSPACE";
        public const string TypeNormalWorkspace = "NORMAL_WORKSPACE";
        
        protected Workspace() { }

        public static Workspace Create(User user,string workspaceName,string color)
        {
            return new Workspace(user,workspaceName,color);
        }
        private Workspace(User user,string workSpaceName,string color)
        {
            ChangeName(workSpaceName);
            ChangeColor(color);
            AssignUser(user);
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

        public virtual string WorkspaceType { get; protected set; }

        public virtual void SetAsDefaultWorkspace() => WorkspaceType = TypeDefaultWorkspace;

        public virtual void SetAsNormalWorkspace() => WorkspaceType = TypeNormalWorkspace;

        public virtual void AssignUser(User user)
        {
            User.AddWorkspace(this);
            User = user;
            UserId = user.UserId;
        }
        
        
        
    }
}
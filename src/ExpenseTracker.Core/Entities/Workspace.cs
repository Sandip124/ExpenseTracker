using System;
using System.ComponentModel.DataAnnotations.Schema;
using ExpenseTracker.Core.Entities.Common;

namespace ExpenseTracker.Core.Entities
{
    [Table("workspace")]
    public class Workspace
    {
        protected Workspace() { }

        public static Workspace Create(WorkspaceType workspaceType,User user,string workspaceName,string color)
        {
            return new Workspace(user,workspaceName,color)
            {
                WorkspaceType = workspaceType
            };
        }
        private Workspace(User user,string workSpaceName,string color)
        {
            UpdateName(workSpaceName);
            UpdateColor(color);
            AssignUser(user);
        }

        public virtual int Id { get; protected set; }

        public virtual string Token { get; protected set; } = Guid.NewGuid().ToString();

        public virtual string WorkSpaceName { get; protected set; }

        public virtual void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new Exception("Invalid Workspace name.");
            WorkSpaceName = name;
        }
        
        public virtual string Color { get; protected set; }

        public virtual void UpdateColor(string color)
        {
            if (string.IsNullOrWhiteSpace(color)) throw new Exception("Invalid Workspace color.");
            // todo more validation for color
            Color = color;
        }
        public virtual string?  Description { get; set; }

        public virtual User User { get; protected set; }
        public virtual int UserId { get; protected set; }

        public virtual WorkspaceType WorkspaceType { get; protected set; }

        public virtual void SetDefault() => IsDefault = true;
        public virtual void RemoveDefault() => IsDefault = false;

        public virtual bool IsDefault { get; protected set; }

        public virtual void AssignUser(User user)
        {
            User = user;
            User.AddWorkspace(this);
        }
        
        
        }
}
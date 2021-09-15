using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;

namespace ExpenseTracker.Core.Entities
{
    public class User
    {
        protected  User(){}

        public static User Create(string userName, string password)
        {
            return new User(userName, password);
        }
        private User(string username, string password)
        {
            SetUserName(username);
            SetPassword(password);
        }
        
        public virtual int UserId { get; protected set; }
        public virtual string FirstName { get;  set; }
        public virtual string LastName { get;  set; }
        public virtual string Username { get; protected set; }
        
        public virtual void SetUserName(string userName)
        {
            //validation for the username
            if (string.IsNullOrWhiteSpace(userName))
            {
                //todo: change to custom exception
                throw new Exception("Username Not Valid.");
            }
            Username = userName;
        }

        [JsonIgnore]
        public virtual string Password { get; protected set; }

        public virtual void SetPassword(string password)
        {
            //validation for the password
            if (string.IsNullOrWhiteSpace(password))
            {
                //todo: change to custom exception
                throw new Exception("Username Not Valid.");
            }
            Password = password;
        }

        public virtual ICollection<Workspace> Workspaces { get; set; } = new List<Workspace>();

        public virtual bool HasDefaultWorkspace => Workspaces.Count(a => a.WorkspaceType == Workspace.TypeDefaultWorkspace) == 1;
        public virtual Workspace DefaultWorkspace =>
            (HasDefaultWorkspace
                ? Workspaces.FirstOrDefault(a => a.WorkspaceType == Workspace.TypeDefaultWorkspace)
                : throw new Exception("Default Workspace Not Found")) ??
            throw new Exception("Default Workspace Not Found");

        public virtual void AddWorkspace(Workspace workspace)
        {
            Workspaces.Add(workspace);
        }

    }
}
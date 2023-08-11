using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace ExpenseTracker.Core.Entities
{
    [Table("user")]
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
        
        public  int UserId { get; set; }
        public  string FirstName { get;  set; }
        public  string LastName { get;  set; }
        public  string Username { get; set; }
        public  string Email { get;  set; }
        public string? Image { get; set; }
        
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

        public virtual bool HasWorkspace => Workspaces.Any();

        public virtual bool HasDefaultWorkspace => Workspaces.Count(a => a.IsDefault) == 1;
        public virtual Workspace DefaultWorkspace =>
            (HasDefaultWorkspace
                ? Workspaces.FirstOrDefault(a => a.IsDefault)
                : throw new Exception("Default Workspace Not Found")) ??
            throw new Exception("Default Workspace Not Found");

        public virtual void AddWorkspace(Workspace workspace)
        {
            Workspaces.Add(workspace);
        }

    }
}
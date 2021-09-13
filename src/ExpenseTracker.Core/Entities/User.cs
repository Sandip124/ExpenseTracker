using System;
using System.Text.Json.Serialization;

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
    }
}
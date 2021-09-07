using System.Text.Json.Serialization;

namespace ExpenseTracker.Authentication.Entities
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
            SetUserName(Username);
            SetPassword(password);
        }
        
        public virtual int Id { get; protected set; }
        public virtual string FirstName { get;  set; }
        public virtual string LastName { get;  set; }
        public virtual string Username { get; protected set; }
        
        public virtual void SetUserName(string userName)
        {
            //validation for the username
            Username = userName;
        }

        [JsonIgnore]
        public virtual string Password { get; protected set; }

        public virtual void SetPassword(string password)
        {
            //validation for the password
            Password = password;
        }
        
    }
}
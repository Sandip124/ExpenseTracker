using ExpenseTracker.Authentication.Entities;

namespace ExpenseTracker.Authentication.Dto
{
    public class AuthenticateResponseDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public bool RememberMe { get; set; }


        public AuthenticateResponseDto(User user, string token)
        {
            Id = user.UserId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            Token = token;
        }
    }
}
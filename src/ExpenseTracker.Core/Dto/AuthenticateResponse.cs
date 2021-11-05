using User = ExpenseTracker.Core.Entities.User;

namespace ExpenseTracker.Core.Dto
{
    public class AuthenticateResponseDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; } = "/";


        public AuthenticateResponseDto(User user, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            Token = token;
        }
    }
}
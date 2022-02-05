
namespace ExpenseTracker.Contracts.Dto.Response
{
    public class AuthenticationResponseDto
    {
        public AuthenticationResponseDto(int userId,string username, string token)
        {
            Id = userId;
            Username = username;
            Token = token;
        }

        public int Id { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string Username { get; init; }

        public string Token { get; init; }

        public bool RememberMe { get; init; }

        public string? ReturnUrl { get; set; } = "/";
    }
}
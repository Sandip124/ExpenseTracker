using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Contracts.Dto.Request
{
    public class AuthenticationRequestDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
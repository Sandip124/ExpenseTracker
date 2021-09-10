using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Authentication.Dto
{
    public class AuthenticateRequestDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
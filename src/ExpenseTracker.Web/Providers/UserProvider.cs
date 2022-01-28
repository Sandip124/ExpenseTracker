using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Web.Providers.Interface;
using Microsoft.AspNetCore.Http;

namespace ExpenseTracker.Web.Providers
{
    public class UserProvider : IUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public UserProvider(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public async Task<User> GetCurrentUser()
        {
            return await _userRepository.FindAsync(GetCurrentUserId()) ?? throw new Exception("User Not Found.");
        }

        public int GetCurrentUserId()
            => Convert.ToInt32(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
}
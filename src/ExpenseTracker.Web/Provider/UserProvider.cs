using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Repositories.Interface;
using Microsoft.AspNetCore.Http;

namespace ExpenseTracker.Web.Provider
{
    public class UserProvider : IUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public UserProvider(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            this._userRepository = userRepository;
        }

        public async Task<User> GetCurrentUser()
        {
            return await _userRepository.GetByIdAsync(GetCurrentUserId()) ?? throw new Exception("User Not Found.");
        }

        public int GetCurrentUserId()
            => Convert.ToInt32(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
}
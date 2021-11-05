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
        private readonly IUserRepository userRepo;

        public UserProvider(IHttpContextAccessor httpContextAccessor, IUserRepository userRepo)
        {
            _httpContextAccessor = httpContextAccessor;
            this.userRepo = userRepo;
        }

        public Task<User> GetCurrentUser()
        {
            return userRepo.GetByIdAsync(GetCurrentUserId());
        }

        public int GetCurrentUserId()
        {
            return Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

    }
}
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Infrastructure.Extensions
{
    public static class CurrentUserExtension
    {
        public static async Task<User> GetCurrentUser(this ControllerBase controller)
        {
            var userId = Convert.ToInt32(controller.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            using var serviceScope = ServiceActivator.GetScope();
            IUserRepository userRepository = serviceScope.ServiceProvider.GetService<IUserRepository>();
            return await userRepository.GetByIdAsync(userId).ConfigureAwait(true) ?? throw new Exception("User Not Found.");
        }
    }
}
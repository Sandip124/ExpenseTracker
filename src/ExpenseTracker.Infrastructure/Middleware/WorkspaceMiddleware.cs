using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Infrastructure.Middleware
{
    public class WorkspaceMiddleware
    {
        private const string WorkspaceCreateUrl = "/Workspace/Create";
        private const string LoginUrl = "/Account/Login";
        

        private static readonly List<string> PathsToAvoid = new()
        {
            LoginUrl,
            WorkspaceCreateUrl
        };
        
        private readonly RequestDelegate _next;
        public WorkspaceMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            var userId = Convert.ToInt32(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            using var serviceScope = ServiceActivator.GetScope();
            IUserRepository _userRepository = serviceScope.ServiceProvider.GetService<IUserRepository>();
            
            var currentRequestPath = httpContext.Request.Path;

            var currentUser = await _userRepository.GetByIdAsync(userId).ConfigureAwait(true);

            if ( currentUser != null && !currentUser.Workspaces.Any() && !currentUser.Workspaces.Any(a => a.IsDefault) && !PathsToAvoid.Contains(currentRequestPath))
            {
                httpContext.Response.Redirect(WorkspaceCreateUrl);
                return;
            }
            
            await _next(httpContext);
        }
        
    }
    
    public static class WorkspaceCheckMiddleware
    {
        public static IApplicationBuilder UseWorkspaceMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<WorkspaceMiddleware>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Infrastructure.Middleware;
using ExpenseTracker.Web.Provider;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

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
        
        private readonly Options _options;
        private readonly RequestDelegate _next;
        public WorkspaceMiddleware(RequestDelegate next,IOptions<Options> options)
        {
            _next = next;
            _options = options.Value;
        }
        public async Task Invoke(HttpContext httpContext,IUserProvider userProvider,
            IWorkspaceRepository workspaceRepository)
        {

            var currentRequestPath = httpContext.Request.Path;

            var currentUserId = userProvider.GetCurrentUserId();

            if ( currentUserId > 0 && IgnorePath(currentRequestPath,_options))
            {
                var hasDefaultWorkspace = await workspaceRepository.HasDefaultWorkspace(currentUserId);
                if (!hasDefaultWorkspace)
                {
                    httpContext.Response.Redirect(WorkspaceCreateUrl);
                }
                else
                {
                    httpContext.Response.Redirect(LoginUrl);
                }
            }

            await _next.Invoke(httpContext);
            
        }
        
        private static bool IgnorePath(PathString path, Options options)
        {
            return options.IgnorePatterns.Any(ignorePattern => Regex.IsMatch(path, ignorePattern, RegexOptions.Compiled | RegexOptions.IgnoreCase));
        }

        public class Options
        {
            public string[] IgnorePatterns { get; set; } = {
                LoginUrl,
                WorkspaceCreateUrl
            };
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
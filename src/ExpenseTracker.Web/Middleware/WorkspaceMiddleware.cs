using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Web.Providers.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace ExpenseTracker.Web.Middleware
{
    public class WorkspaceMiddleware
    {
        private const string WorkspaceCreateUrl = "/Workspace/Create";
        private const string LoginUrl = "/Account/Login";

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

            if ( currentUserId <= 0 && IgnorePath(currentRequestPath,_options))
            {
                var hasDefaultWorkspace = await workspaceRepository.HasDefaultWorkspace(currentUserId);
                httpContext.Response.Redirect(!hasDefaultWorkspace ? WorkspaceCreateUrl : LoginUrl);
            }

            await _next.Invoke(httpContext);
            
        }
        
        private static bool IgnorePath(PathString path, Options options)
        {
            return options.IgnorePatterns.Any(ignorePattern => Regex.IsMatch(path,
                ignorePattern,
                RegexOptions.Compiled | RegexOptions.IgnoreCase));
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
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;

namespace ExpenseTracker.Infrastructure.Extensions
{
    public static class WorkspaceSessionExtension
    {
        private const string DefaultWorkspaceSessionKey = "DEFAULT_WORKSPACE";
        public static void SetDefaultWorkspace(this ISession session,string defaultWorkspaceToken)
        {
            session.SetString(DefaultWorkspaceSessionKey, defaultWorkspaceToken);
        }
        
        public static string GetDefaultWorkspace(this ISession session)
        {
            return session.GetString(DefaultWorkspaceSessionKey);
        }
    }
}
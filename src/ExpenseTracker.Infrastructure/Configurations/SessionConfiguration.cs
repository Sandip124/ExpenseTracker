using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Infrastructure.Configurations
{
    public static class SessionConfiguration
    {
        public static IServiceCollection AddSessionConfiguration(this IServiceCollection services)
        {
            services.AddSession(
                options =>
                {
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SameSite = SameSiteMode.Strict;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.IdleTimeout = TimeSpan.FromMinutes(3);
                }
            );
            
            return services;
        }
    }
}
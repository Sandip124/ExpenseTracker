using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Infrastructure.Configurations
{
    public static class CookiePolicyConfiguration
    {
        public static IServiceCollection AddCookiePolicyConfiguration(this IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = _ => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                }
            );
            return services;
        }
    }
}
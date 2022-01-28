using ExpenseTracker.Core;
using ExpenseTracker.Infrastructure;
using ExpenseTracker.Web.Providers;
using ExpenseTracker.Web.Providers.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Web
{
    public static class DiConfig
    {
        public static IServiceCollection UseExpenseTracker(this IServiceCollection services)
        {
            services.InjectCoreServices();
            services.InjectServices();
            services.InjectRepositories();
            services.AddScoped<IUserProvider, UserProvider>();
            return services;
        }
        
    }
}
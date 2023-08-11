using ExpenseTracker.Core;
using ExpenseTracker.Infrastructure;
using ExpenseTracker.Web.Providers;
using ExpenseTracker.Web.Providers.Interface;
using ExpenseTracker.Web.Services;
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
            services.AddTransient<IUserProvider, UserProvider>();
            services.AddTransient<ISeedingService, SeedingService>();
            return services;
        }
    }
}
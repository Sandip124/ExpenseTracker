using ExpenseTracker.Web.Provider;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Web
{
    public static class DiConfig
    {
        public static IServiceCollection UseExpenseTracker(this IServiceCollection services)
        {
            services.AddScoped<IUserProvider, UserProvider>();
            
            return services;
        }
        
    }
}
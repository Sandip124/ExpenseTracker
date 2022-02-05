using ExpenseTracker.Core.Logging;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;
using ExpenseTracker.Infrastructure.Logging;
using ExpenseTracker.Infrastructure.Repositories;
using ExpenseTracker.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Infrastructure
{
    public static class DiConfig
    {
        public static IServiceCollection InjectRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITransactionCategoryRepository, TransactionCategoryRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            services.AddScoped(typeof(IApplicationLogger<>), typeof(LoggerAdapter<>));
            return services;
        }
        
        public static IServiceCollection InjectServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
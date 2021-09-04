using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Infrastructure.Repository.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Infrastructure
{
    public static class DiConfig
    {
        public static void InjectRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITransactionCategoryRepository, TransactionCategoryRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
        }
    }
}
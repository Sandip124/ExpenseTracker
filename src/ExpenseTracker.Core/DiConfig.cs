using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Implementation;
using ExpenseTracker.Core.Services.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Core
{
    public static class DiConfig
    {
        public static void InjectServices(this IServiceCollection services)
        {
            services.AddScoped<ITransactionCategoryService, TransactionCategoryService>();
            services.AddScoped<ITransactionService, TransactionService>();
        }

    }
}
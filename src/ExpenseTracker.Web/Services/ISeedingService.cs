using System.Threading.Tasks;
using System.Transactions;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Web.Services
{
    public interface ISeedingService
    {
        Task<bool> InitialSeed(DbContext context);
    }

    public class SeedingService : ISeedingService
    {
        public async Task<bool> InitialSeed(DbContext context)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var user = User.Create("admin@gmail.com",
                "AQAAAAEAACcQAAAAEOR5yS1MkTeRgn/WtSjyGJdfnsYszdnztyhrTzwQAI7q1beh4kiKwsDv3aQ44oLyPA==");
            user.UserId = -1;
            user.FirstName = "Admin";
            user.LastName = "Admin";
            user.Email = "admin@gmail.com";
            user.Username = "admin@gmail.com";

            await context.Set<User>().AddAsync(user);

            var defaultWorkspace = Workspace.Create(WorkspaceType.Personal, user, "Personal Workspace", "#00ff0000");
            defaultWorkspace.SetDefault();
            await context.Set<Workspace>().AddAsync(defaultWorkspace);

            user.AddWorkspace(defaultWorkspace);
        
            await context.SaveChangesAsync();
            scope.Complete();
            return true;
        }
    }
}
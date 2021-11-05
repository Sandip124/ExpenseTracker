using System.Threading.Tasks;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Repositories.Implementation
{
    public class WorkspaceRepository : GenericRepository<Workspace>, IWorkspaceRepository
    {
        public WorkspaceRepository(DbContext context) : base(context)
        {
        }

        public async Task<Workspace> GetDefaultWorkspace()
        {
            return await GetPredicatedQueryable(a => a.WorkspaceType == Workspace.TypeDefaultWorkspace)
                .SingleOrDefaultAsync();
        }

        public async Task<Workspace> GetByToken(string token)
        {
            return await GetPredicatedQueryable(a => a.Token == token)
                .SingleOrDefaultAsync();
        }
    }
}
using System.Threading.Tasks;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Repositories.Interface;
using NHibernate.Linq;

namespace ExpenseTracker.Infrastructure.Repositories.Implementation
{
    public class WorkspaceRepository : GenericRepository<Workspace>, IWorkspaceRepository
    {
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
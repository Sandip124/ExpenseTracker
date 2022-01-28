using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Repositories
{
    internal class WorkspaceRepository : GenericRepository<Workspace>, IWorkspaceRepository
    {
        public WorkspaceRepository(DbContext context) : base(context)
        {
        }

        public async Task<Workspace> GetDefaultWorkspace()
        {
            return await GetPredicatedQueryable(a => a.IsDefault == true)
                .SingleOrDefaultAsync();
        }

        public async Task<Workspace> GetByToken(string token)
        {
            return await GetPredicatedQueryable(a => a.Token == token)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Workspace>> GetActiveWorkspaces(int userId)
        {
            return await GetAllAsync(x => x.UserId == userId);
        }

        public async Task<bool> HasDefaultWorkspace(int userId)
            => await CheckIfExistAsync(w =>
                w.IsDefault &&
                w.UserId == userId);
    }
}
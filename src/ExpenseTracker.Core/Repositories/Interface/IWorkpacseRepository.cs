using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseTracker.Common.Repositories.Interface;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Core.Repositories.Interface
{
    public interface IWorkspaceRepository : IGenericRepository<Workspace>
    {
        Task<Workspace> GetDefaultWorkspace();
        Task<Workspace> GetByToken(string token);
        Task<IEnumerable<Workspace>> GetActiveWorkspaces(int userId);
        Task<bool> HasDefaultWorkspace(int userId);
    }
}
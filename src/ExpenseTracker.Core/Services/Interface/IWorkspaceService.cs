using System.Threading.Tasks;
using ExpenseTracker.Core.Dto.Workspace;

namespace ExpenseTracker.Core.Services.Interface
{
    public interface IWorkspaceService
    {
        Task Create(WorkspaceCreateDto workspaceCreateDto);
        Task Update(WorkspaceUpdateDto workspaceUpdateDto);
        Task Delete(int workspaceId);
        Task ChangeDefault(string workspaceToken);
    }
}
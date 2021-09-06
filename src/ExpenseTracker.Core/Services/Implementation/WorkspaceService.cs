using System;
using System.Threading.Tasks;
using ExpenseTracker.Core.Dto.Workspace;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Exceptions;
using ExpenseTracker.Core.Helper;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;

namespace ExpenseTracker.Core.Services.Implementation
{
    public class WorkspaceService : IWorkspaceService
    {
        private readonly IWorkspaceRepository _workspaceRepository;

        public WorkspaceService(IWorkspaceRepository workspaceRepository)
        {
            _workspaceRepository = workspaceRepository;
        }
        public async Task Create(WorkspaceCreateDto workspaceCreateDto)
        {
            using var Tx = TransactionScopeHelper.GetInstance();
            
            var workspace = Workspace.Create(workspaceCreateDto.Name,workspaceCreateDto.Color);
            workspace.BackgroundImage = workspaceCreateDto.BackgroundImage;

            await _workspaceRepository.InsertAsync(workspace).ConfigureAwait(false);

            Tx.Complete();
        }

        public async Task Update(WorkspaceUpdateDto workspaceUpdateDto)
        {
            using var Tx = TransactionScopeHelper.GetInstance();

            var workspace = await _workspaceRepository.GetByIdAsync(workspaceUpdateDto.WorkspaceId)
                                .ConfigureAwait(false) ??
                            throw new WorkspaceNotFoundException();
                
            workspace.ChangeName(workspaceUpdateDto.Name);
            workspace.ChangeColor(workspaceUpdateDto.Color);
            workspace.BackgroundImage = workspaceUpdateDto.BackgroundImage;
            workspace.Description = workspaceUpdateDto.Description;

            await _workspaceRepository.UpdateAsync(workspace).ConfigureAwait(false);

            Tx.Complete();
        }

        public async Task Delete(int workspaceId)
        {
            using var Tx = TransactionScopeHelper.GetInstance();

            var workspace = await _workspaceRepository.GetByIdAsync(workspaceId).ConfigureAwait(false) ??
                            throw new WorkspaceNotFoundException();

            await _workspaceRepository.DeleteAsync(workspace).ConfigureAwait(false);
            
            Tx.Complete();
        }
    }
}
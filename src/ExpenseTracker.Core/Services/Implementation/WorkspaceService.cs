using System;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Common.Helpers;
using ExpenseTracker.Core.Dto.Workspace;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Exceptions;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;

namespace ExpenseTracker.Core.Services.Implementation
{
    public class WorkspaceService : IWorkspaceService
    {
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly IUserRepository _userRepository;

        public WorkspaceService(IWorkspaceRepository workspaceRepository,IUserRepository userRepository)
        {
            _workspaceRepository = workspaceRepository;
            _userRepository = userRepository;
        }
        public async Task Create(WorkspaceCreateDto workspaceCreateDto)
        {
            using var Tx = TransactionScopeHelper.GetInstance();

            var userWorkspaces =  _workspaceRepository
                .GetPredicatedQueryable(a => a.UserId == workspaceCreateDto.UserId).ToList();

            var user = await _userRepository.GetByIdAsync(workspaceCreateDto.UserId).ConfigureAwait(false) ?? throw new Exception("User not found exception");
            
            var workspace = Workspace.Create(user,workspaceCreateDto.Name,workspaceCreateDto.Color);
            
            if (userWorkspaces.Any())
            {
                workspace.SetAsNormalWorkspace();
            }else{
                workspace.SetAsDefaultWorkspace();
            }
            
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
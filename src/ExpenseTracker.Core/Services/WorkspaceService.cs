using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Common.Helpers;
using ExpenseTracker.Core.Dto.Workspace;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Exceptions;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;

namespace ExpenseTracker.Core.Services
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
            using var tx = TransactionScopeHelper.GetInstance();
            
            var user = await _userRepository.FindAsync(workspaceCreateDto.UserId).ConfigureAwait(false) ?? throw new Exception("User not found exception");
            
            var workspace = Workspace.Create(workspaceCreateDto.WorkspaceType,user,workspaceCreateDto.Name,workspaceCreateDto.Color);
            
            if (!user.HasWorkspace) workspace.SetDefault();

            await _workspaceRepository.InsertAsync(workspace).ConfigureAwait(false);

            tx.Complete();
        }

        public async Task Update(WorkspaceUpdateDto workspaceUpdateDto)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var workspace = await _workspaceRepository.FindAsync(workspaceUpdateDto.WorkspaceId)
                                .ConfigureAwait(false) ??
                            throw new WorkspaceNotFoundException();
                
            workspace.UpdateName(workspaceUpdateDto.Name);
            workspace.UpdateColor(workspaceUpdateDto.Color);
            workspace.Description = workspaceUpdateDto.Description;

            await _workspaceRepository.UpdateAsync(workspace).ConfigureAwait(false);
            tx.Complete();
        }

        public async Task Delete(int workspaceId)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var workspace = await _workspaceRepository.FindAsync(workspaceId).ConfigureAwait(false) ??
                            throw new WorkspaceNotFoundException();

            await _workspaceRepository.DeleteAsync(workspace).ConfigureAwait(false);
            tx.Complete();
        }

        public async Task ChangeDefault(string workspaceToken)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var selectedWorkspace = await _workspaceRepository.GetByToken(workspaceToken).ConfigureAwait(false) ??
                            throw new WorkspaceNotFoundException();
            
            var userWorkspaces =  _workspaceRepository
                .GetPredicatedQueryable(a => a.UserId == selectedWorkspace.UserId).ToList();

            foreach (var workspace in userWorkspaces.Except(new List<Workspace> {selectedWorkspace}))
            {
                workspace.RemoveDefault();
            }
            selectedWorkspace.SetDefault();
            tx.Complete();
        }
    }
}
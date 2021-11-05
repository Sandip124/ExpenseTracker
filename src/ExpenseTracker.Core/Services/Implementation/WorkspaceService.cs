using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Common.DBAL;
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
        private readonly IUow _uow;

        public WorkspaceService(IWorkspaceRepository workspaceRepository,IUserRepository userRepository, IUow uow)
        {
            _workspaceRepository = workspaceRepository;
            _userRepository = userRepository;
            _uow = uow;
        }
        public async Task Create(WorkspaceCreateDto workspaceCreateDto)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            
            var user = await _userRepository.GetByIdAsync(workspaceCreateDto.UserId).ConfigureAwait(false) ?? throw new Exception("User not found exception");
            
            var workspace = Workspace.Create(user,workspaceCreateDto.Name,workspaceCreateDto.Color);
            
            if (user.HasWorkspace && user.Workspaces.Count > 1)
            {
                workspace.SetAsNormalWorkspace();
            }else{
                workspace.SetAsDefaultWorkspace();
            }
            
            await _workspaceRepository.InsertAsync(workspace).ConfigureAwait(false);
            await _uow.CommitAsync();
            tx.Complete();
        }

        public async Task Update(WorkspaceUpdateDto workspaceUpdateDto)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var workspace = await _workspaceRepository.GetByIdAsync(workspaceUpdateDto.WorkspaceId)
                                .ConfigureAwait(false) ??
                            throw new WorkspaceNotFoundException();
                
            workspace.ChangeName(workspaceUpdateDto.Name);
            workspace.ChangeColor(workspaceUpdateDto.Color);
            workspace.Description = workspaceUpdateDto.Description;

            _workspaceRepository.Update(workspace);

            tx.Complete();
        }

        public async Task Delete(int workspaceId)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var workspace = await _workspaceRepository.GetByIdAsync(workspaceId).ConfigureAwait(false) ??
                            throw new WorkspaceNotFoundException();

            _workspaceRepository.Delete(workspace);
            
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
                workspace.SetAsNormalWorkspace();
            }
            selectedWorkspace.SetAsDefaultWorkspace();
            
            tx.Complete();
        }
    }
}
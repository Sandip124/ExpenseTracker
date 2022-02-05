using System;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using ExpenseTracker.Core.Dto.Workspace;
using ExpenseTracker.Core.Entities.Common;
using ExpenseTracker.Infrastructure.Extensions;
using ExpenseTracker.Web.Providers.Interface;
using ExpenseTracker.Web.ViewModels.Workspace;
using Microsoft.AspNetCore.Mvc;
using IWorkspaceService = ExpenseTracker.Core.Services.Interface.IWorkspaceService;

namespace ExpenseTracker.Web.Controllers
{
    
    public class OnBoardingController: Controller
    {
        private readonly IUserProvider _userProvider;
        private readonly IWorkspaceService _workspaceService;
        private readonly INotyfService _notifyService;

        public OnBoardingController(IUserProvider userProvider,IWorkspaceService workspaceService,INotyfService notifyService)
        {
            _userProvider = userProvider;
            _workspaceService = workspaceService;
            _notifyService = notifyService;
        }

        public IActionResult Workspace()
        {
            var workspaceViewModel = new WorkspaceViewModel();
            return View(workspaceViewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Workspace(WorkspaceViewModel workspaceViewModel)
        {
            try
            {
                if (!ModelState.IsValid) return View(workspaceViewModel);

                var currentUser = await _userProvider.GetCurrentUser();
                var workspaceDto = new WorkspaceCreateDto()
                {
                    UserId = currentUser.UserId,
                    Color = workspaceViewModel.Color,
                    Name = workspaceViewModel.WorkspaceName,
                    Description = workspaceViewModel.Description,
                    WorkspaceType = WorkspaceType.Personal
                };
                
                await _workspaceService.Create(workspaceDto);
                
                HttpContext?.Session.SetDefaultWorkspace(currentUser.DefaultWorkspace.Token);

                _notifyService.Success("Workspace Created Successfully.");
            }
            catch (Exception e)
            {
                _notifyService.Error(e.Message);
            }
            
            return RedirectToAction(nameof(Index),"Home");
        }
    }
}
using System;
using System.Threading.Tasks;
using ExpenseTracker.Core.Dto.Workspace;
using ExpenseTracker.Core.Entities.Common;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;
using ExpenseTracker.Infrastructure.Extensions;
using ExpenseTracker.Web.Provider;
using ExpenseTracker.Web.ViewModels.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers
{
    public class WorkspaceController : Controller
    {
        private readonly IWorkspaceService _workspaceService;
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly IUserProvider _userProvider;

        public WorkspaceController(IWorkspaceService workspaceService,IWorkspaceRepository workspaceRepository,IUserProvider userProvider)
        {
            _workspaceService = workspaceService;
            _workspaceRepository = workspaceRepository;
            _userProvider = userProvider;
        }
        // GET
        public async Task<IActionResult> Index(string name)
        {
            var currentUser = await _userProvider.GetCurrentUser();
            var workspaceViewModel = new WorkspaceIndexViewModel
            {
                Workspaces = await _workspaceRepository.GetActiveWorkspaces(currentUser.UserId).ConfigureAwait(true)
            };
            return View(workspaceViewModel);
        }

        public IActionResult Create()
        {
            var workspaceViewModel = new WorkspaceViewModel();
            return View(workspaceViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(WorkspaceViewModel workspaceViewModel)
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
                
                await _workspaceService.Create(workspaceDto).ConfigureAwait(true);
                
                HttpContext?.Session.SetDefaultWorkspace(currentUser.DefaultWorkspace.Token);

                this.AddSuccessMessage("Workspace Created Successfully.");
            }
            catch (Exception e)
            {
                this.AddErrorMessage(e.Message);
            }
            
            return RedirectToAction(nameof(Index),"Home");
        }

        public async Task<IActionResult> ChangeDefault(string workspaceToken,string redirectUrl="/")
        {
            try
            {
                await _workspaceService.ChangeDefault(workspaceToken).ConfigureAwait(true);
            
                this.AddSuccessMessage("Workspace Changed Successfully");
            }
            catch (Exception e)
            {
                this.AddErrorMessage(e.Message);
            }

            return LocalRedirectPreserveMethod(redirectUrl);
        }
        
    }
}
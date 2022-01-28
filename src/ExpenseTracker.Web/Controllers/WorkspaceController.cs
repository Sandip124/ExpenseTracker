using System;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using ExpenseTracker.Core.Dto.Workspace;
using ExpenseTracker.Core.Entities.Common;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;
using ExpenseTracker.Infrastructure.Extensions;
using ExpenseTracker.Web.Providers.Interface;
using ExpenseTracker.Web.ViewModels.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers
{
    public class WorkspaceController : Controller
    {
        private readonly IWorkspaceService _workspaceService;
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly IUserProvider _userProvider;
        private readonly INotyfService _notifyService;

        public WorkspaceController(IWorkspaceService workspaceService,
            IWorkspaceRepository workspaceRepository,
            IUserProvider userProvider,INotyfService notifyService)
        {
            _workspaceService = workspaceService;
            _workspaceRepository = workspaceRepository;
            _userProvider = userProvider;
            _notifyService = notifyService;
        }
        // GET
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userProvider.GetCurrentUser();
            var workspaceViewModel = new WorkspaceIndexViewModel
            {
                Workspaces = await _workspaceRepository.GetActiveWorkspaces(currentUser.UserId)
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
                
                await _workspaceService.Create(workspaceDto);
                
                HttpContext?.Session.SetDefaultWorkspace(currentUser.DefaultWorkspace.Token);

                _notifyService.Success("Workspace Created Successfully.");
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
                await _workspaceService.ChangeDefault(workspaceToken);
            
                _notifyService.Success("Workspace Changed Successfully");
            }
            catch (Exception e)
            {
                this.AddErrorMessage(e.Message);
            }

            return LocalRedirectPreserveMethod(redirectUrl);
        }
        
    }
}
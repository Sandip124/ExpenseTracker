using System;
using System.Threading.Tasks;
using ExpenseTracker.Core.Dto.Workspace;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;
using ExpenseTracker.Infrastructure.Extensions;
using ExpenseTracker.Infrastructure.SessionFactory;
using ExpenseTracker.Web.ViewModels.Workspace;
using Microsoft.AspNetCore.Mvc;
using NHibernate;

namespace ExpenseTracker.Web.Controllers
{
    public class WorkspaceController : Controller
    {
        private readonly IWorkspaceService _workspaceService;
        private readonly IWorkspaceRepository _workspaceRepository;

        public WorkspaceController(IWorkspaceService workspaceService,IWorkspaceRepository workspaceRepository)
        {
            _workspaceService = workspaceService;
            _workspaceRepository = workspaceRepository;
        }
        // GET
        public async Task<IActionResult> Index(string name)
        {
            var workspaceViewModel = new WorkspaceIndexViewModel
            {
                Workspaces = await _workspaceRepository.GetAllAsync().ConfigureAwait(true)
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

                var currentUser = await this.GetCurrentUser().ConfigureAwait(true);
                var workspaceDto = new WorkspaceCreateDto()
                {
                    UserId = currentUser.UserId,
                    Color = workspaceViewModel.Color,
                    Name = workspaceViewModel.WorkspaceName,
                    Description = workspaceViewModel.Description
                };
                
                await _workspaceService.Create(workspaceDto).ConfigureAwait(true);

                BaseSessionFactory.HttpContextAccessor?.HttpContext?.Session.SetDefaultWorkspace(currentUser.DefaultWorkspace.Token);
                
                var defaultWorkspace = BaseSessionFactory.HttpContextAccessor?.HttpContext?.Session.GetDefaultWorkspace();
                
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
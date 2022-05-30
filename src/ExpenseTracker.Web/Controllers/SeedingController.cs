using System;
using System.Threading.Tasks;
using ExpenseTracker.Core.Dto;
using ExpenseTracker.Core.Dto.Workspace;
using ExpenseTracker.Core.Entities.Common;
using ExpenseTracker.Core.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers
{
    public class SeedingController : Controller
    {
        private readonly IUserService _userService;
        private readonly IWorkspaceService _workspaceService;

        public SeedingController(IUserService userService,IWorkspaceService workspaceService)
        {
            _userService = userService;
            _workspaceService = workspaceService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> SeedUser()
        {
            try
            {
                var dto = new UserDto()
                {
                    UserName = "admin",
                    Password = "Admin@123",
                    FirstName = "Sandip",
                    LastName = "Chaudhary",
                };
                await _userService.CreateUser(dto);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        
        [AllowAnonymous]
        public async Task<IActionResult> SeedWorkspace()
        {
            try
            {
                  var dto = new WorkspaceCreateDto()
                    {
                        Name = "Default",
                        Description = "Default Workspace",
                        UserId = 1,
                        Color="#ffffff",
                        WorkspaceType = WorkspaceType.Personal
                    };
                await _workspaceService.Create(dto);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ExpenseTracker.Authentication.Dto;
using ExpenseTracker.Authentication.Services.Interface;
using ExpenseTracker.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ExpenseTracker.Web.Controllers.Authentication
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AccountController(IUserService userService,IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
        
        public IActionResult Login(string returnUrl = "/")
        {
            var authenticationResponseDto = new AuthenticateRequestDto
            {
                ReturnUrl = returnUrl
            };

            return View(authenticationResponseDto);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] AuthenticateRequestDto authenticateRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Login credentials didn't match.");
                }
                var response = _userService.Authenticate(authenticateRequestDto);

                if (response == null)
                    //return View(authenticateRequestDto);  
                    return BadRequest(new { message = "Username or password is incorrect" });

                await SetClaimsAndSignInUser(response).ConfigureAwait(true);

                return this.Ok(new
                {
                    AccessToken = response.Token,
                    Id = response.Id,
                    FirstName = response.FirstName,
                    LastName = response.LastName,
                    UserName = response.Username,
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        private async Task SetClaimsAndSignInUser(AuthenticateResponseDto Result)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, Result.Id.ToString()),
                new Claim(ClaimTypes.Name, Result.Username.ToString()),
            };

            var UserIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var Principal = new ClaimsPrincipal(UserIdentity);
            var Prop = new AuthenticationProperties { IsPersistent = Result.RememberMe };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, Principal, Prop).ConfigureAwait(true);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirectPreserveMethod("/");
        }

    }
}
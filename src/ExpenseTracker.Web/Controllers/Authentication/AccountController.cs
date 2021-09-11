using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ExpenseTracker.Authentication.Dto;
using ExpenseTracker.Authentication.Services.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers.Authentication
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        
        public IActionResult Login(string returnUrl = "/")
        {
            var authenticationResponseDto = new AuthenticateRequestDto
            {
                ReturnUrl = returnUrl
            };

            return View(authenticationResponseDto);
        }

        /// <summary>
        /// LoginIn to system
        /// </summary>
        /// <param name="authenticateRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthenticateRequestDto authenticateRequestDto)
        {
            try
            {
                var response = _userService.Authenticate(authenticateRequestDto);

                if (response == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                await SetClaimsAndSignInUser(response).ConfigureAwait(true);

                return this.Ok(new
                {
                    AccessToken = response.Token,
                    Id = response.Id,
                    FirstName = response.FirstName,
                    LastName = response.LastName,
                    UserName = response.Username,
                    ReturnUrl = response.ReturnUrl
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        private async Task SetClaimsAndSignInUser(AuthenticateResponseDto result)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, result.Id.ToString()),
                new(ClaimTypes.Name, result.Username)
            };

            var UserIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var Principal = new ClaimsPrincipal(UserIdentity);
            var Prop = new AuthenticationProperties { IsPersistent = result.RememberMe };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, Principal, Prop).ConfigureAwait(true);
        }
        
        /// <summary>
        /// Logout From  The  Application
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirectPreserveMethod("/");
        }

    }
}
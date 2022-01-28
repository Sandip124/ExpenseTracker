using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ExpenseTracker.Core.Dto;
using ExpenseTracker.Core.Services.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers.Api
{
    [ApiController]
    [AllowAnonymous]
    [Route("[Controller]")]
    public class AuthenticationController : ControllerBase
    {
        
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }
        // GET
        /// <summary>
        /// LoginIn to system
        /// </summary>
        /// <param name="authenticateRequestDto"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> JwtLogin([FromBody] AuthenticateRequestDto authenticateRequestDto)
        {
            try
            {
                var response = _userService.Authenticate(authenticateRequestDto);

                await SetClaimsAndSignInUser(response);

                    return Ok(new
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

            var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(userIdentity);
            var prop = new AuthenticationProperties { IsPersistent = result.RememberMe };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, prop);
        }
    }
}
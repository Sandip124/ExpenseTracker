using System.Security.Claims;
using ExpenseTracker.Contracts.Dto.Request;
using ExpenseTracker.Core.Dto;
using ExpenseTracker.Core.Logging;
using ExpenseTracker.Core.Services.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.WebApi.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("/api/authentication")]
    public class AuthenticationController : ApiControllerBase
    {
        
        private readonly IUserService _userService;
        private readonly IApplicationLogger<AuthenticationController> _logger;

        public AuthenticationController(IUserService userService,IApplicationLogger<AuthenticationController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        // GET
        /// <summary>
        /// LoginIn to system
        /// </summary>
        /// <param name="authenticationRequestDto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> JwtLogin([FromBody] AuthenticationRequestDto authenticationRequestDto)
        {
            try
            {
                var response = _userService.Authenticate(authenticationRequestDto);

                var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, response.Id.ToString()),
                    new(ClaimTypes.Name, response.Username)
                };

                var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(userIdentity);
                var prop = new AuthenticationProperties { IsPersistent = response.RememberMe };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, prop);

                return Ok(response);

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message,e);
                return BadRequest(e.Message);
            }
        }
    }
}
using System.Threading.Tasks;
using ExpenseTracker.Authentication.Dto;
using ExpenseTracker.Authentication.Repositories.Interface;
using ExpenseTracker.Authentication.Services.Interface;
using ExpenseTracker.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers.Authentication
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public UsersController(IUserService userService,IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequestDto model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAllAsync().ConfigureAwait(true);
            return Ok(users);
        }
    }
}
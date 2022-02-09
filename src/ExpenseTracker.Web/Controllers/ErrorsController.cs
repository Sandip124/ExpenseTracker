using System.Diagnostics;
using ExpenseTracker.Core.Logging;
using ExpenseTracker.Web.Models;
using ExpenseTracker.Web.Providers.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers
{
    public class ErrorsController : Controller
    {
        private readonly IUserProvider _userProvider;
        private readonly IApplicationLogger<ErrorsController> _logger;

        public ErrorsController(IUserProvider userProvider,IApplicationLogger<ErrorsController> logger)
        {
            _userProvider = userProvider;
            _logger = logger;
        }
        // GET
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            var httpContext = HttpContext;
            var requestId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
            var statusCode = httpContext.Response.StatusCode;
            _logger.LogError("Error occured on a page {Path} {StatusCode}{@User}",httpContext.Request.Path,statusCode,_userProvider.GetCurrentUser());
            return View(new ErrorViewModel
            {
                RequestId = requestId,
                StatusCode = statusCode,
            });
        }
    }
}
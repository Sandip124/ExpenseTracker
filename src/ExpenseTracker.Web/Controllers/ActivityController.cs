using ExpenseTracker.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers
{
    [Authorize]
    public class ActivityController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}
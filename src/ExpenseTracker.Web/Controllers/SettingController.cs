using ExpenseTracker.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers
{
    [Authorize]
    public class SettingController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}
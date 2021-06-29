using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers
{
    public class SettingController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}
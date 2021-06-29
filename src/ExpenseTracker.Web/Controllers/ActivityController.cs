using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers
{
    public class ActivityController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}
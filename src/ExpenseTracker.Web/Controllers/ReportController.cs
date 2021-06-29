using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers
{
    public class ReportController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}
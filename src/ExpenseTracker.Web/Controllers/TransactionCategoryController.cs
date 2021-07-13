using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers
{
    public class TransactionCategoryController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}
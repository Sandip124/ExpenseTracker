using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers
{
    public class TransactionController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
        
        
    }
}
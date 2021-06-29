using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers
{
    public class ContactsController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}
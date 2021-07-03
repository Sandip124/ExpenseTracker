using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Infrastructure.ViewComponents
{
    [ViewComponent(Name = "Notify")]
    public class NotifyViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
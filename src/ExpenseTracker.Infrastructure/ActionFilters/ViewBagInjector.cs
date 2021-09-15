using System.Threading.Tasks;
using ExpenseTracker.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExpenseTracker.Infrastructure.ActionFilters
{
    public class ViewBagInjector : IAsyncActionFilter
    {
        private const string LoginPath = "/Account/Login";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var c = context.Controller;
            var userId = (c as ControllerBase).GetCurrentUserId();

            var containsApiControllerAttribute = c.GetType().GetCustomAttributes(typeof(ApiControllerAttribute), true).Length > 0;
            var extendsControllerBase = c.GetType().BaseType == typeof(ControllerBase);
            var isNotLoginPage = (c as ControllerBase).Request.Path != LoginPath;

            if (userId != 0 && !containsApiControllerAttribute && !extendsControllerBase && isNotLoginPage)
            {
                var user = (c as ControllerBase).GetCurrentUser();
                (c as Controller).ViewBag.__CurrentUser = await user.ConfigureAwait(true);
            }

            await next().ConfigureAwait(true);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ExpenseTracker.Infrastructure.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult SendModelValidationErrors(this Controller controller, ModelStateDictionary modelState)
        {
            return controller.BadRequest(new
            {
                error = new
                {
                    message = "Model validation failed",
                    validationErrors = controller.SerializedValidationErrors(modelState),
                }
            });
        }

        public static Dictionary<string, string[]> SerializedValidationErrors(this Controller controller,ModelStateDictionary modelState)
        {
            var modelErrors = modelState.Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(
                    x => x.Key,
                    x => x.Value.Errors.Select(y => y.ErrorMessage).ToArray()
                );
            return modelErrors;
        }
        
    }

}
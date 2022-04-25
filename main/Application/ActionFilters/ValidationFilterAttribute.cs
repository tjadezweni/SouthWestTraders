using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.ActionFilters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }
            context.Result = new UnprocessableEntityObjectResult(context.ModelState);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}

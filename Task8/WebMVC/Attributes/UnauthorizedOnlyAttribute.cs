using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebMVC.Attributes;

public class UnauthorizedOnlyAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.User.Identity == null ||
            context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new RedirectResult("/");
        }

        base.OnActionExecuting(context);
    }
}

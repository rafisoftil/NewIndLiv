using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IndiaLivings_Web_UI.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                // Redirect to the login page if the user is not authenticated
                context.Result = new RedirectToActionResult("Login", "Dashboard", null);
            }
            base.OnActionExecuting(context);
        }
    }
}

namespace Smart_Home_Automation_System_MVC.Attributes
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class RedirectToLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Retrieve the token from the cookie
            string token = filterContext.HttpContext.Request.Cookies["token"];

            // If the token is null or empty, redirect to the login page
            if (string.IsNullOrEmpty(token))
            {
                filterContext.Result = new RedirectToActionResult("Login", "Auth", null);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}

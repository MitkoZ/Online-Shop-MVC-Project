using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShopMVC.Helpers
{
    public class CustomAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        public bool AllowAccessToUser { get; set; }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any()
                || filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any())
            {
                return;
            }

            if (LoginUserSession.Current.IsAuthenticated == false)
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(new { controller = "Home", action = "Login" }));
            }
            else if (AllowAccessToUser == false && LoginUserSession.Current.isAdmin == false)
            {
                filterContext.Result = new ViewResult { ViewName = "AccessDenied" };
            }
        }
    }

}
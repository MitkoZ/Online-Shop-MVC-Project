using OnlineShopMVC.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShopMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            CustomAuthorizeAttribute authorizeAttribute = new CustomAuthorizeAttribute();
            authorizeAttribute.AllowAccessToUser = true;
            GlobalFilters.Filters.Add(authorizeAttribute);
            GlobalFilters.Filters.Add(new CustomErrorHandlerAttribute());
        }
    }
}

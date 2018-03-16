using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace resourceEdge.webUi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
             name: "Default1",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "Selfservice", action = "Leave", id = UrlParameter.Optional }
         );
         

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Selfservice", action = "Leave", id = UrlParameter.Optional }
            );
        }
    }
}

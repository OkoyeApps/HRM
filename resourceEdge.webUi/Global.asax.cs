using Ninject.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using System.Data.Entity;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Concrete;
using System.Net;
using System.IO;
using Microsoft.AspNet.Identity;
using resourceEdge.webUi.Infrastructure;
using resourceEdge.webUi.Infrastructure.Handlers;

namespace resourceEdge.webUi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //string action = "";
        //string controller = "";
        //string parameter = "";
        //string hostName = "";
        //string ipaddress = "";
        //string requesturl = "";
        //string dataparameter = String.Empty;
        //IActivityLog LogsRepo = new ActivityLogRepo();
        //string userId = "No user Yet";
        //ControllerContext context = new ControllerContext();
        public void Application_BeginRequest(object sender, EventArgs args)
        {
            LoggingFilter Logs = new LoggingFilter();
            //Logs.Logging();
        }
        protected void Application_Start()
        {
            //Database.SetInitializer(new resourceEdge.webUi.Infrastructure.DbInitializer());
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        
    }
}

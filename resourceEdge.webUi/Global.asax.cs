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

namespace resourceEdge.webUi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        string action = "";
        string controller = "";
        string parameter = "";
        string hostName = "";
        string ipaddress = "";
        string requesturl = "";
        string dataparameter = String.Empty;
        IActivityLog LogsRepo = new ActivityLogRepo();
        string userId = "No user Yet";
        ControllerContext context = new ControllerContext();
       // public void Application_BeginRequest(object sender, EventArgs args)
       //{
          
       //     if (Context.User != null &&Context.User.Identity.IsAuthenticated)
       //     {
       //         userId = User.Identity.GetUserId();
       //     }
       //     Logging();
       // }

        //private void Logging()
        //{
        //    List<ActivityLogs> activityLogs = new List<ActivityLogs>();
        //    LoggingEvents logEvent = new LoggingEvents(LogsRepo);
        //    HttpContextBase currentCurrent = new HttpContextWrapper(HttpContext.Current);
        //    UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
        //    RouteData routeDate = urlHelper.RouteCollection.GetRouteData(currentCurrent);
        //    action = routeDate.Values["action"].ToString();
        //    controller = routeDate.Values["controller"].ToString();
        //    parameter = routeDate.Values["id"].ToString();
        //    hostName = Dns.GetHostName();
        //    ipaddress = Dns.GetHostByName(hostName).AddressList[0].ToString();
        //    HttpContext.Current.Request.InputStream.Position = 0;
        //    using (StreamReader inputStream = new StreamReader(HttpContext.Current.Request.InputStream))
        //    {
        //        dataparameter = inputStream.ReadToEnd();
        //    }
        //    requesturl = Request.Url.AbsoluteUri;
        //    activityLogs.Add(new ActivityLogs()
        //    {
        //        actionname = action,
        //        controllername = controller,
        //        dataparameter = dataparameter,
        //        myip = ipaddress,
        //        parameters = parameter,
        //        requesturl = requesturl,
        //        UserId = userId
        //    });
            
        //    logEvent.InsertActivityLogs(activityLogs);
        //}
        protected void Application_Start()
        {
            
            Database.SetInitializer(new resourceEdge.webUi.Infrastructure.DbInitializer());
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        
    }
}

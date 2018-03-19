using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Concrete;
using resourceEdge.Domain.Entities;
using resourceEdge.webUi.Infrastructure.ActitivityLogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace resourceEdge.webUi.Infrastructure.Handlers
{
    public class LoggingFilter : System.Web.HttpApplication
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
        private void Logging()
        {
            List<ActivityLog> activityLogs = new List<ActivityLog>();
            LoggingEvents logEvent = new LoggingEvents(LogsRepo);
            HttpContextBase currentCurrent = new HttpContextWrapper(HttpContext.Current);
            UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            RouteData routeDate = urlHelper.RouteCollection.GetRouteData(currentCurrent);
            action = routeDate.Values["action"].ToString();
            controller = routeDate.Values["controller"].ToString();
            parameter = routeDate.Values["id"].ToString();
            hostName = Dns.GetHostName();
            ipaddress = Dns.GetHostByName(hostName).AddressList[0].ToString();
            HttpContext.Current.Request.InputStream.Position = 0;
            using (StreamReader inputStream = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                dataparameter = inputStream.ReadToEnd();
            }
            requesturl = Request.Url.AbsoluteUri;
            activityLogs.Add(new ActivityLog()
            {
                actionname = action,
                controllername = controller,
                dataparameter = dataparameter,
                myip = ipaddress,
                parameters = parameter,
                requesturl = requesturl,
                UserId = userId
            });

            logEvent.InsertActivityLogs(activityLogs);
        }
    }
}
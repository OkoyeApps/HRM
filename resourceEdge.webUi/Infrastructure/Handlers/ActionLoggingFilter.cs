using Microsoft.AspNet.Identity;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Concrete;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;
using resourceEdge.webUi.Infrastructure.ActitivityLogs;
using resourceEdge.webUi.Models;
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
    public class ActionLoggingFilter : FilterAttribute, IActionFilter
    {
        string action = "";
        string controller = "";
        string parameter = "";
        string hostName = "";
        string ipaddress = "";
        string requesturl = "";
        string userId = "No user Yet";
        string userFullName = "No user Yet";
        SessionModel sessionObject;
        IActivityLog LogsRepo = new ActivityLogRepo();
        public void Logging(HttpSessionStateBase Session, RequestContext Request)
        {
            List<ActivityLog> activityLogs = new List<ActivityLog>();
            LoggingEvents logEvent = new LoggingEvents(LogsRepo);
            //HttpContextBase currentContext = new HttpContextWrapper(HttpContext.Current);
            //UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            //RouteData routeDate = urlHelper.RouteCollection.GetRouteData(currentContext); 
            //var allkeys = routeDate.Values.Keys;
            //action =(string) routeDate.Values["action"] ?? null;
            //controller =(string) routeDate.Values["controller"] ?? null;
            //if (routeDate.Values.ContainsKey("id"))
            //{
            //parameter =routeDate.Values["id"].ToString() ?? null;
            //}
            hostName = Dns.GetHostName();
            ipaddress = Dns.GetHostEntry(hostName).AddressList[0].ToString();
            HttpContext.Current.Request.InputStream.Position = 0;
            if (Session != null)
            {
               sessionObject = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
               requesturl = Request.HttpContext.Request.Url.AbsoluteUri;
                if (sessionObject != null)
                {
                   userId = Request.HttpContext.User.Identity.GetUserId();
                   userFullName = sessionObject.FullName;
                }
            }
            //using (StreamReader inputStream = new StreamReader(HttpContext.Current.Request.InputStream))
            //{
            //    dataparameter = inputStream.ReadToEnd();
            //}
            activityLogs.Add(new ActivityLog()
            {
                actionname = action,
                controllername = controller,
                myip = ipaddress,
                parameters = parameter,
                requesturl = requesturl,
                UserId = userId,
                UserName = userFullName,
                HttpMethod =Request.HttpContext.Request.HttpMethod

        });
           var Ip =  HttpContext.Current.Request.UserHostAddress;
            logEvent.InsertActivityLogs(activityLogs);          

        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
             
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {
                if (filterContext.Controller.ControllerContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    controller = filterContext.RouteData.Values["controller"].ToString();
                    action = filterContext.RouteData.Values["action"].ToString();
                   
                    if (filterContext.RouteData.Values.ContainsKey("id")) parameter = filterContext.RouteData.Values["id"].ToString();
                    var Request = filterContext.RequestContext;
                    var Session = filterContext.HttpContext.Session;
                    if (!string.IsNullOrEmpty(controller) && !string.IsNullOrEmpty(action))
                    {
                        Logging(Session, Request);
                    }
                }
            }
            catch(Exception ex)
            {
                //throw ex;
            }
        }
    }
}
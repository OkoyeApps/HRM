using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Infrastructure.Handlers
{
    public class TempdateInterceptorFilter : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var ErrortempData = filterContext.Controller.TempData["App.Notifications.Error"];
            var SuccesstempData = filterContext.Controller.TempData["App.Notifications.Success"];
            var WarningtempData = filterContext.Controller.TempData["App.Notifications.Warning"];
            var InfotempData = filterContext.Controller.TempData["App.Notifications.Info"];
            if (ErrortempData != null)
            {
                if (!string.IsNullOrEmpty(ErrortempData.ToString()))
                {
                    ICollection<string> temDataCollection = new List<string>();
                    ICollection<string> newCollectionErrorData = new List<string>();
                    foreach (var item in ErrortempData as ICollection<String>)
                    {
                        temDataCollection.Add(item);
                    }
                    foreach (var item in temDataCollection)
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.Append(item);
                        builder.Append($"|{filterContext.HttpContext.Request.Url.AbsolutePath}");
                       
                        newCollectionErrorData.Add(builder.ToString());
                        
                    }
                    filterContext.Controller.TempData.Remove("App.Notifications.Error");
                    filterContext.Controller.TempData["App.Notifications.Error"] = newCollectionErrorData;
                }
            }
            if (SuccesstempData != null)
            {
                if (!string.IsNullOrEmpty(SuccesstempData.ToString()))
                {
                    ICollection<string> temDataCollection = new List<string>();
                    ICollection<string> newCollectionofSuccessData = new List<string>();
                    foreach ( var item in SuccesstempData as ICollection<String>)
                    {
                        temDataCollection.Add(item);
                    }
                    foreach (var item in temDataCollection)
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.Append(item);
                        builder.Append($"|{filterContext.HttpContext.Request.Url.AbsolutePath}");
                        newCollectionofSuccessData.Add(builder.ToString());
                    }
                    filterContext.Controller.TempData.Remove("App.Notifications.Success");
                    filterContext.Controller.TempData["App.Notifications.Success"] = newCollectionofSuccessData;
                }
            }
            if (WarningtempData != null)
            {
                if (!string.IsNullOrEmpty(WarningtempData.ToString()))
                {
                    ICollection<string> temDataCollection = new List<string>();
                    ICollection<string> newCollectionofWarningData = new List<string>();
                    foreach (var item in WarningtempData as ICollection<String>)
                    {
                        temDataCollection.Add(item);
                    }
                    foreach (var item in temDataCollection)
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.Append(item);
                        builder.Append($"|{filterContext.HttpContext.Request.Url.AbsolutePath}");
                        newCollectionofWarningData.Add(builder.ToString());
                    }
                    filterContext.Controller.TempData.Remove("App.Notifications.Warning");
                    filterContext.Controller.TempData["App.Notifications.Warning"] = newCollectionofWarningData;
                }
            }
            if (InfotempData != null)
            {
                if (!string.IsNullOrEmpty(InfotempData.ToString()))
                {
                    ICollection<string> temDataCollection = new List<string>();
                    ICollection<string> newCollectionofInfoData = new List<string>();
                    foreach (var item in InfotempData as ICollection<String>)
                    {
                        temDataCollection.Add(item);
                    }
                    foreach (var item in temDataCollection)
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.Append(item);
                        builder.Append($"|{filterContext.HttpContext.Request.Url.AbsolutePath}");
                        newCollectionofInfoData.Add(builder.ToString());
                    }
                    filterContext.Controller.TempData.Remove("App.Notifications.Info");
                    filterContext.Controller.TempData["App.Notifications.Info"] = newCollectionofInfoData;
                }
            }

        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }
    }
}
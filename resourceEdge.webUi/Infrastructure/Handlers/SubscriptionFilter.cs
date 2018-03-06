using Microsoft.AspNet.Identity;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Concrete;
using resourceEdge.Domain.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Infrastructure.Handlers
{
    public class SubscriptionFilter : FilterAttribute, IActionFilter
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
          
  
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var initializationCode = filterContext.HttpContext.Request.Form["Code"].ToString();
            var user = filterContext.HttpContext.User.Identity.GetUserId();
            if (user != null && initializationCode != null)
            {
                var Subscribtion = unitOfWork.SubscribedAppraisal.Get(filter: x => x.UserId == user && x.Code == initializationCode).FirstOrDefault();
                if (user != null && Subscribtion != null)
                {
                    var SubScribedAppraisal = unitOfWork.AppraisalInitialization.Get(filter: x => x.Id == Subscribtion.AppraisalInitializationId && x.InitilizationCode == Subscribtion.Code).FirstOrDefault();
                    if (SubScribedAppraisal != null)
                    {
                        filterContext.Result = new ViewResult
                        {
                            ViewName = "UnAuthorized Access"
                        };
                        filterContext.HttpContext.Response.StatusCode = 200;
                    }
                }
                var returnUrl = filterContext.HttpContext.Request.Url.AbsoluteUri;
                filterContext.Result = new RedirectResult("http://localhost:58124/subcription?returnUrl=" + returnUrl);
            }
        }
    }

}
using Microsoft.AspNet.Identity;
using resourceEdge.Domain.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Infrastructure.Handlers
{
    public class LeaveFilter : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userId = filterContext.HttpContext.User.Identity.GetUserId();
            if (userId != null)
            {
               using(UnitOfWork Context = new UnitOfWork())
                {
                    var result = Context.EmployeeLeave.Get(filter: x => x.UserId == userId).FirstOrDefault();
                    if (result == null || result.EmpLeaveLimit == 0)
                    {
                        filterContext.Controller.TempData["Error"] = "Sorry No Leave for you yet. Kindly ask the Hr For Leave";

                        //var returnUrl = "/selfservice/leave";
                        filterContext.Result = new RedirectResult("~/NoLeave" );
                        
                    }
                    else
                    {
                        //var returnUrl = filterContext.HttpContext.Request.Url.AbsoluteUri;
                        //filterContext.Result = new RedirectResult("http://localhost:58124/subcription" + returnUrl);
                    }
                }
            }
        }
    }
}
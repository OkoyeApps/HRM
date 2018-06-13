using resourceEdge.webUi.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Infrastructure.Handlers
{
    public class RedirectPostInterceptor : FilterAttribute, IActionFilter,IAuthorizationFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.HttpMethod.ToLower() == "post")
            {
                var token = new Generators();
                if (!filterContext.RequestContext.HttpContext.Request.Form.AllKeys.Contains("__RequestVerificationToken"))
                {
                    filterContext.RequestContext.HttpContext.Request.Form.Add("__RequestVerificationToken", token.GenerateAntiforgeryToken());
                }
            }
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //throw new NotImplementedException();
        }
    }
}
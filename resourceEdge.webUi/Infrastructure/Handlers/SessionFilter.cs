using Microsoft.AspNet.Identity;
using resourceEdge.Domain.UnitofWork;
using resourceEdge.webUi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Infrastructure.Handlers
{
    public class SessionFilter : ActionFilterAttribute
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        public async Task OnActionExecuting(ActionExecutedContext filterContext)
        {
            string userIdentityObject = null;
            if (filterContext.Controller.TempData["UserId"] != null)
            {
                userIdentityObject = filterContext.Controller.TempData["UserId"].ToString();
            }
            var sessionObject = filterContext.Controller.ControllerContext.HttpContext.Session["_ResourceEdgeTeneceIdentity"];
            if (sessionObject == null)
            {
                if (userIdentityObject == null)
                {
                    userIdentityObject = filterContext.Controller.ControllerContext.HttpContext.User.Identity.GetUserId();
                }
                var userObject = await unitOfWork.GetDbContext().Employee.Where(x => x.userId == userIdentityObject).FirstOrDefaultAsync();
                if (filterContext.Controller.ControllerContext.HttpContext.Session != null && userObject != null)
                {
                    var unitDetails = unitOfWork.GetDbContext().Businessunit.Find(userObject.BusinessunitId);
                    string controller = filterContext.RequestContext.RouteData.Values["controller"].ToString();
                    string action = filterContext.RequestContext.RouteData.Values["action"].ToString();
                    var key = new SessionModel()
                    {
                        Email = userObject.empEmail,
                        FullName = userObject.FullName,
                        LocationId = userObject.LocationId.Value,
                        GroupId = userObject.GroupId,
                        IssuedDate = DateTime.Now,
                        UnitId = userObject.BusinessunitId,
                        UnitName = unitDetails.unitname,
                    };
                    filterContext.Controller.ControllerContext.HttpContext.Session["_ResourceEdgeTeneceIdentity"] = key;
                }
            }
        }
    }
}
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Concrete;
using resourceEdge.Domain.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using resourceEdge.webUi.Models;
using System.Data.Entity;
using resourceEdge.Domain.Entities;

namespace resourceEdge.webUi.Infrastructure.Handlers
{

    public class EdgeIdentityFilter : ActionFilterAttribute
    {
        private IEmployees EmpRepo = new EmployeeRepository();
        
       
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            using (EdgeDbContext unitOfWork = new EdgeDbContext())
            {
                string userIdentityObject = null;
                SessionModel key;
                if (filterContext.Controller.TempData["UserId"] != null)
                {
                    userIdentityObject = filterContext.Controller.TempData["UserId"].ToString();
                }
                var sessionObject = filterContext.Controller.ControllerContext.HttpContext.Session["_ResourceEdgeTeneceIdentity"];
                if (sessionObject == null && filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    if (userIdentityObject == null)
                    {
                        userIdentityObject = filterContext.Controller.ControllerContext.HttpContext.User.Identity.GetUserId();
                    }
                    if (userIdentityObject != null)
                    {
                        var userObject = unitOfWork.Employee.Where(x => x.userId == userIdentityObject).Include("Department").FirstOrDefault();
                        if (filterContext.Controller.ControllerContext.HttpContext.Session != null && userObject != null)
                        {
                            var unitDetails = unitOfWork.Businessunit.Find(userObject.businessunitId);
                            key = new SessionModel()
                            {
                                Email = userObject.empEmail,
                                FullName = userObject.FullName,
                                LocationId = userObject.LocationId.Value,
                                GroupId = userObject.GroupId,
                                IssuedDate = DateTime.Now,
                                UnitId = userObject.businessunitId,
                                UnitName = unitDetails.unitname,
                                 DepartmentId = userObject.DepartmentId,
                                 DepartmentName = userObject.Department.deptname
                            };
                            filterContext.Controller.ControllerContext.HttpContext.Session["_ResourceEdgeTeneceIdentity"] = key;
                        }
                        //this check is done only for the Admin because he is not a user...
                        else
                        {
                            using (ApplicationDbContext db = new ApplicationDbContext()) // this was done like this because i wanted to keep the applicationdbContext only to this scope
                            {

                                var user = db.Users.Where(X => X.Id == userIdentityObject).Select(x => new { Email = x.Email, locationId = x.LocationId, groupId = x.GroupId, fullName = x.UserfullName }).FirstOrDefault();

                                if (filterContext.Controller.ControllerContext.HttpContext.User.IsInRole("System Admin"))
                                {
                                    key = new SessionModel()
                                    {
                                        Email = user.Email,
                                        FullName = user.fullName,
                                        GroupId = user.groupId,
                                        LocationId = user.locationId,
                                        IssuedDate = DateTime.Now
                                    };
                                    filterContext.Controller.ControllerContext.HttpContext.Session["_ResourceEdgeTeneceIdentity"] = key;
                                }
                            }

                        }
                    }
                }
             
            }
            
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            using (EdgeDbContext unitOfWork = new  EdgeDbContext())
            {
                var sessionObject = filterContext.Controller.ControllerContext.HttpContext.Session["_ResourceEdgeTeneceIdentity"];
                if (sessionObject == null)
                {
                    if (filterContext.Controller.ControllerContext.HttpContext.User.Identity.IsAuthenticated)
                    {
                        var userIdentityObject = filterContext.Controller.ControllerContext.HttpContext.User.Identity.GetUserId();
                        var userObject = unitOfWork.Employee.Where(x => x.userId == userIdentityObject).Include("Department").FirstOrDefaultAsync();
                        if (filterContext.Controller.ControllerContext.HttpContext.Session != null && userObject.Result != null)
                        {
                            var unitDetails = unitOfWork.Businessunit.Find(userObject.Result.businessunitId);
                            SessionModel Key = new SessionModel();
                            if (Key != null)
                            {
                                Key.Email = userObject.Result.empEmail;
                                Key.FullName = userObject.Result.FullName;
                                Key.LocationId = userObject.Result.LocationId.Value;
                                Key.GroupId = userObject.Result.GroupId;
                                Key.IssuedDate = DateTime.Now;
                                Key.UnitId = userObject.Result.businessunitId;
                                Key.UnitName = unitDetails.unitname;
                                Key.DepartmentId = userObject.Result.DepartmentId;
                                Key.DepartmentName = userObject.Result.Department.deptname;                          

                                filterContext.Controller.ControllerContext.HttpContext.Session["_ResourceEdgeTeneceIdentity"] = Key;
                            }
                        }

                    }

                }
                string controller = filterContext.RequestContext.RouteData.Values["controller"].ToString();
                string action = filterContext.RequestContext.RouteData.Values["action"].ToString();

                var crumb = new BreadCrumb()
                {
                    Action = action,
                    ActionUrl = $"/{controller}/{action}",
                    Controller = controller
                };
                filterContext.Controller.ControllerContext.HttpContext.Session["_Crumbs"] = crumb;
               
            }
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            
        }
    }


}
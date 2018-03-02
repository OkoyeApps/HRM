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

namespace resourceEdge.webUi.Infrastructure.Handlers
{

        public class EdgeIdentityHandler : FilterAttribute, IActionFilter
        {
            private IEmployees EmpRepo = new EmployeeRepository();
           UnitOfWork unitOfWork = new UnitOfWork();
            public void OnActionExecuted(ActionExecutedContext filterContext)
            {
            if (filterContext.Controller.TempData["UserId"] != null)
            {
                var userIdentityObject = filterContext.Controller.TempData["UserId"].ToString();
                var sessionObject = filterContext.HttpContext.Session["_ResourceEdgeTeneceIdentity"];
                if (sessionObject == null)
                {
                    if (userIdentityObject == null)
                    {
                        userIdentityObject = filterContext.HttpContext.User.Identity.GetUserId();
                    }
                    var userObject = unitOfWork.GetDbContext().employees.Where(x => x.userId == userIdentityObject).FirstOrDefault();
                    var unitDetails = unitOfWork.BusinessUnit.GetByID(userObject.businessunitId);

                    if (filterContext.Controller.ControllerContext.HttpContext.Session != null)
                    {
                        var key = new SessionModel()
                        {
                            Email = userObject.empEmail,
                            FullName = userObject.FullName,
                            LocationId = userObject.LocationId.Value,
                            GroupId = userObject.GroupId,
                            IssuedDate = DateTime.Now,
                            UnitId = userObject.businessunitId,
                            UnitName = userObject.Departments.BusinessUnits.unitname
                        };
                        filterContext.HttpContext.Session["_ResourceEdgeTeneceIdentity"] = key;
                    }
                }
           
           
            }
        }
                

            public void OnActionExecuting(ActionExecutingContext filterContext)
            {
            var sessionObject = filterContext.HttpContext.Session["_ResourceEdgeTeneceIdentity"];
            if (sessionObject == null)
            {
                var userIdentityObject = filterContext.HttpContext.User.Identity.GetUserId();
                var userObject = unitOfWork.GetDbContext().employees.Where(x=>x.userId == userIdentityObject).FirstOrDefault();
                var unitDetails = unitOfWork.BusinessUnit.GetByID(userObject.businessunitId);
                if (filterContext.Controller.ControllerContext.HttpContext.Session != null)
                {
                    SessionModel Key = new SessionModel();
                    if (Key != null)
                    {
                        Key.Email = userObject.empEmail;
                        Key.FullName = userObject.FullName;
                        Key.LocationId = userObject.LocationId.Value;
                        Key.GroupId = userObject.GroupId;
                        Key.IssuedDate = DateTime.Now;
                        Key.UnitId = userObject.businessunitId;
                        Key.UnitName = unitDetails.unitname;

                        filterContext.HttpContext.Session["_ResourceEdgeTeneceIdentity"] = Key;

                    }
                }

            }
        }

    }


}
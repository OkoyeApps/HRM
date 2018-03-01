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

        public class AccountHandler : FilterAttribute, IActionFilter
        {
            private IEmployees EmpRepo = new EmployeeRepository();
            public void OnActionExecuted(ActionExecutedContext filterContext)
            {
                var userIdentityObject = filterContext.Controller.TempData["UserId"].ToString();
                var userObject = EmpRepo.GetByUserId(userIdentityObject);
                var key = new SessionModel()
                {
                    Email = userObject.empEmail,
                    FullName = userObject.FullName,
                    LocationId = userObject.LocationId.Value,
                    GroupId = userObject.GroupId,
                    IssuedDate = DateTime.Now,
                    UnitId = userObject.businessunitId
                    };
                    filterContext.Controller.ControllerContext.HttpContext.Session["_ResourceEdgeTeneceIdentity"] = key;
                }

            public void OnActionExecuting(ActionExecutingContext filterContext)
            {

            }

    }


}
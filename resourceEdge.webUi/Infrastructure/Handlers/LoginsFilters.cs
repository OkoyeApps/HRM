using Microsoft.AspNet.Identity;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Infrastructure.Handlers
{
    public class LoginsFilters : ActionFilterAttribute
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var CurrentUrl = filterContext.HttpContext.Request.Url.AbsoluteUri;

            if (filterContext.Controller.TempData["Email"] != null && CurrentUrl.ToLower().Contains("login"))
            {
                filterContext.Controller.TempData["Email"] = null;
                filterContext.Controller.TempData.Remove("Password");
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var sessionId = filterContext.RequestContext.HttpContext.Session.SessionID;
            var UserID = filterContext.HttpContext.User.Identity.GetUserId();
            string Email = null;
            string Password = null;
            if (UserID != null)
            {
                var currentLogin = unitOfWork.Logins.Get(filter: x => x.UserID == UserID && x.SessionID == sessionId && x.IsLogOut == false);
                var previousUserSessions = unitOfWork.Logins.Get(filter: x => x.UserID == UserID && x.IsLogOut == false && x.SessionID != sessionId).FirstOrDefault();
                var CurrentUrl = filterContext.HttpContext.Request.Url.AbsoluteUri;
                if (previousUserSessions != null)
                {
                     Email = filterContext.Controller.TempData["Email"] != null ? filterContext.Controller.TempData["Email"].ToString() : null;
                     Password = filterContext.Controller.TempData["Password"] != null ? filterContext.Controller.TempData["Password"].ToString() : null;
                    if (Email != null && Password != null)
                    {
                        filterContext.HttpContext.Session.Clear();
                        filterContext.HttpContext.Session.Abandon();
                        HttpContext.Current.Session.Remove(previousUserSessions.SessionID);
                        HttpContext.Current.Session.Abandon();
                        HttpContext.Current.Session.Clear();
                        previousUserSessions.IsLogOut = true;
                        previousUserSessions.LogOutTime = DateTime.Now;
                        unitOfWork.Logins.Update(previousUserSessions);
                        unitOfWork.Save();
                    }
                    else
                    {
                       // filterContext.Controller.TempData.Clear();

                        if (Email != null && Password != null)
                        {
                            filterContext.Result = new RedirectResult($"~/Account/CustomLogOff?Email={Email}&Password={Password}");
                            var browser = HttpContext.Current.Request.Browser.Type;
                        }
                        else if(Email == null && Password == null && !CurrentUrl.ToLower().Contains("customlogoff") && !CurrentUrl.ToLower().Contains("login"))
                        {
                            filterContext.Result = new RedirectResult("~/Account/CustomLogOff");
                        }
                    }
                }

                if (currentLogin.Count() == 0 && previousUserSessions == null)
                {
                    Login login = new Login()
                    {
                        UserID = filterContext.HttpContext.User.Identity.GetUserId(),
                        IsLogIn = true,
                        IsLogOut = false,
                        LoginTime = DateTime.Now,
                        SessionID = filterContext.HttpContext.Session.SessionID
                    };
                    unitOfWork.Logins.Insert(login);
                    unitOfWork.Save();
                }
            }
        }
    }
}

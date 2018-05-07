using Microsoft.AspNet.Identity;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Infrastructure.Handlers
{
    public class LoginsFilters : ActionFilterAttribute
    {

        //AsyncGenericRepository<Login> LoginRepo = new AsyncGenericRepository<Login>(new EdgeDbContext());
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
            using (EdgeDbContext unitOfWork = new EdgeDbContext())
            {
                var sessionId = filterContext.RequestContext.HttpContext.Session.SessionID;
                var UserID = filterContext.HttpContext.User.Identity.GetUserId();
                string Email = null;
                string Password = null;
                if (UserID != null)
                {
                    var currentLogin = unitOfWork.Login.Where(x => x.UserID == UserID && x.SessionID == sessionId && x.IsLogOut == false);
                    var previousUserSessions = unitOfWork.Login.Where(x => x.UserID == UserID && x.IsLogOut == false && x.SessionID != sessionId).FirstOrDefault();
                    //var previousUserSessions = LoginRepo.Get(x => x.UserID == UserID && x.IsLogOut == false && x.SessionID != sessionId).GetAwaiter().GetResult().FirstOrDefault();

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
                            //unitOfWork.Logins.Update(previousUserSessions);
                            unitOfWork.Entry(previousUserSessions).State = EntityState.Modified;
                            unitOfWork.SaveChanges();
                            // LoginRepo.Update(previousUserSessions).Wait();

                        }
                        else
                        {
                            // filterContext.Controller.TempData.Clear();

                            if (Email != null && Password != null)
                            {
                                filterContext.Result = new RedirectResult($"~/Account/CustomLogOff?Email={Email}&Password={Password}");
                                var browser = HttpContext.Current.Request.Browser.Type;
                            }
                            else if (Email == null && Password == null && !CurrentUrl.ToLower().Contains("customlogoff") && !CurrentUrl.ToLower().Contains("login"))
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
                        // LoginRepo.Insert(login).Wait();
                        unitOfWork.Login.Add(login);
                        unitOfWork.SaveChanges();
                    }
                  
                }
            }
        }
    }
}

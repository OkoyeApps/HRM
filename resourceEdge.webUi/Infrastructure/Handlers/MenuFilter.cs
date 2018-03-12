using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Infrastructure.Handlers
{
    public class MenuFilter : FilterAttribute, IActionFilter
    {
        /// <summary>
        /// This filter was registered globally because it only makes one Db call and it intializes all filters that are 
        /// set to active by the administrator and it is set on the 
        /// </summary>
        UnitOfWork unitOfWork = new UnitOfWork();
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
       
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var MenuFromSession = filterContext.HttpContext.Session;
            if (MenuFromSession == null || MenuFromSession.Count != 0)
            {
                var Menus = unitOfWork.Menu.Get(filter: x => x.Active == true).ToList();
                if (Menus.Count != 0)
                {
                filterContext.HttpContext.Session["_Menu"] = Menus;
                }
                else
                {
                    filterContext.HttpContext.Session["_Menu"] = null;
                }
            }
        }
    }
}
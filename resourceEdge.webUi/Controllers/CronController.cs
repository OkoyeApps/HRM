using resourceEdge.webUi.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Controllers
{
    public class CronController : Controller
    {
        CronJob cron = new CronJob();
        // GET: Cron
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Schedule()
        {
            cron.AddSubscriptionCodeToMail();
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}
using resourceEdge.webUi.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Hangfire;

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
        public async Task<JsonResult> SendAccountDetail()
        {
           //BackgroundJob.Schedule( () => cron.GetAllppraisalForCalculation(), TimeSpan.FromMinutes(5));
            await cron.SendAccountDetails();
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}
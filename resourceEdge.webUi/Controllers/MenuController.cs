using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using resourceEdge.webUi.Infrastructure.Core;
using System.Web.Mvc;

namespace resourceEdge.webUi.Controllers
{
    [Authorize(Roles ="System Admin,HR, Head HR")]
    public class MenuController : Controller
    {
        IMenu MenuRepo;
        public MenuController(IMenu Mparam)
        {
            MenuRepo = Mparam;
        }
        // GET: Menu
        public ActionResult Index()
        {
            return View(MenuRepo.Get());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Activate(int id)
        {
            MenuRepo.ActivateMenu(id);
            this.AddNotification("Menu Activated SuccessFully", NotificationType.SUCCESS);
            //Make this a json result later
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DeActivate(int id)
        {
            MenuRepo.DeActivateMenu(id);
            this.AddNotification("Menu De-activated SuccessFully", NotificationType.SUCCESS);
            //Make this a json result later
            return RedirectToAction("Index");
        }
    }
}
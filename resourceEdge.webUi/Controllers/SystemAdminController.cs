using Microsoft.AspNet.Identity.EntityFramework;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.webUi.Infrastructure;
using resourceEdge.webUi.Infrastructure.Core;
using resourceEdge.webUi.Infrastructure.Handlers;
using resourceEdge.webUi.Infrastructure.SystemManagers;
using resourceEdge.webUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Controllers
{
    [CustomAuthorizationFilter(Roles ="Super Admin")]
    [RoutePrefix("Admin")]
    public class SystemAdminController : Controller
    {
        IGroups GroupRepo;
        AdminManager manager;
        public SystemAdminController(IGroups GParam)
        {
            manager = new AdminManager();
            GroupRepo = GParam;
        }
        [Route("AssignGroupHeadHr")]
        public ActionResult AssignGroupHeadHr()
        {
            ViewBag.Groups = new SelectList(GroupRepo.Get().OrderBy(X => X.Id), "Id", "GroupName", "Id");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignGroupHeadHr(FormCollection collection)
        {
            int groupid;
            int.TryParse(collection["groupId"], out groupid);
            string userId = collection["userId"].ToString();
            if (userId != null && groupid.ToString() != null)
            {
                var result = await manager.AssignHeadHR(userId, groupid);
                if (result != true)
                {
                    return RedirectToAction("Index", "HR");
                }
            }
            return RedirectToAction("AssignGroupHeadHr");
        }

        // GET: SuperAdmin/Admin
        //list of all admins and there locations
        [Route("Index")]
        public ActionResult Index()
        {
            ViewBag.PageTitle = "All Admin(s)";
            return View(manager.GetAllSystemAdmin());
        }
        //Add admin for group and is irrespective of employee
        [Route("AddAdmin")]
        public ActionResult AddAdmin()
      {
            DropDownManager dropManager = new DropDownManager();
            ViewBag.PageTitle = "Add Admin";
            ViewBag.Groups = dropManager.GetGroup();
            return View();
        }
        [ValidateAntiForgeryToken, HttpPost, Route("AddAdmin")]
        public ActionResult AddAdmin(SystemAdminViewModel model)
        {
            var result = manager.AddLocationSystemAdmin(model);
            if (result)
            {
                this.AddNotification("Admin Added!", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            this.AddNotification("Something went wrong and admin could not be added, if problem persists, please contact your system administrator", NotificationType.ERROR);
            return RedirectToAction("AddAdmin");
        }
        [Route("AdminDetail/{id:int}")]
        public ActionResult AdminDetail(int id)
        {
            var result = manager.GetAdminById(id);
            if (result != null)
            {
                return PartialView(result);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
    }
}
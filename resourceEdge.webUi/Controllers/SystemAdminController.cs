using Microsoft.AspNet.Identity.EntityFramework;
using resourceEdge.Domain.Abstracts;
using resourceEdge.webUi.Infrastructure;
using resourceEdge.webUi.Infrastructure.Handlers;
using resourceEdge.webUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Controllers
{
    [CustomAuthorizationFilter(Roles ="Super Admin")]
    [RoutePrefix("Admin")]
    public class SystemAdminController : Controller
    {
        IEmployees empRepo;
        IGroups GroupRepo;
        AdminManager manager;
        public SystemAdminController(IEmployees eParam,IGroups GParam)
        {
            empRepo = eParam;
            manager = new AdminManager(eParam);
            GroupRepo = GParam;
        }

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
        public ActionResult Index()
        {
            return View();
        }
        //Add admin for group and is irrespective of employee
        public ActionResult AddAdmin()
        {
            return View();
        }
    }
}
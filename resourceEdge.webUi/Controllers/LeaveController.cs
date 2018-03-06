using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.Abstracts;
using Microsoft.AspNet.Identity;
using resourceEdge.webUi.Infrastructure;
using resourceEdge.webUi.Infrastructure.Handlers;

namespace resourceEdge.webUi.Controllers
{
    [EdgeIdentityFilter]
    public class LeaveController : Controller
    {
        private EdgeDbContext db = new EdgeDbContext();
        private ILeaveManagement leaveRepo;
        private IBusinessUnits BunitsRepo;
        LeaveManager leavemanagerRepo;
        Apimanager Apimanager;
        public LeaveController(ILeaveManagement lParam, IBusinessUnits bparam)
        {
            leaveRepo = lParam;
            BunitsRepo = bparam;
            Apimanager = new Apimanager();
        }

        [Authorize(Roles = "HR")]
        public ActionResult Index()
        {
           return View(leaveRepo.GetLeave());
        }

        [Authorize(Roles = "HR")]
        public ActionResult Create()
        {

            ViewBag.businessUnits = new SelectList(BunitsRepo.Get().OrderBy(x => x.BusId), "BusId", "unitname", "BusId");
            ViewBag.Months = new SelectList(Apimanager.GetAllMonths().OrderBy(x => x.id), "MonthId", "MonthName", "MonthId");
            ViewBag.weekDays = new SelectList(Apimanager.GetWeekDays().OrderByDescending(x => x.id), "id", "DayLongCode", "id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,CalStartMonth,WeekendStartDay,WeekendEndDay,BusinessunitId,DepartmentId,HrId,HoursDay,IsHalfday,IsLeaveTransfer,IsSkipHolidays,Descriptions,Isactive")] LeaveManagementViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LeaveManagement leave = new LeaveManagement();
                    leave.CalStartMonth = model.CalStartMonth.Value.ToString();
                    leave.businessunitId = model.businessunitId;
                    leave.WeekendStartDay = model.WeekendStartDay.Value.ToString();
                    leave.WeekendEndDay = model.WeekendEndDay.Value.ToString();
                    leave.IsSkipHolidays = model.IsSkipHolidays;
                    leave.IsLeaveTransfer = model.IsLeaveTransfer;
                    leave.departmentId = model.departmentId;
                    leave.Descriptions = model.Descriptions;
                    leave.HoursDay = model.HoursDay.Value.ToString();
                    leave.HrId = model.HrId;
                    leave.createdby = User.Identity.GetUserId();
                    leave.modifiedby = User.Identity.GetUserId();
                    leave.Isactive = true;
                    leaveRepo.AddLeaveManagement(leave);
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            ViewBag.businessUnits = new SelectList(BunitsRepo.Get().OrderBy(x => x.BusId), "BusId", "unitname", "BusId");
            ViewBag.Months = new SelectList(Apimanager.GetAllMonths().OrderBy(x => x.id), "MonthId", "MonthName", "MonthId");
            ViewBag.weekDays = new SelectList(Apimanager.GetWeekDays().OrderByDescending(x => x.id), "id", "DayLongCode", "id");
            return View(model);
        }
        [Authorize(Roles = "HR")]
        public ActionResult AllEmployeeLeave()
        {      
            return View(leaveRepo.GetAllotedLeave());
        }
        [Authorize(Roles = "HR")]
        public ActionResult AllotLeaves()
        {
            ViewBag.PageTitle = "Allot Leave";
            ViewBag.businessUnits = new SelectList(BunitsRepo.Get().OrderBy(x => x.BusId), "BusId", "unitname", "BusId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AllotLeaves(FormCollection collection)
        {
            var Days = collection["Emp"];
            var userid = collection["id"];
            var year = collection["year"];
            var array = new string[] { };
            if (Days.Contains(",") && userid.Contains(","))
            {
              var allotedDays =   Days.Split(',');
              var splitId = userid.Split(',');
              var result =   leavemanagerRepo.AllotCollectiveLeave(allotedDays, splitId, year, User.Identity.GetUserId());
                if (result != false)
                {
                    RedirectToAction("Index");
                }
            }
            else
            {
                var result = leavemanagerRepo.allotIndividualLeave(Days, userid, year, User.Identity.GetUserId());
                if (result != false)
                {
                    RedirectToAction("Index");
                }
            }
            TempData["Error"] = "Something went wrong, please kindly make sure you fill all fields for allocation";
            ViewBag.businessUnits = new SelectList(BunitsRepo.Get().OrderBy(x => x.BusId), "BusId", "unitname", "BusId");
            return View();
        }

        // GET: LeaveManagementViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var leaveManagementViewModel = db.LeaveManagement.Find(id);
            if (leaveManagementViewModel == null)
            {
                return HttpNotFound();
            }
            return View(leaveManagementViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,CalStartMonth,WeekendStartDay,WeekendEndDay,BusinessunitId,DepartmentId,HrId,HoursDay,IsHalfday,IsLeaveTransfer,IsSkipHolidays,Descriptions,Isactive")] LeaveManagementViewModel leaveManagementViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leaveManagementViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(leaveManagementViewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var leaveManagementViewModel = db.LeaveManagement.Find(id);
            if (leaveManagementViewModel == null)
            {
                return HttpNotFound();
            }
            return View(leaveManagementViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var leaveManagementViewModel = db.LeaveManagement.Find(id);
            db.LeaveManagement.Remove(leaveManagementViewModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult AllLeaveRequest()
        {
            ViewBag.PageTitle = "All Leave Request";
            return View(leaveRepo.AllLeaveRequestForConfirmation());
        }

        [Authorize(Roles ="Manager")]
        public ActionResult leaveRequests()
        {
            var requests = leaveRepo.GetLeaveRequestsForManager(User.Identity.GetUserId());
            return View(requests);
        }

        [Authorize(Roles ="HR")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectResult ApproveLeave(int? id, string userId, string returnUrl)
        {
            if (id != null && userId != null)
            {
               var result =  leavemanagerRepo.Approveleave(id.Value, userId, User.Identity.GetUserId());
                if (result != false)
                {
                   return Redirect(returnUrl);
                }
            }
            TempData["Error"] = "Something went wong and this request could not be completed. please retry in a moment";
            return Redirect(returnUrl);
        }


        [Authorize(Roles ="Manager, HR")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectResult DenyLeave(int? id, string userid, string returnUrl)
        {
            if (id != null && userid != null)
            {
                var result = leavemanagerRepo.DenyLeave(id.Value, userid);
                if (result != false)
                {
                    return Redirect(returnUrl);
                }
            }
            TempData["Error"] = "Something went wong and this request could not be completed. please retry in a moment";
            return Redirect(returnUrl);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

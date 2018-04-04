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
using resourceEdge.webUi.Infrastructure.Core;

namespace resourceEdge.webUi.Controllers
{
    [CustomAuthorizationFilter]
    public class LeaveController : Controller
    {
        private EdgeDbContext db = new EdgeDbContext();
        private ILeaveManagement leaveRepo;
        private IBusinessUnits BunitsRepo;
        private LeaveManager LmanagerRepo;
        Apimanager Apimanager;
        public LeaveController(IEmployees eParam, ILeaveManagement lParam, IBusinessUnits bparam)
        {
            leaveRepo = lParam;
            BunitsRepo = bparam;
            Apimanager = new Apimanager();
            LmanagerRepo = new LeaveManager(eParam, lParam);
        }

        [Authorize(Roles = "HR")]
        public ActionResult Index()
        {
           return View(leaveRepo.GetLeave());
        }

        [Authorize(Roles = "HR")]
        public ActionResult Create()
        {

            ViewBag.businessUnits = new SelectList(BunitsRepo.Get().OrderBy(x => x.Id), "Id", "unitname", "Id");
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
            ViewBag.businessUnits = new SelectList(BunitsRepo.Get().OrderBy(x => x.Id), "Id", "unitname", "Id");
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
            ViewBag.businessUnits = new SelectList(BunitsRepo.Get().OrderBy(x => x.Id), "Id", "unitname", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AllotLeaves(FormCollection collection)
        {
            var leaveAmounts = collection["amount"];
            if (leaveAmounts.Contains(","))
            {
              var result =   LmanagerRepo.AllotCollectiveLeave(collection);
                if (result != false)
                {
                    this.AddNotification("Yay!", NotificationType.SUCCESS);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                var result = LmanagerRepo.AllotOrUpdateIndividualLeave(collection);
                if (result != false)
                {
                    this.AddNotification("Yay!", NotificationType.SUCCESS);
                    return RedirectToAction("Index");
                }
            }
            this.AddNotification("Something went wrong, please kindly Try Again", NotificationType.ERROR);
            return RedirectToAction("AllotLeaves");
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

        [Authorize(Roles ="HR, Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectResult ApproveLeave(int? id, string userId, string returnUrl)
        {
            if (id != null && userId != null)
            {
               var result =  LmanagerRepo.Approveleave(id.Value, userId, User.Identity.GetUserId());
                if (result != false)
                {
                    this.AddNotification("Leave Approved", NotificationType.SUCCESS);
                   return Redirect(returnUrl);
                }
            }
            this.AddNotification("Something went wong and this request could not be completed. please retry in a moment", NotificationType.ERROR);
            return Redirect(returnUrl);
        }


        [Authorize(Roles ="Manager, HR")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectResult DenyLeave(int? id, string userid, string returnUrl)
        {
            if (id != null && userid != null)
            {
                var result = LmanagerRepo.DenyLeave(id.Value, userid);
                if (result != false)
                {
                    this.AddNotification("Leave Denied", NotificationType.SUCCESS);
                    return Redirect(returnUrl);
                }
            }
            this.AddNotification("Something went wrong and this request could not be completed. please retry in a moment", NotificationType.ERROR);
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

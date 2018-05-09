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
using resourceEdge.webUi.Infrastructure.SystemManagers;
using resourceEdge.webUi.Models;

namespace resourceEdge.webUi.Controllers
{
    [CustomAuthorizationFilter]
    public class LeaveController : Controller
    {
        private EdgeDbContext db = new EdgeDbContext();
        private ILeaveManagement leaveRepo;
        private IBusinessUnits BunitsRepo;
        private LeaveManager LmanagerRepo;
        DropDownManager dropdownManager;
        public LeaveController(IEmployees eParam, ILeaveManagement lParam, IBusinessUnits bparam)
        {
            leaveRepo = lParam;
            BunitsRepo = bparam;
            LmanagerRepo = new LeaveManager(eParam, lParam);
            dropdownManager = new DropDownManager();
        }

        [CustomAuthorizationFilter(Roles = "HR")]
        public ActionResult Index()
        {
            return View(leaveRepo.GetLeave());
        }

        [CustomAuthorizationFilter(Roles = "HR")]
        public ActionResult Create()
        {

            ViewBag.businessUnits = dropdownManager.GetBusinessUnit();
            ViewBag.Months = dropdownManager.GetAllMonths();
            ViewBag.weekDays = dropdownManager.GetWeekDays();
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
                    this.AddNotification($"", NotificationType.SUCCESS);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ViewBag.businessUnits = dropdownManager.GetBusinessUnit();
            ViewBag.Months = dropdownManager.GetAllMonths();
            ViewBag.weekDays = dropdownManager.GetWeekDays();
            this.AddNotification($"", NotificationType.ERROR);
            return View(model);
        }
        [CustomAuthorizationFilter(Roles = "HR")]
        public ActionResult AllEmployeeLeave()
        {
            return View(leaveRepo.GetAllotedLeave());
        }
        [CustomAuthorizationFilter(Roles = "HR")]
        public ActionResult AllotLeaves()
        {
            ViewBag.PageTitle = "Allot Leave";
            var UserFromSession = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            ViewBag.businessUnits = dropdownManager.GetBusinessUnit(UserFromSession.GroupId, UserFromSession.LocationId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AllotLeaves(FormCollection collection)
        {
            var leaveAmounts = collection["amount"];
            if (leaveAmounts.Contains(","))
            {
                int year = 0;
                var AllotYear = collection["year"];
                int.TryParse(AllotYear, out year);
                if (year == DateTime.Now.Year)
                {

                    var result = LmanagerRepo.AllotCollectiveLeave(collection);
                    if (result != false)
                    {
                        this.AddNotification($"Yay!", NotificationType.SUCCESS);
                        return RedirectToAction("Index");
                    }

                }
                this.AddNotification("Sorry you can only allot leave for current year", NotificationType.ERROR);
                return RedirectToAction("AllotLeaves");
            }
            else
            {
                var result = LmanagerRepo.AllotOrUpdateIndividualLeave(collection);
                if (result != false)
                {
                    this.AddNotification($"Yay!", NotificationType.SUCCESS);
                    return RedirectToAction("AllotLeaves");
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
            var UserFromSession = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            return View(LmanagerRepo.AllLeaveRequestForConfirmation(UserFromSession.GroupId, UserFromSession.LocationId));
        }

        [CustomAuthorizationFilter(Roles = "Manager")]
        public ActionResult leaveRequests()
        {
            ViewBag.PageTitle = "All Leave Request";
            var requests = LmanagerRepo.GetAllLeaveRequestForManager(User.Identity.GetUserId());
            return View(requests);
        }

        [CustomAuthorizationFilter(Roles = "HR, Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectResult ApproveLeave(int? id, string userId, string returnUrl)
        {
            if (id != null && userId != null)
            {
                var result = LmanagerRepo.Approveleave(id.Value, userId, User.Identity.GetUserId());
                if (result != false)
                {
                    this.AddNotification($"Leave Approved", NotificationType.SUCCESS);
                    return Redirect(returnUrl);
                }
            }
            this.AddNotification($"Something went wong and this request could not be completed. please retry in a moment", NotificationType.ERROR);
            return Redirect(returnUrl);
        }


        [CustomAuthorizationFilter(Roles = "Manager, HR")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectResult DenyLeave(int? id, string userid, string returnUrl)
        {
            if (id != null && userid != null)
            {
                var result = LmanagerRepo.DenyLeave(id.Value, userid);
                if (result != false)
                {
                    this.AddNotification($"Leave Denied", NotificationType.SUCCESS);
                    return Redirect(returnUrl);
                }
            }
            this.AddNotification($"Something went wrong and this request could not be completed. please retry in a moment", NotificationType.ERROR);
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

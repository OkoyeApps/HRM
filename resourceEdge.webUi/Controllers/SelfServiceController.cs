using Microsoft.AspNet.Identity;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.webUi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Controllers
{
    public class SelfServiceController : Controller
    {
        private EmployeeManager empManager;
        ILeaveManagement leaveRepo;
        LeaveManager leavemanagerRepo;

        public SelfServiceController(EmployeeManager Emparam, ILeaveManagement lParam)
        {
            empManager = Emparam;
            leaveRepo = lParam;
            leavemanagerRepo = new LeaveManager(leaveRepo, empManager);
        }

        public ActionResult Leave()
        {
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.Approved = leavemanagerRepo.GetEmployeeApprovedLeave(User.Identity.GetUserId());
            ViewBag.Denied = leavemanagerRepo.GetEmployeeDeniedLeave(User.Identity.GetUserId());
            ViewBag.Pending = leavemanagerRepo.GetEmployeePendingLeave(User.Identity.GetUserId());
            ViewBag.All = leavemanagerRepo.GetEmployeeAllLeave(User.Identity.GetUserId());
            return View();
        }

        public ActionResult RequestLeave()
        {
            ViewBag.leaveType = new SelectList(leaveRepo.GetLeaveTypes().OrderBy(x => x.leavetype), "id", "leavetype", "id");
            ViewBag.userId = User.Identity.GetUserId();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestLeave(LeaveRequestViewModel model)
        {
            var leaveName = leavemanagerRepo.GetLeaveDetails(model.LeavetypeId.Value);
            var getPreviousAppliedDateNo = leavemanagerRepo.GetLeaveAppliedFor(model.UserId);
            var validLeave = leavemanagerRepo.CheckIfLeaveIsFinished(model.UserId,(int) model.requestDays,int.Parse(model.AvailableLeave));
            if (ModelState.IsValid && leaveName != null && validLeave != true)
            {       
                LeaveRequest leave = new LeaveRequest();
                leave.UserId = model.UserId.ToString();
                leave.RepmangId = model.RepmangId;
                leave.NoOfDays = model.LeaveNoOfDays;
                leave.ToDate = model.ToDate;
                leave.FromDate = model.FromDate;
                leave.Reason = model.Reason;
                leave.LeavetypeId = model.LeavetypeId;
                leave.createdby = User.Identity.GetUserId();
                leave.modifiedby = User.Identity.GetUserId();
                leave.createddate = DateTime.Now;
                leave.modifieddate = DateTime.Now;
                leave.isactive = true;
                leave.LeaveStatus = null;
                leave.LeaveName = leaveName.leavetype;
                leave.Availableleave =int.Parse(model.AvailableLeave);
                leave.AppliedleavesCount =+ model.requestDays + getPreviousAppliedDateNo; //Remember to do this with the modelState to check if leave is finished
                leave.requestDaysNo = model.requestDays;
                leaveRepo.AddLeaveRequest(leave);
                return RedirectToAction("Leave", "selfService"); //redirect to employee selservice page for him to see his requests
            }
            TempData["Error"] = "Something went wrong";
            ViewBag.leaveType = new SelectList(leaveRepo.GetLeaveTypes().OrderBy(x => x.leavetype), "id", "leavetype", "id");
            ViewBag.userId = User.Identity.GetUserId();
            ModelState.AddModelError("", "Please make sure your leave your leave hasn't been exhausted or you can ask The HR to assign more leave for you.");
            return View(model);
        }

        [HttpGet]
        public IEnumerable<LeaveRequest> GetEmployeePendingLeave(string userId)
        {
            if (userId != null)
            {
              var result =   leavemanagerRepo.GetEmployeePendingLeave(userId);
                return result;
            }
            return null;
        }
        [HttpGet]
        public IEnumerable<LeaveRequest> GetEmployeeApprovedLeave(string userId)
        {
            if (userId != null)
            {
                var result = leavemanagerRepo.GetEmployeeApprovedLeave(userId);
                return result;
            }
            return null;
        }
        [HttpGet]
        public IEnumerable<LeaveRequest> GetEmployeeDeniedLeave(string userId)
        {
            if (userId != null)
            {
                var result = leavemanagerRepo.GetEmployeeDeniedLeave(userId);
                return result;
            }
            return null;
        }
        [HttpGet]
        public IEnumerable<LeaveRequest> GetEmployeeAllLeave(string userId)
        {
            if (userId != null)
            {
                var result = leavemanagerRepo.GetEmployeeAllLeave(userId);
                return result;
            }
            return null;
        }















        // GET: SelfService/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SelfService/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

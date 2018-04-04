using Microsoft.AspNet.Identity;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.webUi.Infrastructure;
using resourceEdge.webUi.Infrastructure.Core;
using resourceEdge.webUi.Infrastructure.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Controllers
{
    
    [CustomAuthorizationFilter]
    public class SelfServiceController : Controller
    {
        ILeaveManagement leaveRepo;
        LeaveManager leavemanagerRepo;

        public SelfServiceController(ILeaveManagement lParam, IEmployees EmpParam)
        {
            leaveRepo = lParam;
            leavemanagerRepo = new LeaveManager(EmpParam,leaveRepo);
        }

        public ActionResult Leave()
        {
            ViewBag.PageTitle = "LEAVE DASHBOARD";
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.Approved = leavemanagerRepo.GetEmployeeApprovedLeave(User.Identity.GetUserId());
            ViewBag.Denied = leavemanagerRepo.GetEmployeeDeniedLeave(User.Identity.GetUserId());
            ViewBag.Pending = leavemanagerRepo.GetEmployeePendingLeave(User.Identity.GetUserId());
            ViewBag.All = leavemanagerRepo.GetEmployeeAllLeave(User.Identity.GetUserId());
            return View();
        }

        [LeaveFilter]
        public ActionResult RequestLeave()
        {
            ViewBag.PageTitle = "Request Leave";
            ViewBag.leaveType = new SelectList(leaveRepo.GetLeaveTypes().OrderBy(x => x.leavetype), "id", "leavetype", "id");
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.UsedLeave = leavemanagerRepo.GetEmployeeUsedLeave(User.Identity.GetUserId());
            ViewBag.AvailableLeave = leavemanagerRepo.GetEmpAvailableLeave(User.Identity.GetUserId());
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestLeave(LeaveRequestViewModel model)
        {
            var leaveName = leavemanagerRepo.GetLeaveDetails(model.LeavetypeId.Value);
            var getPreviousAppliedDateNo = leavemanagerRepo.GetLeaveAppliedFor(model.userKey);
            var validLeave = leavemanagerRepo.CheckIfLeaveIsFinished(model.userKey,(int) model.requestDays,int.Parse(model.AvailableLeave));
            if (ModelState.IsValid && leaveName != null && validLeave != true)
            {       
                LeaveRequest leave = new LeaveRequest();
                leave.UserId = model.userKey;
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
                this.AddNotification("", NotificationType.SUCCESS);
                return RedirectToAction("Leave", "selfService"); //redirect to employee selservice page for him to see his requests
            }
            this.AddNotification("Something went wrong, Please try again", NotificationType.ERROR);
            ViewBag.leaveType = new SelectList(leaveRepo.GetLeaveTypes().OrderBy(x => x.leavetype), "id", "leavetype", "id");
            ViewBag.userId = User.Identity.GetUserId();
            this.AddNotification("Please make sure your leave your leave hasn't been exhausted or you can ask The HR to assign more leave for you.", NotificationType.WARNING);
            return RedirectToAction("RequestLeave");
        }
    }
}

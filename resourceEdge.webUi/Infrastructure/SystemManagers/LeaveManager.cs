using Microsoft.AspNet.Identity;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;
using resourceEdge.webUi.Models.SystemModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Infrastructure
{
    public class LeaveManager
    {
        private ILeaveManagement LeaveRepo;
        IEmployees EmpRepo;
        UnitOfWork unitofWork;
        EmployeeManager EmpLogic;
        public LeaveManager()
        {

        }
        public LeaveManager(IEmployees Eparam, ILeaveManagement lParam)
        {
            LeaveRepo = lParam;
            EmpRepo = Eparam;
            unitofWork = new UnitOfWork();
        }
        public EmployeeLeaveType GetLeaveDetails(int id)
        {
            var leave = unitofWork.GetDbContext().LeaveType.Find(id);
            return leave ?? null;
        }

        public double? GetEmpAvailableLeave(string userId)
        {
            var leave = LeaveRepo.GetEmplyeeLeaveByUserId(userId);
            return leave != null ? leave.EmpLeaveLimit : null;
        }

        public double? GetEmployeeUsedLeave(string userId)
        {
            var result = unitofWork.EmployeeLeave.Get(filter: x => x.UserId == userId).FirstOrDefault();
            return result.UsedLeaves != null ? result.UsedLeaves : 0;
        }

     

        public List<LeaveRequest> GetPendingLeaveForManager(string userid)
        {
            var leave = unitofWork.LRequest.Get(filter: x => x.RepmangId == userid && x.LeaveStatus == null).ToList();
            return leave ?? null;
        }

        public bool Approveleave(int leaveId, string userId, string ApproveeId)
        {
            var leaveRequest = unitofWork.LRequest.GetByID(leaveId);
            var Approvee = unitofWork.employees.Get(filter: x => x.userId == ApproveeId).FirstOrDefault();

            if (leaveRequest != null)
            {
                var EmpLeave = LeaveRepo.GetEmplyeeLeaveByUserId(userId);
                if (EmpLeave != null)
                {
                    ///this check is for the Hr and the unit manager. 
                    ///The unit manager has to approve first before the Hr.
                    ///at first it is the Unit Head then the Hr.
                    if (Approvee.empRoleId == 2)
                    {
                        leaveRequest.Approval1 = true;
                        LeaveRepo.updateLeaveRequest(leaveRequest);
                        return true;
                    }
                    else if(Approvee.empRoleId == 3)
                    {
                        int usedLeave;
                        if (EmpLeave.UsedLeaves == null)
                            usedLeave = 0;
                        else usedLeave = (int)EmpLeave.UsedLeaves;
                        EmpLeave.UsedLeaves = leaveRequest.requestDaysNo + usedLeave;
                        LeaveRepo.UpdateEmployeeLeave(EmpLeave);
                        leaveRequest.LeaveStatus = true;
                        leaveRequest.Approval2 = true; //this is not working now. check to rectify later
                        LeaveRepo.updateLeaveRequest(leaveRequest);
                        return true;
                    }            
                }
            }
            return false;
        }

        public bool DenyLeave(int leaveId, string userId)
        {
            var CurrentleaveRequest = unitofWork.LRequest.Get(filter: x=>x.id==leaveId).FirstOrDefault();
            if (CurrentleaveRequest != null)
            {
                var EmpLeave = LeaveRepo.GetEmplyeeLeaveByUserId(userId);
                var LastLeaveRequest = unitofWork.LRequest.Get().LastOrDefault();
                if (EmpLeave != null )
                {
                    LeaveRepo.UpdateEmployeeLeave(EmpLeave);
                    CurrentleaveRequest.LeaveStatus = false;
                    LastLeaveRequest.AppliedleavesCount = LastLeaveRequest.AppliedleavesCount - CurrentleaveRequest.requestDaysNo; 
                    LeaveRepo.updateLeaveRequest(CurrentleaveRequest);
                    LeaveRepo.updateLeaveRequest(LastLeaveRequest);
                    return true;

                    ///in Order to understand the logic  applied here, you must consider that at first as we apply for a leave
                    ///the leaveRequest Table only updates the appliedLeave column which we use for checking if the available leave
                    ///has been exceeded.
                    ///for this part since we update the available leave in the leaveRequest Table , in means tha the last row
                    ///would always hold the value of the most current calculated leave.
                    ///therefore we get the Lastleave and the current leave, then Minus the value of the LastLeave.AppliedLeaveCount
                    ///From the current leaveRequest.AppliedLeaveCount.

                }
            }
            return false;
        }

        public bool CheckIfLeaveIsFinished(string userId, int currentLeave, int AllotedLeave)
        {
            var leave = unitofWork.EmployeeLeave.Get(filter: x => x.UserId == userId).SingleOrDefault();
            var LeaveRequest = unitofWork.LRequest.Get(filter: x => x.UserId == userId).ToList().LastOrDefault();
            if (LeaveRequest != null)
            {
                int totalLeave = (int)LeaveRequest.AppliedleavesCount + currentLeave;
                if (totalLeave > LeaveRequest.Availableleave)
                {
                    return true;
                }
                else if (totalLeave == LeaveRequest.Availableleave)
                {
                    return false;
                }
                else if (totalLeave < LeaveRequest.Availableleave)
                {
                    return false;
                }
            }
            return false;
        }
        public double? GetLeaveAppliedFor(string userId)
        {
            var AppliedForNo = LeaveRepo.GetempLeaveRequestsBtUserId(userId).ToList();
            var currentLeave = AppliedForNo.LastOrDefault();
            if (currentLeave != null)
            {
                return currentLeave.AppliedleavesCount.Value;
            }
            return 0;
        }

        public List<LeaveRequest> GetEmployeePendingLeave(string userId)
        {
            var result = LeaveRepo.GetEmployeePendingLeave(userId).ToList();
            return result ?? null;
        }
        public List<LeaveRequest> GetEmployeeApprovedLeave(string userId)
        {
            var result = LeaveRepo.GetEmployeeApprovedLeaveRequest(userId).ToList();
            return result ?? null;
        }
        public List<LeaveRequest> GetEmployeeDeniedLeave(string userId)
        {
            var result = LeaveRepo.GetEmployeeDeniedLeaveRequest(userId).ToList();
            return result ?? null;
        }
        public List<LeaveRequest> GetEmployeeAllLeave(string userId)
        {
            var result = LeaveRepo.GetEmployeeAllLeaveRequest(userId).ToList();
            return result ?? null;
        }
        public IList<LeaveRequestListItem> AllLeaveRequestForConfirmation(int groupId, int locationId)
        {
            var result = unitofWork.LRequest.Get(filter: x => x.LeaveStatus == null && x.Approval1 == true && x.LocationId == locationId && x.GroupId == groupId)
                .Select(x=> new LeaveRequestListItem
                {
                     From = x.FromDate.Value,
                      To = x.ToDate.Value,
                       LeaveName = x.LeaveName,
                        Reason =x.Reason,
                         RequestDays = x.NoOfDays.Value,
                          FullName = x.UserId,
                           UnitName = unitofWork.employees.Get(filter: y=>y.userId == x.UserId).FirstOrDefault().businessunitId.ToString(),
                            UserId = x.UserId,
                             Id =x.id
                }).ToList();
            result.ForEach(x => x.FullName = unitofWork.employees.Get(y => y.userId == y.userId).FirstOrDefault().FullName);
            result.ForEach(x => x.UnitName = unitofWork.BusinessUnit.GetByID(int.Parse(x.UnitName)).unitname);
            return result;
        }

        public IList<LeaveRequestListItem> GetAllLeaveRequestForManager(string userId)
        {
            var result = unitofWork.LRequest.Get(filter: x => x.RepmangId == userId && x.Approval1 == null)
                   .Select(x => new LeaveRequestListItem
                   {
                       From = x.FromDate.Value,
                       To = x.ToDate.Value,
                       LeaveName = x.LeaveName,
                       Reason = x.Reason,
                       RequestDays = x.NoOfDays.Value,
                       FullName = x.UserId,
                       UnitName = unitofWork.employees.Get(filter: y => y.userId == x.UserId).FirstOrDefault().businessunitId.ToString(),
                       UserId = x.UserId,
                       Id = x.id
                   }).ToList();

            if (result != null)
            {
                result.ForEach(x => x.FullName = unitofWork.employees.Get(y => y.userId == y.userId).FirstOrDefault().FullName);
                result.ForEach(x => x.UnitName = unitofWork.BusinessUnit.GetByID(int.Parse(x.UnitName)).unitname);
                return result;
            }
            return null;
        }
        public bool AllotCollectiveLeave(FormCollection collection) //Change this allot year to a dateTime and also change this to a dictionary
        {
            var leaveAmounts = collection["amount"];
            var Userid = collection["id"];
            var AllotYear = collection["year"];
            var array = new string[] { };
            var allotedDays = leaveAmounts.Split(',');
            var splitId = Userid.Split(',');
            
            double Days = 0;
            int year = 0;
            int.TryParse(AllotYear, out year);
            if (allotedDays != null && splitId != null && year != 0 && AllotYear.Length == 4)
            {
                EmployeeLeave leave = new EmployeeLeave();
                for (int id = 0, allotTime = 0; id < splitId.Length; id++, allotTime++)
                {
                    double.TryParse(allotedDays[allotTime], out Days);
                    double? allotDays = Days;

                    leave.UserId = splitId[id];
                    leave.AllotedYear = int.Parse(AllotYear);
                    leave.EmpLeaveLimit = allotDays != 0 ? allotDays : null;
                    leave.Createdby = HttpContext.Current.User.Identity.GetUserId();
                    leave.Modifiedby = HttpContext.Current.User.Identity.GetUserId();
                    leave.UsedLeaves = null;
                    leave.Isactive = true;
                    leave.IsLeaveTrasnferSet = null;
                    if (leave.EmpLeaveLimit != null)
                    {
                    LeaveRepo.AllotEmployeeLeave(leave);
                    }
                }
                    return true;
            }
            return false;
        }
        public bool AllotOrUpdateIndividualLeave(FormCollection collection) //This would be tested when i get the employee detail page
        {
            string allotDays = collection["amount"].ToString();
            string UserIdToUpdate = collection["id"].ToString();
            string year = collection["year"].ToString();
            int yearOfLeave = 0;
            double amountOfLeave = 0;
            int.TryParse(year, out yearOfLeave);
            double.TryParse(allotDays, out amountOfLeave);
            EmployeeLeave leave = new EmployeeLeave();
            var ExistingLeave = unitofWork.EmployeeLeave.Get(filter: x => x.UserId == UserIdToUpdate).FirstOrDefault();
            if (ExistingLeave != null)
            {
                if (allotDays != null && UserIdToUpdate != null)
                {
                    leave.UserId = UserIdToUpdate;
                    leave.AllotedYear = yearOfLeave;
                    leave.EmpLeaveLimit = amountOfLeave;
                    leave.Createdby = HttpContext.Current.User.Identity.GetUserId();
                    leave.Modifiedby = HttpContext.Current.User.Identity.GetUserId();
                    leave.UsedLeaves = null;
                    leave.Isactive = true;
                    leave.IsLeaveTrasnferSet = null;
                    LeaveRepo.AllotEmployeeLeave(leave);
                    return true;
                }
            }
            else
            {
                ExistingLeave.EmpLeaveLimit += amountOfLeave;
                LeaveRepo.UpdateEmployeeLeave(ExistingLeave);
            }
            return false;
        }
    }
}
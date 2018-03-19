using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace resourceEdge.webUi.Infrastructure
{
    public class LeaveManager
    {
        private ILeaveManagement LeaveRepo;
        IEmployees EmpRepo;
        UnitOfWork unitofWork;
        EmployeeManager EmpLogic;
        public LeaveManager(IEmployees Eparam, ILeaveManagement lParam)
        {
            LeaveRepo = lParam;
            EmpRepo = Eparam;
            unitofWork = new UnitOfWork();
        }
        public EmployeeLeaveType GetLeaveDetails(int id)
        {
            var leave = unitofWork.GetDbContext().LeaveType.Find(id);
            if (leave != null)
            {
                return leave;
            }
            return null;
        }

        public double? GetEmpAvailableLeave(string userId)
        {
            var leave = LeaveRepo.GetEmplyeeLeaveByUserId(userId);
            if (leave != null)
            {
                return leave.EmpLeaveLimit;
            }
            return null;
        }

        public List<LeaveRequest> GetPendingLeaveForManager(string userid)
        {
            var leave = unitofWork.LRequest.Get(filter: x => x.RepmangId == userid && x.LeaveStatus == null).ToList();
            if (leave != null)
            {
                return leave;
            }
            return null;
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
                if (EmpLeave != null)
                {
                    LeaveRepo.UpdateEmployeeLeave(EmpLeave);
                    CurrentleaveRequest.LeaveStatus = false;
                    LastLeaveRequest.AppliedleavesCount = LastLeaveRequest.AppliedleavesCount - CurrentleaveRequest.requestDaysNo; 
                    LeaveRepo.updateLeaveRequest(CurrentleaveRequest);
                    LeaveRepo.updateLeaveRequest(LastLeaveRequest);
                    return true;

                    ///in Order to understand the logic  applied here, you must consider that at first as we apply for a leave
                    ///the leaveRequest db only updates the appliedLeave column which we use for checking if the available leave
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
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public List<LeaveRequest> GetEmployeeApprovedLeave(string userId)
        {
            var result = LeaveRepo.GetEmployeeApprovedLeaveRequest(userId).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public List<LeaveRequest> GetEmployeeDeniedLeave(string userId)
        {
            var result = LeaveRepo.GetEmployeeDeniedLeaveRequest(userId).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public List<LeaveRequest> GetEmployeeAllLeave(string userId)
        {
            var result = LeaveRepo.GetEmployeeAllLeaveRequest(userId).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public List<LeaveRequest> AllLeaveRequestForConfirmation()
        {
           return LeaveRepo.AllLeaveRequestForConfirmation().ToList();
        }
        public bool AllotCollectiveLeave(string[] allotedDays, string[] splitId, string AllotYear, string userId) //Change this allot year to a dateTime and also change this to a dictionary
        {
            if (allotedDays != null && splitId != null)
            {
                EmployeeLeave leave = new EmployeeLeave();
                for (int id = 0, allotTime = 0; id < splitId.Length; id++, allotTime++)
                {
                    leave.UserId = splitId[id];
                    leave.AllotedYear = int.Parse(AllotYear);
                    leave.EmpLeaveLimit = double.Parse(allotedDays[allotTime]);
                    leave.Createdby = userId;
                    leave.Modifiedby = userId;
                    leave.UsedLeaves = null;
                    leave.Isactive = true;
                    leave.IsLeaveTrasnferSet = null;
                    LeaveRepo.AllotEmployeeLeave(leave);
                }
                    return true;
            }
            return false;
        }
        public bool allotIndividualLeave(string allotDays, string id, string year, string userId)
        {
            EmployeeLeave leave = new EmployeeLeave();
            if (allotDays != null && id != null && userId != null)
            {
                leave.UserId = id;
                leave.AllotedYear = int.Parse(year);
                leave.EmpLeaveLimit = double.Parse(allotDays);
                leave.Createdby = userId;
                leave.Modifiedby = userId;
                leave.UsedLeaves = null;
                leave.Isactive = true;
                leave.IsLeaveTrasnferSet = null;
                LeaveRepo.AllotEmployeeLeave(leave);
                return true;
            }
            return false;
        }
    }
}
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace resourceEdge.webUi.Infrastructure
{
    public class LeaveManager
    {
        private ILeaveManagement LeaveRepo;
        UnitOfWork unitofWork;
        EmployeeManager EmpLogic;
        public LeaveManager(ILeaveManagement lParam, EmployeeManager ELParam)
        {
            LeaveRepo = lParam;
            unitofWork = new UnitOfWork();
            EmpLogic = ELParam;
        }
        public EmployeeLeaveTypes GetLeaveDetails(int id)
        {
            var leave = unitofWork.GetDbContext().LeaveType.Find(id);
            if (leave != null)
            {
                return leave;
            }
            return null;
        }

        public double GetEmpAvailableLeave(string userId)
        {
            var leave = unitofWork.GetDbContext().EmpLeaves.Where(X => X.UserId == userId).FirstOrDefault();
            return leave.EmpLeaveLimit.Value;
        }

        public List<LeaveRequest> GetPendingLeaveForManager(string userid)
        {
            var leave = unitofWork.GetDbContext().LeaveRequest.Where(x => x.RepmangId == userid && x.LeaveStatus == null).ToList();
            if (leave != null)
            {
                return leave;
            }
            return null;
        }

        public bool Approveleave(int leaveId, string userId)
        {
            var leaveRequest = unitofWork.GetDbContext().LeaveRequest.Find(leaveId);
            if (leaveRequest != null)
            {
                var EmpLeave = EmpLogic.getEmpLeaveByUserId(userId);
                if (EmpLeave != null)
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
            return false;
        }

        public bool DenyLeave(int leaveId, string userId)
        {
            var CurrentleaveRequest = unitofWork.LRequest.Get().ToList().Find(x => x.id == leaveId);
            if (CurrentleaveRequest != null)
            {
                var EmpLeave = EmpLogic.getEmpLeaveByUserId(userId);
                var LastLeaveReuest = unitofWork.LRequest.Get().LastOrDefault();
                if (EmpLeave != null)
                {
                    //EmpLeave.UsedLeaves = EmpLeave.UsedLeaves - CurrentleaveRequest.requestDaysNo;
                    LeaveRepo.UpdateEmployeeLeave(EmpLeave);
                    CurrentleaveRequest.LeaveStatus = false;
                    LastLeaveReuest.AppliedleavesCount = LastLeaveReuest.AppliedleavesCount - CurrentleaveRequest.requestDaysNo; 
                    LeaveRepo.updateLeaveRequest(CurrentleaveRequest);
                    LeaveRepo.updateLeaveRequest(LastLeaveReuest);
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
            var leave = unitofWork.GetDbContext().EmpLeaves.Where(X => X.UserId == userId).SingleOrDefault();
            var LeaveRequest = unitofWork.GetDbContext().LeaveRequest.Where(x => x.UserId == userId).ToList().LastOrDefault();
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
            var AppliedForNo = unitofWork.GetDbContext().LeaveRequest.Where(x => x.UserId == userId).ToList();
            var currentLeave = AppliedForNo.LastOrDefault();
            if (currentLeave != null)
            {
                return currentLeave.AppliedleavesCount.Value;
            }
            return 0;
        }

        public List<LeaveRequest> GetEmployeePendingLeave(string userId)
        {
            var result = unitofWork.GetDbContext().LeaveRequest.Where(x => x.LeaveStatus == null).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public List<LeaveRequest> GetEmployeeApprovedLeave(string userId)
        {
            var result = unitofWork.GetDbContext().LeaveRequest.Where(x => x.UserId == userId &&  x.LeaveStatus == true).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public List<LeaveRequest> GetEmployeeDeniedLeave(string userId)
        {
            var result = unitofWork.GetDbContext().LeaveRequest.Where(x => x.UserId == userId && x.LeaveStatus == false).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public List<LeaveRequest> GetEmployeeAllLeave(string userId)
        {
            var result = unitofWork.GetDbContext().LeaveRequest.Where(x => x.UserId == userId).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }
    }
}
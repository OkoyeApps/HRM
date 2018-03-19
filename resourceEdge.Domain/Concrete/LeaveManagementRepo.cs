using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class LeaveManagementRepo : ILeaveManagement
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void AddLeaveManagement(LeaveManagement leave)
        {
            unitOfWork.LeaveManagement.Insert(leave);
            unitOfWork.Save();
        }

        public void AllotEmployeeLeave(EmployeeLeave empLeave)
        {
            unitOfWork.GetDbContext().EmployeeLeave.Add(empLeave);
            unitOfWork.Save();
        }

        public IEnumerable<EmployeeLeave> GetAllotedLeave()
        {
            return unitOfWork.GetDbContext().EmployeeLeave.ToList();
        }

        public void EditLeaveManagement(LeaveManagement leave)
        {
            unitOfWork.LeaveManagement.Update(leave);
            unitOfWork.Save();
        }

        public IEnumerable<LeaveManagement> GetLeave()
        {
            return unitOfWork.LeaveManagement.Get();
        }

        public LeaveManagement GetLeaveById(int? id)
        {
            return unitOfWork.LeaveManagement.GetByID(id);
        }

        public void RemoveLeave(int? id)
        {
            unitOfWork.LeaveManagement.Delete(id);
            unitOfWork.Save();
        }

        public void AddLeaveTypes(EmployeeLeaveType leaveType)
        {
            unitOfWork.LeaveType.Insert(leaveType);
            unitOfWork.Save();
        }

        public IEnumerable<EmployeeLeaveType> GetLeaveTypes()
        {
            return unitOfWork.LeaveType.Get();
        }

        public void AddLeaveRequest(LeaveRequest request)
        {
            unitOfWork.LRequest.Insert(request);
            unitOfWork.Save();
        }

        public void updateLeaveRequest(LeaveRequest leaveRequest)
        {
            unitOfWork.LRequest.Update(leaveRequest);
            unitOfWork.Save();
        }

        public void UpdateEmployeeLeave(EmployeeLeave empLeave)
        {
            unitOfWork.EmployeeLeave.Update(empLeave);
            unitOfWork.Save();
        }

        public void EditLeaveRequest(LeaveRequest request)
        {
            unitOfWork.LRequest.Update(request);
            unitOfWork.Save();
        }

        public void DeleteLeaveRequest(int id)
        {
            unitOfWork.LRequest.Delete(id);
            unitOfWork.Save();

        }

        public IEnumerable<LeaveRequest> GetLeaveRequest()
        {
            return unitOfWork.LRequest.Get();
        }

        public EmployeeLeave GetEmplyeeLeaveByUserId(string id)
        {
            var result = unitOfWork.EmployeeLeave.Get(filter: x=>x.UserId == id).FirstOrDefault();
            return result ?? null;

        }
        
        public IEnumerable<EmployeeLeave> GetEmployeeLeaves()
        {
            return unitOfWork.EmployeeLeave.Get();
        }
        public IEnumerable<LeaveRequest> AllLeaveRequestForConfirmation()
        {
            var result = unitOfWork.LRequest.Get(filter: x => x.LeaveStatus == null && x.Approval1 == true).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public IEnumerable<LeaveRequest> GetempLeaveRequestsBtUserId(string userId)
        {
            var result = unitOfWork.LRequest.Get(filter: x => x.UserId == userId);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public IEnumerable<LeaveRequest> GetEmployeePendingLeave(string userId)
        {
            var result = unitOfWork.LRequest.Get(filter: x => x.UserId == userId && x.LeaveStatus == null);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public IEnumerable<LeaveRequest> GetEmployeeApprovedLeaveRequest(string userId)
        {
            var result = unitOfWork.LRequest.Get(filter: x => x.UserId == userId && x.LeaveStatus == true);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public IEnumerable<LeaveRequest> GetEmployeeDeniedLeaveRequest(string userId)
        {
            var result = unitOfWork.LRequest.Get(filter: x => x.UserId == userId && x.LeaveStatus == false);
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public IEnumerable<LeaveRequest> GetEmployeeAllLeaveRequest(string userId)
        {
            var result = unitOfWork.LRequest.Get(filter: x => x.UserId == userId);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public IEnumerable<LeaveRequest> GetLeaveRequestsForManager(string userId)
        {
            var result = unitOfWork.LRequest.Get(filter: x => x.RepmangId == userId);
            if (result != null)
            {
                return result;
            }
            return null;
        }
    }
}

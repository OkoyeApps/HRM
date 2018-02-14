using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Abstracts
{
   public interface ILeaveManagement
    {
        void AddLeaveManagement(LeaveManagement leave);
        void EditLeaveManagement(LeaveManagement leave);
        IEnumerable<LeaveManagement> GetLeave();
        IEnumerable<EmployeeLeaves> GetAllotedLeave();
        void UpdateEmployeeLeave(EmployeeLeaves empLeave);
        void AddLeaveTypes(EmployeeLeaveTypes leaveType);
        IEnumerable<EmployeeLeaveTypes> GetLeaveTypes();
        LeaveManagement GetLeaveById(int? id);
        void RemoveLeave(int? id);
        void AddLeaveRequest(LeaveRequest request);
        IEnumerable<LeaveRequest> GetLeaveRequest();
        void EditLeaveRequest(LeaveRequest request);
        void DeleteLeaveRequest(int id);
        void updateLeaveRequest(LeaveRequest leaveRequest);
        void AllotEmployeeLeave(EmployeeLeaves empLeave);
        EmployeeLeaves GetEmplyeeLeaveByUserId(string userId);

        EdgeDbContext GetDbContext();

    }
}

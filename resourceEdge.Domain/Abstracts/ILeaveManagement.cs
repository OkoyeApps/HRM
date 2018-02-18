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
        LeaveManagement GetLeaveById(int? id);


        void AllotEmployeeLeave(EmployeeLeaves empLeave);
        IEnumerable<EmployeeLeaves> GetEmployeeLeaves();
        IEnumerable<EmployeeLeaves> GetAllotedLeave();
        void UpdateEmployeeLeave(EmployeeLeaves empLeave);
        EmployeeLeaves GetEmplyeeLeaveByUserId(string userId);


        void AddLeaveTypes(EmployeeLeaveTypes leaveType);
        IEnumerable<EmployeeLeaveTypes> GetLeaveTypes();

        
        

        IEnumerable<LeaveRequest> GetLeaveRequest();
        void AddLeaveRequest(LeaveRequest request);
        void updateLeaveRequest(LeaveRequest leaveRequest);
        void EditLeaveRequest(LeaveRequest request);
        IEnumerable<LeaveRequest> AllLeaveRequestForConfirmation();
        IEnumerable<LeaveRequest> GetempLeaveRequestsBtUserId(string userId);
        IEnumerable<LeaveRequest> GetEmployeePendingLeave(string userId);
        IEnumerable<LeaveRequest> GetEmployeeApprovedLeaveRequest(string userId);
        IEnumerable<LeaveRequest> GetEmployeeDeniedLeaveRequest(string userId);
        IEnumerable<LeaveRequest> GetEmployeeAllLeaveRequest(string userId);
        IEnumerable<LeaveRequest> GetLeaveRequestsForManager(string userId);
    }
}

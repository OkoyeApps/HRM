using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace resourceEdge.webUi.Infrastructure
{
    public static class StatManager
    {
        static UnitOfWork unitOfWork;
        static StatManager()
        {
           unitOfWork = new UnitOfWork();
        }

        public static string GetApprovedLeaveCount(string userId)
        {
            var leave = unitOfWork.GetDbContext().LeaveRequest.Where(x => x.UserId == userId && x.LeaveStatus == true).Count().ToString();      
            return leave ??  "0";
        }

        public static string GetDeniedLeaveCount(string userId)
        {
            var leave = unitOfWork.GetDbContext().LeaveRequest.Where(x => x.UserId == userId && x.LeaveStatus == false).Count().ToString();
            
            return leave ?? "0";
        }
        public static string GetPendingLeaveCount(string userId)
        {
            var leave = unitOfWork.GetDbContext().LeaveRequest.Where(x => x.UserId == userId && x.LeaveStatus == null).Count().ToString();
         
            return leave ??  "0";
        }
        public static string GetAllLeave(string userid)
        {
            var leave = unitOfWork.GetDbContext().LeaveRequest.Where(X => X.UserId == userid).Count().ToString();
            return leave ?? "0";
        }
    }
}
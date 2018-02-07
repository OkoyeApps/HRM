using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
   public class LeaveRequest
    {
        public int id { get; set; }
        public string UserId { get; set; }
        public string Reason { get; set; }
        public string ApprovalComment { get; set; }
        public Nullable<int> LeavetypeId { get; set; }
        public string LeaveName { get; set; }
        public Nullable<bool> LeaveDay { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public bool? LeaveStatus { get; set; }
        public string RepmangId { get; set; }
        public string HrId { get; set; }
        public Nullable<double> NoOfDays { get; set; }
        public Nullable<double> AppliedleavesCount { get; set; }
        public int Availableleave { get; set; }
        public int requestDaysNo { get; set; }
        public string createdby { get; set; }
        public string modifiedby { get; set; }
        public Nullable<System.DateTime> createddate { get; set; }
        public Nullable<System.DateTime> modifieddate { get; set; }
        public Nullable<bool> isactive { get; set; }
    }
}

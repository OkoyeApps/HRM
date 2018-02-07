using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
 public class LeaveRequestSummary
    {
        public int id { get; set; }
        public Nullable<int> leave_req_id { get; set; }
        public Nullable<int> user_id { get; set; }
        public string user_name { get; set; }
        public Nullable<int> department_id { get; set; }
        public string department_name { get; set; }
        public Nullable<int> bunit_id { get; set; }
        public string buss_unit_name { get; set; }
        public string reason { get; set; }
        public string approver_comments { get; set; }
        public Nullable<int> leavetypeid { get; set; }
        public string leavetype_name { get; set; }
        public Nullable<bool> leaveday { get; set; }
        public Nullable<System.DateTime> from_date { get; set; }
        public Nullable<System.DateTime> to_date { get; set; }
        public string leavestatus { get; set; }
        public Nullable<int> rep_mang_id { get; set; }
        public string rep_manager_name { get; set; }
        public Nullable<int> hr_id { get; set; }
        public string hr_name { get; set; }
        public Nullable<double> no_of_days { get; set; }
        public Nullable<double> appliedleavescount { get; set; }
        public Nullable<bool> is_sat_holiday { get; set; }
        public Nullable<int> createdby { get; set; }
        public Nullable<int> modifiedby { get; set; }
        public Nullable<System.DateTime> createddate { get; set; }
        public Nullable<System.DateTime> modifieddate { get; set; }
        public Nullable<bool> isactive { get; set; }
    }
}

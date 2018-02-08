using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
 public  class LeaveManagementSummary
    {
        public int id { get; set; }
        public Nullable<int> leavemgmt_id { get; set; }
        public Nullable<int> cal_startmonth { get; set; }
        public string cal_startmonthname { get; set; }
        public Nullable<int> weekend_startday { get; set; }
        public string weekend_startdayname { get; set; }
        public Nullable<int> weekend_endday { get; set; }
        public string weekend_enddayname { get; set; }
        public Nullable<int> businessunit_id { get; set; }
        public string businessunit_name { get; set; }
        public Nullable<int> department_id { get; set; }
        public string department_name { get; set; }
        public Nullable<int> hours_day { get; set; }
        public string is_satholiday { get; set; }
        public string is_halfday { get; set; }
        public string is_leavetransfer { get; set; }
        public string is_skipholidays { get; set; }
        public string descriptions { get; set; }
        public Nullable<int> createdby { get; set; }
        public Nullable<int> modifiedby { get; set; }
        public Nullable<System.DateTime> createddate { get; set; }
        public Nullable<System.DateTime> modifieddate { get; set; }
        public Nullable<bool> isactive { get; set; }
    }
}

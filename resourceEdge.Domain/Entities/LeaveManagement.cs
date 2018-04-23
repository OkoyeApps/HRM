using resourceEdge.Domain.Infrastructures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
   public class LeaveManagement
    {
        [Key]
        public int id { get; set; }
        public string CalStartMonth { get; set; }
        public string WeekendStartDay { get; set; }
        public string WeekendEndDay { get; set; }
        [ForeignKey("BusinessUnit")]
        public int? businessunitId { get; set; }
        public string departmentId { get; set; }
        [ForeignKey("ReportManager")] //Note this is the userId which is represened as the managerid in the Reportmanager table
        public string HrId { get; set; }
        public string HoursDay { get; set; }
        public string IsSatHoliday { get; set; }
        public Answers IsHalfday { get; set; }
        public Answers IsLeaveTransfer { get; set; }
        public Answers IsSkipHolidays { get; set; }
        public string Descriptions { get; set; }
        public string createdby { get; set; }
        public string modifiedby { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> Isactive { get; set; }
        public virtual BusinessUnit BusinessUnit { get; set; }
        public virtual ReportManager ReportManager { get; set; }
    }
}

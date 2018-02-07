using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
   public class RequisitionSummary
    {
        public int Id { get; set; }
        public Nullable<int> ReqId { get; set; }
        public string RequisitionCode { get; set; }
        public Nullable<System.DateTime> OnboardDate { get; set; }
        public Nullable<int> PositionId { get; set; }
        public string PositionName { get; set; }
        public Nullable<int> ReportingId { get; set; }
        public string ReportingManagerName { get; set; }
        public Nullable<int> BusinessunitId { get; set; }
        public string BusinessunitName { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public string department_name { get; set; }
        public Nullable<int> jobtitle { get; set; }
        public string jobtitleName { get; set; }
        public Nullable<int> ReqNoPositions { get; set; }
        public Nullable<int> SelectedMembers { get; set; }
        public Nullable<int> FilledPositions { get; set; }
        public string JobDescription { get; set; }
        public string ReqSkills { get; set; }
        public string ReqQualification { get; set; }
        public string ReqExpYears { get; set; }
        public Nullable<int> EmpType { get; set; }
        public string EmpTypeName { get; set; }
        public Nullable<bool> ReqPriority { get; set; }
        public string AdditionalInfo { get; set; }
        public string ReqStatus { get; set; }
        public string Approver1 { get; set; }
        public string Approver1Name { get; set; }
        public string Approver2 { get; set; }
        public string Approver2Name { get; set; }
        public string Approver3 { get; set; }
        public string Approver3Name { get; set; }
        public string Appstatus1 { get; set; }
        public string Appstatus2 { get; set; }
        public string Appstatus3 { get; set; }
        public string Recruiters { get; set; }
        public Nullable<int> Client_id { get; set; }
        public Nullable<bool> Isactive { get; set; }
        public string CreatedbyName { get; set; }
        public string Createdby { get; set; }
        public string Modifiedby { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

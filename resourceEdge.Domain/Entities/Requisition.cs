using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
   public class Requisition
    {
        public int id { get; set; }
        public int GroupId { get; set; }
        public int LocationId { get; set; }
        public string RequisitionCode { get; set; }
        public DateTime? OnboardDate { get; set; }
        public int? PositionId { get; set; }
        public string ReportingId { get; set; }
        public int? BusinessUnitId { get; set; }
        public int? DepartmentId { get; set; }
        public int? JobTitleId { get; set; }
        public string ReqNoPositions { get; set; }
        public string SelectedMembers { get; set; }
        public string FilledPositions { get; set; }
        public string JobDescription { get; set; }
        public string ReqSkills { get; set; }
        public string ReqQualification { get; set; }
        public string ReqExpYears { get; set; }
        public string EmpType { get; set; }
        public string ReqPriority { get; set; }
        public string AdditionalInfo { get; set; }
        public string ReqStatus { get; set; }
        public string Approver1 { get; set; }
        public string Approver2 { get; set; }
        public string Approver3 { get; set; }
        public bool? AppStatus1 { get; set; }
        public bool? AppStatus2 { get; set; }
        public bool? AppStatus3 { get; set; }
        public string Recruiters { get; set; }
        public string ClientId { get; set; }
        public string Createdby { get; set; }
        public string Modifiedby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? modifiedDate { get; set; }
        public string Isactive { get; set; }
        public BusinessUnit BusinessUnit { get; set; }
        public Departments Department { get; set; }
        public Jobtitle JobTitle { get; set; }
        public Position Position { get; set; }
        public Group Group { get; set; }
        public Location Location { get; set; }
    }
}

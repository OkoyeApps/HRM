using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
   public  class DisciplinaryIncident
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int LocationId { get; set; }
        public string RaisedBy { get; set; }
        public int BusinessUnitId { get; set; }
        public int DepartmentId { get; set; }
        public string EmployeeName { get; set; }
        public int? JobTitle { get; set; }
        public string ReportingManager { get; set; }
        public DateTime DateOfOccurence { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int ViolationId { get; set; }
        public string ViolationDescription { get; set; }
        public int Verdict { get; set; }
        public int EmployeeAppeal { get; set; }
        public string EmployeeStatement { get; set; }
        public int CorrectiveActionId { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public Violation Violation { get; set; }
        public Consequence CorrectiveAction { get; set; }
        public Group Group { get; set; }
        public Location Location { get; set; }
        public Department Department { get; set; }
        public BusinessUnit BusinessUnit { get; set; }
    }
}

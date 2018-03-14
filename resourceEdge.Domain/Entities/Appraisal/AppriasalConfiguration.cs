using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
   public  class AppraisalConfiguration
    {
        public int Id { get; set; }
        public int AppraisalInitializationId { get; set; }
        public int? LocationId { get; set; }
        public int? BusinessUnitId { get; set; }
        public int? DepartmentId { get; set; }
        public int AppraisalStatus { get; set; }
        public int EnableTo { get; set; }
        public string Eligibility { get; set; } // Change this later to use the int as datatype and check for all the types available 
        public string Parameters { get; set; }// Change this later to use the int as datatype and check for all the types available 
        public string Code { get; set; }
        public string LineManager1 { get; set; }
        public string LineManager2 { get; set; }
        public string LineManager3 { get; set; }
        public bool? Enabled { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public AppraisalInitialization AppraisalInitialization { get; set; }
    }
}

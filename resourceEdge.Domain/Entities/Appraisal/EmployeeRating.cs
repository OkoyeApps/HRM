using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
   public class EmployeeRating
    {
        public int Id { get; set; }
        public string PaInitializationId { get; set; }
        public string EemployeeId { get; set; }
        public string EmployeeResponse { get; set; }
        public string ManagerResponse { get; set; }
        public string SkillResponse { get; set; }
        public string LineManager1 { get; set; }
        public string LineManager2 { get; set; }
        public string LineManager3 { get; set; }
        public string LineManager4 { get; set; }
        public string LineManager5 { get; set; }
        public string LineComment1 { get; set; }
        public string LineComment2 { get; set; }
        public string LineComment3 { get; set; }
        public string LineComment4 { get; set; }
        public string LineComment5 { get; set; }
        public Nullable<int> LineRating1 { get; set; }
        public Nullable<int> LineRating2 { get; set; }
        public Nullable<int> LineRating3 { get; set; }
        public Nullable<int> LineRating4 { get; set; }
        public Nullable<int> LineRating5 { get; set; }
        public Nullable<double> ConsolidatedRating { get; set; }
        public string AppraisalStatus { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedbyRole { get; set; }
        public string CreatedByGroup { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedbyRole { get; set; }
        public string ModifiedbyGroup { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> Isactive { get; set; }
    }
}

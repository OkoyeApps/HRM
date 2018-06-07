using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
   public  class Education
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public int EmployeeId { get; set; }
        public int GroupId { get; set; }
        public int LocationId { get; set; }
        public int EducationlevelId { get; set; }
        public string InstitutionName { get; set; }
        public string Course { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }       
        public string CreatedBY { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public Group Group { get; set; }
        public Location Location { get; set; }
        public Employee Employee { get; set; }
        public EducationLevel Educationlevel { get; set; }
    }
}

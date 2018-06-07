using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
    public class TrainingAndCertification
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public int EmployeeId { get; set; }
        public int GroupId { get; set; }
        public int LocationId { get; set; }
        public string CreatedBY { get; set; }
        public string CourseName { get; set; }
        public string CourseLevel { get; set; }
        public string CouserOfferedBy { get; set; }
        public string Description { get; set; }
        public string CertificateName { get; set; }
        public DateTime? IssuedDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public Group Group { get; set; }
        public Location Location { get; set; }
        public Employee Employee { get; set; }
    }
}

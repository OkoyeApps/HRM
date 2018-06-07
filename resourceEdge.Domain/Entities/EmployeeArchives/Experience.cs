using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
    public class Experience
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public int EmployeeId { get; set; }
        public int GroupId { get; set; }
        public int LocationId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyWebsite { get; set; }
        public string Designation { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string ReasonForLeaving { get; set; }
        public string ReferrerName { get; set; }
        public string ReferrerContact { get; set; }
        public string ReferrerEmail { get; set; }
        public string CreatedBY { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public Group Group { get; set; }
        public Location Location { get; set; }
        public Employee Employee { get; set; }
    }
}

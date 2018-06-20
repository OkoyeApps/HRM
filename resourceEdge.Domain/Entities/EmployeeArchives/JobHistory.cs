using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
     public class JobHistory
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public int EmployeeId { get; set; }
        public int GroupId { get; set; }
        public int LocationId { get; set; }
        public int DepartmentId { get; set; }
        public int JobId { get; set; }
        public int positionId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public double AmountRecieved { get; set; }
        public double AmountPaid{ get; set; }
        public Department Department { get; set; }
        public Jobtitle Job { get; set; }
        public Position Position { get; set; }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
    public class Contact
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public int EmployeeId { get; set; }
        public int GroupId { get; set; }
        public int LocationId { get; set; }
        public string Email { get; set; }
        public string PermanentStreet { get; set; }
        public string PermanentCountry { get; set; }
        public string PermanentState { get; set; }
        public string PermanentPostalCode { get; set; }
        public string TemptStreet { get; set; }
        public string TempCountry { get; set; }
        public string TempState { get; set; }
        public string TempPostalCode{ get; set; }
        public string EmergencyName { get; set; }
        public string EmergencyEmail { get; set; }
        public string EmergencyNumber { get; set; }
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

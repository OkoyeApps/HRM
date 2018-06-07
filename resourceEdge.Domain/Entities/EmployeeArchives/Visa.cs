using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
    public class Visa
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public int EmployeeId { get; set; }
        public int GroupId { get; set; }
        public int LocationId { get; set; }
        public string PassportNumber { get; set; }
        public DateTime? IssuedDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string VisaTypeCode { get; set; }
        public int VisaNumber { get; set; }
        public DateTime? VisaIssuedDate { get; set; }
        public DateTime? VisaExpiryDate { get; set; }
        public string oneToNineStatus { get; set; }
        public DateTime oneToNineReviewDate { get; set; }
        public string IssuingAuthority { get; set; }
        public string OneToNightyFourStatus { get; set; }
        public DateTime? OneToNightyFourExpiryDate { get; set; }

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

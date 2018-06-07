using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
    public class CooperateCard
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public int EmployeeId { get; set; }
        public int GroupId { get; set; }
        public int LocationId { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string IssuedBy { get; set; }
        public string Code { get; set; }
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

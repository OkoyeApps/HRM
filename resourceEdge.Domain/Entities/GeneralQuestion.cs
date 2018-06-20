using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
    public class GeneralQuestion
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int? LocationId { get; set; }
        public int? BusinessUnitId { get; set; }
        public int? DepartmentId { get; set; }
        public string Question { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public Group Group { get; set; }
        public Location Location { get; set; }
        public BusinessUnit BusinessUnit { get; set; }
        public Department Department { get; set; }

    }
}

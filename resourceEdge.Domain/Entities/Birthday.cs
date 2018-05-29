using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
 public  class Birthday
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public int GroupId { get; set; }
        public int LocationId { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Group Group { get; set; }
        public Location Location { get; set; }
    }
}

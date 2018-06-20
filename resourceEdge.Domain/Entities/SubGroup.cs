using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
    public class SubGroup
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Descriptions { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Group Group { get; set; }
    }
}

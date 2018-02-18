using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
   public class CareerPath
    {
        public int Id { get; set; }
        public int CarreerId { get; set; }
        public int LevelId { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Careers CarrierName { get; set; }
        public Levels level { get; set; }
    }
}

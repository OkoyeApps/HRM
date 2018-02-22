using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
   public  class Skills
    {
        public int id { get; set; }
        public string SkillName { get; set; }
        public string Description { get; set; }
        public string createdby { get; set; }
        public string CreatedbyRole { get; set; }
        public string CreatedbyGroup { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedbyRole { get; set; }
        public string ModifiedbyGroup { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? Isactive { get; set; }
        public bool? Isused { get; set; }
    }
}

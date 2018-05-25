using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
    public    class SystemAdmin
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string UserID { get; set; }
        public int GroupId { get; set; }
        public int LocationId { get; set; }
        public Group Group { get; set; }
        public Location Location { get; set; }
    }
}

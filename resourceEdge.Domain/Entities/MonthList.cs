using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
   public class MonthList
    {

        public int id { get; set; }
        public string MonthId { get; set; }
        public string MonthCode { get; set; }
        public string Description { get; set; }
        public string Createdby { get; set; }
        public string Modifiedby { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> Isactive { get; set; }
    }
}

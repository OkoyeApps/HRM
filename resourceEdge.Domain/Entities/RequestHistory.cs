using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
   public class RequestHistory
    {
        public int Id { get; set; }
        public string RequestId { get; set; }
        public string Description { get; set; }
        public string EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpProfileimg { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> Createddate { get; set; }
        public Nullable<System.DateTime> Modifieddate { get; set; }
        public Nullable<bool> Isactive { get; set; }
        public string Comments { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
  public class EmployeeLeave
    {
        public int id { get; set; }
        public string UserId { get; set; }
        public double? EmpLeaveLimit { get; set; }
        public Nullable<double> UsedLeaves { get; set; }
        public int AllotedYear { get; set; }
        public string Createdby { get; set; }
        public string Modifiedby { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> Isactive { get; set; }
        public Nullable<bool> IsLeaveTrasnferSet { get; set; }
    }
}

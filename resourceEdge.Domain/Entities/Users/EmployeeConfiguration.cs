using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
    public class EmployeeConfiguration
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string GroupID { get; set; }
        public int SubGroupID { get; set; }
        public int LocationID { get; set; }
        public ICollection<EmployeeUnitDepartment> EmployeeUnitDepartment { get; set; }
    }
}

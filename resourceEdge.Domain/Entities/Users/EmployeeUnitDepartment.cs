using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
    public class EmployeeUnitDepartment
    {
        public int Id { get; set; }
        public int EmployeeID { get; set; }
        public int LocationID { get; set; }
        public int SubGroupID { get; set; }
        public int UnitID { get; set; }
        public int DepartmentId { get; set; }
        public int UserID { get; set; }
        public AppUser User { get; set; }
        public Employee Employee { get; set; }
        public BusinessUnit Unit { get; set; }
        public Department Department { get; set; }
        public SubGroup SubGroup { get; set; }
    }
}

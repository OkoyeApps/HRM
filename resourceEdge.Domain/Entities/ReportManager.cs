using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
   public class ReportManager
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public string ManagerUserId { get; set; }
        [ForeignKey("Departments")]
        public int DepartmentId { get; set; }
        public int BusinessUnitId { get; set; }
        public int employeeId { get; set; }
        public string FullName { get; set; }
        public  Departments Departments { get; set; }

        //Consider adding a manager Id for every employee that is a manager, which would help in getting a list of managers

    }
}

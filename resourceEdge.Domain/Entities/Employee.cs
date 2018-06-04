using resourceEdge.Domain.Entities;
using resourceEdge.Domain.Infrastructures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
   // [MetadataType(typeof(EmployeeViewModel))]
   public class Employee
    {
        [Key]
        public int ID { get; set; }
        public string userId { get; set; }
        public string empEmail { get; set; }
        public int empRoleId { get; set; }
        public int GroupId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? dateOfJoining { get; set; }
        public DateTime? dateOfLeaving { get; set; }
        public  string  empStatusId { get; set; }
        public int BusinessunitId { get; set; }
        public int DepartmentId { get; set; }
        public int JobTitleId { get; set; }
        public int PositionId { get; set; }
        public string yearsExp { get; set; }
        public int LevelId { get; set; }
        public int? LocationId { get; set; }
        public int prefixId { get; set; }
        public string officeNumber { get; set; }
        public string createdby { get; set; }
        public string modifiedby { get; set; }
        public DateTime? createddate { get; set; }
        public DateTime? modifieddate { get; set; }
        public bool? isactive { get; set; }
        public bool? isOrghead { get; set; }
        public int modeofEmployement { get; set; }
        public bool? IsUnithead { get; set; }
        public bool? IsDepthead { get; set; }
        public BusinessUnit Businessunit { get; set; }
        public Jobtitle JobTitle { get; set; }
        public Position Position { get; set; }
        public  Departments Department { get; set; }
        public  Level Level { get; set; }
        public  Group Group { get; set; }
        public Location Location { get; set; }
        
    }
}

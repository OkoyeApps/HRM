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
   public class Employees
    {
        [Key]
        public int empID { get; set; }
        public string userId { get; set; }
        public string empEmail { get; set; }
        public int empRoleId { get; set; }
        public string Firstname { get; set; }
        public string lastName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<System.DateTime> dateOfJoining { get; set; }
        public Nullable<System.DateTime> dateOfLeaving { get; set; }
        public string reportingManager1 { get; set; }
        public string reportingManager2 { get; set; }
        public  string  empStatusId { get; set; }
        public int businessunitId { get; set; }
        public int departmentId { get; set; }
        public int jobtitleId { get; set; }
        public int positionId { get; set; }
        public string yearsExp { get; set; }
        public Nullable<int> holidayGroup { get; set; }
        public prefixes prefixId { get; set; }
        public string extensionNumber { get; set; }
        public string officeNumber { get; set; }
        public string officeFaxnumber { get; set; }
        public string createdby { get; set; }
        public string modifiedby { get; set; }
        public Nullable<System.DateTime> createddate { get; set; }
        public Nullable<System.DateTime> modifieddate { get; set; }
        public Nullable<bool> isactive { get; set; }
        public Nullable<bool> isOrghead { get; set; }
        public ModeOfEmployement modeofEmployement { get; set; }
        public Nullable<bool> IsUnithead { get; set; }
        public Nullable<bool> IsDepthead { get; set; }
        public virtual Departments Departments { get; set; }
        
    }
}

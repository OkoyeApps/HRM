using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
    [MetadataType(typeof(DeptViewModel))]
    public partial class Departments
    {
        [Key]
        public int DeptId { get; set; }
        public string deptname { get; set; }
        public string deptcode { get; set; }
        public string descriptions { get; set; }
        public Nullable<System.DateTime> startdate { get; set; }
        public string country { get; set; }
        public string CurrentState { get; set; }
        public string city { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        [ForeignKey("Employees")]
        public string reportManager1 { get; set; }
        public string reportManager2 { get; set; }
        public Nullable<int> depthead { get; set; }
       [ForeignKey("BusinessUnits")]
        public int BunitId { get; set; }
        public string createdby { get; set; }
        public string modifiedby { get; set; }
        public Nullable<System.DateTime> createddate { get; set; }
        public Nullable<System.DateTime> modifieddate { get; set; }
        public Nullable<bool> isactive { get; set; }
        public virtual BusinessUnits BusinessUnits { get; set; }
        public virtual ICollection<Employees> Employees { get; set; }
    }
}

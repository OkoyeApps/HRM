using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
    public partial class Departments
    {
        [Key]
        public int Id { get; set; }
        public string deptname { get; set; }
        public string deptcode { get; set; }
        public string descriptions { get; set; }
        public int? LocationId { get; set; }
        public int? GroupId { get; set; }
        public DateTime? startdate { get; set; }
        public string reportManager1 { get; set; }
        public string reportManager2 { get; set; }
        public string depthead { get; set; }
       //[ForeignKey("BusinessUnits")]
        public int BusinessUnitsId { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? Isactive { get; set; }
        public  BusinessUnit BusinessUnits { get; set; }
        public Location Location { get; set; }
        public Group Group { get; set; }
        //public virtual ICollection<Employees> Employees { get; set; }
    }
}

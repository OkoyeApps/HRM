using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace resourceEdge.Domain.Entities
{
    //[MetadataType(typeof (BusinessUnitsVIewModel))]
    public class BusinessUnits
    {
        [Key]
        public int BusId { get; set; }
        public string unitname { get; set; }
        public string unitcode { get; set; }
        public string descriptions { get; set; }
        public DateTime? startdate { get; set; }
        [ForeignKey("Employee")]
        public string reportManager1 { get; set; }
        public string reportManager2 { get; set; }
        public int? LocationId { get; set; }
        public string createdby { get; set; }
        public string modifiedby { get; set; }
        public Nullable<System.DateTime> createddate { get; set; }
        public Nullable<System.DateTime> modifieddate { get; set; }
        public Nullable<bool> isactive { get; set; }
        public virtual ICollection<Employees> Employee { get; set; }
        public virtual Location Location { get; set; }
    }
}

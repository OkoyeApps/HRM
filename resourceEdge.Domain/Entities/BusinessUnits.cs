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
    [MetadataType(typeof (BusinessUnitsVIewModel))]
    public class BusinessUnits
    {
        [Key]
        public int BusId { get; set; }
        public string unitname { get; set; }
        public string unitcode { get; set; }
        public string descriptions { get; set; }
        public Nullable<System.DateTime> startdate { get; set; }
        public string country { get; set; }
        public string CurrentState { get; set; }
        public string city { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string unithead { get; set; }
        [ForeignKey("ReportManager")]
        public string reportManager1 { get; set; }
        public string reportManager2 { get; set; }
        public string createdby { get; set; }
        public string modifiedby { get; set; }
        public Nullable<System.DateTime> createddate { get; set; }
        public Nullable<System.DateTime> modifieddate { get; set; }
        public Nullable<bool> isactive { get; set; }
        public virtual ICollection<Employees> ReportManager { get; set; }
    }
}

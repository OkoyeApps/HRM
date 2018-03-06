using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
   //[MetadataType(typeof(positionViewModel))]
   public class Positions
    {
        [Key]
        public int PosId { get; set; }
        public string positionname { get; set; }
        //Remove this later because jobtititle should not be nullable
       [ForeignKey("Jobtitles")]
        public int jobtitleid { get; set; }
        public string description { get; set; }
        public Nullable<int> createdby { get; set; }
        public Nullable<int> modifiedby { get; set; }
        public Nullable<System.DateTime> createddate { get; set; }
        public Nullable<System.DateTime> modifieddate { get; set; }
        public Nullable<bool> isactive { get; set; }
        public virtual Jobtitles Jobtitles { get; set; }
    }
}

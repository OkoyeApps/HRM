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
   public class Position
    {
        [Key]
        public int Id { get; set; }
        public string positionname { get; set; }
        //Remove this later because jobtititle should not be nullable
       //[ForeignKey("Jobtitles")]
        public int JobtitleId { get; set; }
        public string description { get; set; }
        public string createdby { get; set; }
        public string modifiedby { get; set; }
        public Nullable<System.DateTime> createddate { get; set; }
        public Nullable<System.DateTime> modifieddate { get; set; }
        public Nullable<bool> isactive { get; set; }
        public virtual Jobtitle Jobtitle { get; set; }
    }
}

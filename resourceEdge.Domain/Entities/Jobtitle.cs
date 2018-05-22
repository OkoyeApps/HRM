using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
    //[MetadataType(typeof(JobtitlesVIewModel))]
    public class Jobtitle
    {
        [Key]
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int LocationId { get; set; }
        public string jobtitlecode { get; set; }
        public string jobtitlename { get; set; }
        public string jobdescription { get; set; }
        public Nullable<double> minexperiencerequired { get; set; }
        public string jobpaygradecode { get; set; }
        public string jobpayfrequency { get; set; }
        public string comments { get; set; }
        public string createdby { get; set; }
        public string modifiedby { get; set; }
        public Nullable<System.DateTime> createddate { get; set; }
        public Nullable<System.DateTime> modifieddate { get; set; }
        public Nullable<bool> isactive { get; set; }
        public Group Group { get; set; }
        public Location Location { get; set; }
    }
}

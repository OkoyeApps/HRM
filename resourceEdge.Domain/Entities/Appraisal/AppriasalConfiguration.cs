using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities.Appraisal
{
   public  class AppriasalConfiguration
    {
        public int id { get; set; }
        public int Group { get; set; }
        public int? Location { get; set; }
        public int? BusinessUnit { get; set; }
        public int? Department { get; set; }
        public DateTime FromYear { get; set; }
        public DateTime ToYear { get; set; }
        public int  AppraisalMode { get; set; }
        public int  Period { get; set; }
        public int AppraisalStatus { get; set; }
        public int EnableTo { get; set; }
        public DateTime DueDate { get; set; }
        public List<int> Eligibility { get; set; }
        public int Parameters { get; set; }
        public string RatingType { get; set; }
        public bool? Enabled { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}

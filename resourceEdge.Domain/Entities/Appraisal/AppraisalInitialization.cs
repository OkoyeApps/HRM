using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
   public class AppraisalInitialization
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int FromYear { get; set; }
        public int ToYear { get; set; }
        public int AppraisalMode { get; set; }
        public int Period { get; set; }
        public DateTime DueDate { get; set; }
        public string RatingType { get; set; }
        public int AppraisalStatus { get; set; }
        public string InitilizationCode { get; set; }
        public bool? Enable { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Groups Group { get; set; }
    }
}

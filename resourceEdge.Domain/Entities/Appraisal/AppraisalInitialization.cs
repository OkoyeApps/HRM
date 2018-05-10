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
        public int AppraisalModeId { get; set; }
        public int AppraisalPeriodId { get; set; }
        public int AppraisalRatingId { get; set; }
        public int AppraisalStatusId { get; set; }
        public string InitilizationCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? Enable { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public Group Group { get; set; }
        public AppraisalPeriod AppraisalPeriod { get; set; }
        public AppraisalMode AppraisalMode { get; set; }
        public AppraisalRating AppraisalRating { get; set; }
        public AppraisalStatus AppraisalStatus { get; set; }
    }
}

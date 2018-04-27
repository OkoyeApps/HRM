using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
    public class Interview
    {
        public int Id { get; set; }
        //public int CandidateId { get; set; }
        public string Interviewer { get; set; }
        public string LocationId { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public  int InterviewTypeId { get; set; }
        public int InterviewStatusId { get; set; }
        public int RequisitionId { get; set; }
        public DateTime InterviewDate { get; set; }
        public DateTime InterviewTime { get; set; }
        public string InterviewName { get; set; }
        public string FeedBack { get; set; }
        public string FeedBackSummary { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public int UseCount { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Requisition Requisition { get; set; }
        public InterviewType InterviewType { get; set; }
        public InterviewStatus InterviewStatus { get; set; }
        public ICollection<CandidateInterview> CandidateInterview { get; set; }

    }
}

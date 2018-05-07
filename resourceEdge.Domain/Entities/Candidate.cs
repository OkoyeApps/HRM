using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
    public class Candidate
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int RequisitionId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfileImage { get; set; }
        public string Resume { get; set; }
        public string Qualification { get; set; }
        public string EducationSummary { get; set; }
        public int Experience { get; set; }
        public string Skills { get; set; }
        public bool? Status { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public Requisition Requisition { get; set; }
        //public Group Group { get; set; }
        //public CandidateStatus CandidateStatus { get; set; }
    }
}

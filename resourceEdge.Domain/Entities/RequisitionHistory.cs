using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
    public class RequisitionHistory
    {
        public int Id { get; set; }
        public Nullable<int> RequisitionId { get; set; }
        public string CandidateId { get; set; }
        public string CandidateName { get; set; }
        public Nullable<int> InterviewId { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> Isactive { get; set; }
    }
}

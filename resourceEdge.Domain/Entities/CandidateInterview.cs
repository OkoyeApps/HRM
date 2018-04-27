using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
    public class CandidateInterview
    {
        public int ID { get; set; }
        public int InterviewId { get; set; }
        public int CandidateId { get; set; }
        public Interview InterView { get; set; }
        public Candidate Candidate { get; set; }
    }
}

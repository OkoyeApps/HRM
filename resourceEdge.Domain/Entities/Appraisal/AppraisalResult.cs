using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
    public class AppraisalResult
    {
        public int Id { get; set; }
        public int AppraisalInitializationId { get; set; }
        public string UserId { get; set; }
        public double Score { get; set; }
        public AppraisalInitialization AppraisalInitialization { get; set; }
    }
}

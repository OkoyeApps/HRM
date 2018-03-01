using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
   public class SubscribedAppraisal
    {
        public int id { get; set; }
        public string  UserId { get; set; }
        public string Code { get; set; }
        public int Year { get; set; }
        public int Period { get; set; }
        public int AppraisalInitializationId { get; set; }
        public AppraisalInitialization AppraisalInitialization { get; set; }
    }
}

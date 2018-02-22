using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities.Appraisal
{
   public class AppraisalHistory
    {
        public int Id { get; set; }
        public string employeeId { get; set; }
        public int? PainItializationId { get; set; }
        public string Description { get; set; }
        public Nullable<int> descEmpId { get; set; }
        public string DescEmpName { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> Isactive { get; set; }
    }
}

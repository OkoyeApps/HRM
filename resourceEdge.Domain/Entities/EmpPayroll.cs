using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
    public class EmpPayroll
    {
        public EmpPayroll()
        {
        }

        public int Id { get; set; }
        public string BusinessUnit { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public double Deduction { get; set; }
        public string Department { get; set; }
        public string EmpName { get; set; }
        public string EmpStatus { get; set; }
        public double LeaveAllowance { get; set; }
        public double Loan { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public double Reimbursable { get; set; }
        public string Remarks { get; set; }
        public DateTime ResignationDate { get; set; }
        public DateTime ResumptionDate { get; set; }
        public double Salary { get; set; }
        public double Total { get; set; }
        public string UserId { get; set; }
    }
}


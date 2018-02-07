using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Abstracts
{
    public interface IEmploymentStatus
    {
        void AddEmploymentStatus(EmploymentStatus status);
        IEnumerable<EmploymentStatus> GetEmployementStatus();
        EmploymentStatus GetEmployementStatusById(int id);
        void RemoveEmploymentStatus(int id);
    }
}

using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
   public class EmployementStatusRepository : IEmploymentStatus
    {
        UnitofWork.UnitOfWork unitofWork = new UnitofWork.UnitOfWork();

        public void AddEmploymentStatus(EmploymentStatus status)
        {
            unitofWork.employmentStatus.Insert(status);
            unitofWork.Save();
        }
        public IEnumerable<EmploymentStatus> GetEmployementStatus() => unitofWork.employmentStatus.Get();
        public EmploymentStatus GetEmployementStatusById(int id)  => unitofWork.employmentStatus.GetByID(id);
        public void RemoveEmploymentStatus(int id)
        {
            unitofWork.employmentStatus.Delete(id);
            unitofWork.Save();
        }
    }
}

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

        public void Insert(EmploymentStatus status)
        {
            unitofWork.employmentStatus.Insert(status);
            unitofWork.Save();
        }
        public IEnumerable<EmploymentStatus> Get() => unitofWork.employmentStatus.Get();
        public EmploymentStatus GetById(int id)  => unitofWork.employmentStatus.GetByID(id);
        public void Delete(int id)
        {
            unitofWork.employmentStatus.Delete(id);
            unitofWork.Save();
        }

        public void update(EmploymentStatus entity)
        {
            throw new NotImplementedException();
        }

        public EmploymentStatus GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}

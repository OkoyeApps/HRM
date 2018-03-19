using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
   public class CareerRepository : ICareers
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Career> Get() => unitOfWork.Careers.Get();

        public Career GetById(int id) => unitOfWork.Careers.GetByID(id);

        public Career GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void Insert(Career entity)
        {
            unitOfWork.Careers.Insert(entity);
            unitOfWork.Save();
        }

        public void update(Career entity)
        {
            unitOfWork.Careers.Update(entity);
            unitOfWork.Save();
        }
    }
}

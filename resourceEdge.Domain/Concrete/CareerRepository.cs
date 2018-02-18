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

        public IEnumerable<Careers> Get() => unitOfWork.Careers.Get();

        public Careers GetById(int id) => unitOfWork.Careers.GetByID(id);

        public Careers GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void Insert(Careers entity)
        {
            unitOfWork.Careers.Insert(entity);
            unitOfWork.Save();
        }

        public void update(Careers entity)
        {
            unitOfWork.Careers.Update(entity);
            unitOfWork.Save();
        }
    }
}

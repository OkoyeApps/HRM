using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class CareerPathRepository : ICareerPath
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Entities.CareerPath> Get() => unitOfWork.CareerPath.Get();

        public Entities.CareerPath GetById(int id) => unitOfWork.CareerPath.GetByID(id);

        public Entities.CareerPath GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void Insert(Entities.CareerPath entity)
        {
            unitOfWork.CareerPath.Insert(entity);
            unitOfWork.Save();
        }

        public void update(Entities.CareerPath entity)
        {
            unitOfWork.CareerPath.Update(entity);
            unitOfWork.Save();
        }
    }
}

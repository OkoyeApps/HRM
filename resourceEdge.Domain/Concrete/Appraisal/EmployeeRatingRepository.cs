using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class EmployeeRatingRepository : IEmployeeRating
    {
        UnitofWork.UnitOfWork unitofWork = new UnitofWork.UnitOfWork();
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmployeeRating> Get()
        {
           return unitofWork.EmployeeRatings.Get();
        }

        public EmployeeRating GetById(int id)
        {
            throw new NotImplementedException();
        }

        public EmployeeRating GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void Insert(EmployeeRating entity)
        {
            unitofWork.EmployeeRatings.Insert(entity);
            unitofWork.Save();
        }

        public void update(EmployeeRating entity)
        {
            unitofWork.EmployeeRatings.Update(entity);
            unitofWork.Save();
        }
    }
}

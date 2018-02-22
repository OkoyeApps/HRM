using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class RatingRepository : IRating
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AppraisalRating> Get()
        {
            return unitOfWork.Ratings.Get();
        }

        public AppraisalRating GetById(int id)
        {
            return unitOfWork.Ratings.GetByID(id);
        }

        public AppraisalRating GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void Insert(AppraisalRating entity)
        {
            unitOfWork.Ratings.Insert(entity);
            unitOfWork.Save();
        }

        public void update(AppraisalRating entity)
        {
            unitOfWork.Ratings.Update(entity);
            unitOfWork.Save();
        }
    }
}

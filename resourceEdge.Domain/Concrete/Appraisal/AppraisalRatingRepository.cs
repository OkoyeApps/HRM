using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class AppraisalRatingRepository : IAppraisalRating
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AppraisalRating> Get() => unitOfWork.AppraislRating.Get();

        public AppraisalRating GetById(int id)
        {
            throw new NotImplementedException();
        }

        public AppraisalRating GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void Insert(AppraisalRating entity)
        {
            unitOfWork.AppraislRating.Insert(entity);
            unitOfWork.Save();
        }

        public void update(AppraisalRating entity)
        {
            throw new NotImplementedException();
        }
    }
}

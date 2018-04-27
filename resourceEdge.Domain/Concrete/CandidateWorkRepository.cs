using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;

namespace resourceEdge.Domain.Concrete
{
    public class CandidateWorkRepository : ICandidateWorkDetail
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CandidateWorkDetail> Get()
        {
           return unitOfWork.CandidateWorkDetail.Get();
        }

        public CandidateWorkDetail GetById(int id)
        {
           return unitOfWork.CandidateWorkDetail.GetByID(id);
            
        }

        public CandidateWorkDetail GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void Insert(CandidateWorkDetail entity)
        {
            unitOfWork.CandidateWorkDetail.Insert(entity);
            unitOfWork.Save();
        }

        public void update(CandidateWorkDetail entity)
        {
            unitOfWork.CandidateWorkDetail.Update(entity);
            unitOfWork.Save();
        }
    }
}

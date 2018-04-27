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
    class AppraisalResultRepository : IAppraisalResult
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        public void Delete(int id)
        {
          
        }

        public IEnumerable<AppraisalResult> Get()
        {
            return unitOfWork.AppraisalResult.Get();
        }

        public AppraisalResult GetById(int id)
        {
            return unitOfWork.AppraisalResult.GetByID(id);
        }

        public AppraisalResult GetByUserId(string userId)
        {
            return null;
        }

        public void Insert(AppraisalResult entity)
        {
            unitOfWork.AppraisalResult.Insert(entity);
            unitOfWork.Save();
        }

        public void update(AppraisalResult entity)
        {
            
        }
    }
}

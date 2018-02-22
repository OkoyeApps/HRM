using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class AppraisalStatusRepository : IAppraisalStatus
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AppraisalStatus> Get() => unitOfWork.AppraisalStatus.Get();


        public AppraisalStatus GetById(int id)
            {
                throw new NotImplementedException();
            }

        public AppraisalStatus GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void Insert(AppraisalStatus entity)
        {
            unitOfWork.AppraisalStatus.Insert(entity);
            unitOfWork.Save();
        }

        public void update(AppraisalStatus entity)
        {
            throw new NotImplementedException();
        }
    }
}

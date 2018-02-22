using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class AppraisalPeriodRepository : IAppriaslPeriods
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AppraisalPeriods> Get() => unitOfWork.AppraisalPeriod.Get();

        public AppraisalPeriods GetById(int id)
        {
            throw new NotImplementedException();
        }

        public AppraisalPeriods GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void Insert(AppraisalPeriods entity)
        {
            unitOfWork.AppraisalPeriod.Insert(entity);
            unitOfWork.Save();
        }

        public void update(AppraisalPeriods entity)
        {
            throw new NotImplementedException();
        }
    }
}

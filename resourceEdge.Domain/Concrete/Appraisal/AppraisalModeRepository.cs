using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class AppraisalModeRepository : IAppraisalMode
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AppraisalMode> Get() => unitOfWork.AppraisalMode.Get();

        public AppraisalMode GetById(int id)
        {
            throw new NotImplementedException();
        }

        public AppraisalMode GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void Insert(AppraisalMode entity)
        {
             unitOfWork.AppraisalMode.Insert(entity);
            unitOfWork.Save();
        }

        public void update(AppraisalMode entity)
        {
            throw new NotImplementedException();
        }
    }
}

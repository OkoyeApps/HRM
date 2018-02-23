using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class AppraisalInitializationRepository : IAppraisalInitialization
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AppraisalInitialization> Get() => unitOfWork.AppraisalInitialization.Get();

        public AppraisalInitialization GetById(int id) => unitOfWork.AppraisalInitialization.GetByID(id);

        public AppraisalInitialization GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void Insert(AppraisalInitialization entity)
        {
            unitOfWork.AppraisalInitialization.Insert(entity);
            unitOfWork.Save();
        }

        public void update(AppraisalInitialization entity)
        {
            unitOfWork.AppraisalInitialization.Update(entity);
            unitOfWork.Save();
        }
    }
}

using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
   public class AppraisalConfigurationRepository : IAppraisalConfiguration
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AppraisalConfiguration> Get() => unitOfWork.AppraisalConfiguration.Get();

        public AppraisalConfiguration GetById(int id) => unitOfWork.AppraisalConfiguration.GetByID(id);

        public AppraisalConfiguration GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void Insert(AppraisalConfiguration entity)
        {
            unitOfWork.AppraisalConfiguration.Insert(entity);
            unitOfWork.Save();
        }

        public void update(AppraisalConfiguration entity)
        {
            unitOfWork.AppraisalConfiguration.Update(entity);
            unitOfWork.Save();
        }
    }
}

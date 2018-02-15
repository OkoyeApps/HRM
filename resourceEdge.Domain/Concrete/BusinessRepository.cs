using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class BusinessRepository : IBusinessUnits
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void Insert(BusinessUnits businessunit)
        {
            unitOfWork.BusinessUnit.Insert(businessunit);
            unitOfWork.Save();
        }
        public IEnumerable<BusinessUnits> Get() => unitOfWork.BusinessUnit.Get();

        public BusinessUnits GetById(int id) => unitOfWork.BusinessUnit.GetByID(id);
        public void Delete(int id)
        {
            unitOfWork.BusinessUnit.Delete(id);
            unitOfWork.Save();
        }
        public void update(BusinessUnits businessunit)
        {
            unitOfWork.BusinessUnit.Update(businessunit);
            unitOfWork.Save();
        }

        public BusinessUnits GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }
        
    }
}

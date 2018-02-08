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
        public void addbusinessunit(BusinessUnits businessunit)
        {
            unitOfWork.BusinessUnit.Insert(businessunit);
            unitOfWork.Save();
        }
        public IEnumerable<BusinessUnits> GetBusinessUnit() => unitOfWork.BusinessUnit.Get();

        public BusinessUnits GetBusinessUnitById(int? id) => unitOfWork.BusinessUnit.GetByID(id);
        public void RemoveBusinessunit(int id)
        {
            unitOfWork.BusinessUnit.Delete(id);
            unitOfWork.Save();
        }
        public void UpdateBusinessunit(BusinessUnits businessunit)
        {
            unitOfWork.BusinessUnit.Update(businessunit);
            unitOfWork.Save();
        }
    }
}

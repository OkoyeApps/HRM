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
        public void Insert(BusinessUnit businessunit)
        {
            unitOfWork.BusinessUnit.Insert(businessunit);
            unitOfWork.Save();
        }
        public IEnumerable<BusinessUnit> Get() => unitOfWork.BusinessUnit.Get();

        public BusinessUnit GetById(int id) => unitOfWork.BusinessUnit.GetByID(id);
        public void Delete(int id)
        {
            unitOfWork.BusinessUnit.Delete(id);
            unitOfWork.Save();
        }
        public void update(BusinessUnit businessunit)
        {
            unitOfWork.BusinessUnit.Update(businessunit);
            unitOfWork.Save();
        }

        public BusinessUnit GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public BusinessUnit DoesUnitExitByName(string Name)
        {
            var unit = unitOfWork.BusinessUnit.Get(filter: x=> x.unitname.Contains(Name)).FirstOrDefault();
            if (unit != null)
            {
                return unit;
            }
            return null;
        }

        public BusinessUnit GetUnitByLocation(int locationId, string unitName)
        {
            var result = unitOfWork.BusinessUnit.Get(filter: x => x.LocationId == locationId && x.unitname.Contains(unitName)).FirstOrDefault();
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public List<BusinessUnit> GetUnitsByLocation(int locationId)
        {
            var result = unitOfWork.BusinessUnit.Get(filter: x => x.LocationId == locationId).ToList();
            return result ?? null;
        }
    }
}

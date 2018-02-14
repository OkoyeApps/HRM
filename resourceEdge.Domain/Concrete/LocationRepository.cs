using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class LocationRepository : ILocation
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void Delete(int id)
        {
            unitOfWork.Locations.Delete(id);
            unitOfWork.Save();
        }

        public IEnumerable<Location> Get()
        {
           return unitOfWork.Locations.Get();
        }

        public Location GetById(int id)
        {
            return unitOfWork.Locations.GetByID(id);
        }

        public Location GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void Insert(Location entity)
        {
            unitOfWork.Locations.Insert(entity);
            unitOfWork.Save();
        }

        public void update(Location entity)
        {
            unitOfWork.Locations.Update(entity);
            unitOfWork.Save();
        }
    }
}

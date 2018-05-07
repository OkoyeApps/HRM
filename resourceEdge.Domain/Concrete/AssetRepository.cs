using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class AssetRepository : IAsset
    {
      UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void Delete(int id)
        {
            unitOfWork.Asset.Delete(id);
            unitOfWork.Save();
        }

        public IEnumerable<Asset> Get()
        {
            return unitOfWork.Asset.Get();
        }

        public IEnumerable<Asset> GetallAssetLazily(string includeProperties)
        {
            var result = unitOfWork.Asset.Get(includeProperties: includeProperties);
            return result;
        }

        public Asset GetById(int id)
        {
            var result = unitOfWork.Asset.GetByID(id);
            return result;
        }

        public Asset GetByUserId(string userId)
        {
            return null;
        }

        public void Insert(Asset entity)
        {
            unitOfWork.Asset.Insert(entity);
            unitOfWork.Save();
        }

        public void update(Asset entity)
        {
            unitOfWork.Asset.Update(entity);
            unitOfWork.Save();
        }
    }
}

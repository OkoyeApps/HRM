using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class AssetCategoryRepository : IAssetCategory
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void Delete(int id)
        {
            unitOfWork.AssetCategory.Delete(id);
            unitOfWork.Save();
        }

        public IEnumerable<AssetCategory> Get()
        {
            return unitOfWork.AssetCategory.Get();
        }

        public AssetCategory GetById(int id)
        {
            return unitOfWork.AssetCategory.GetByID(id);
        }

        public AssetCategory GetByUserId(string userId)
        {
            return null;
        }

        public void Insert(AssetCategory entity)
        {
            unitOfWork.AssetCategory.Insert(entity);
            unitOfWork.Save();
        }

        public void update(AssetCategory entity)
        {
            unitOfWork.AssetCategory.Update(entity);
            unitOfWork.Save();
        }
    }
}

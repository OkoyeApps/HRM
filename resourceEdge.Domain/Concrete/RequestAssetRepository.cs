using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class RequestAssetRepository : IRequestAsset
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void Delete(int id)
        {
            unitOfWork.RequestAsset.Delete(id);
            unitOfWork.Save();       
        }

        public IEnumerable<RequestAsset> Get()
        {
            return unitOfWork.RequestAsset.Get();
        }

        public RequestAsset GetById(int id)
        {
            var result = unitOfWork.RequestAsset.GetByID(id);
            return result;
        }

        public RequestAsset GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RequestAsset> GetRequestLazily(string includeProperties)
        {
            var result = unitOfWork.RequestAsset.Get(includeProperties: includeProperties);
            return result;
        }

        public void Insert(RequestAsset entity)
        {
            unitOfWork.RequestAsset.Insert(entity);
            unitOfWork.Save();
        }

        public void update(RequestAsset entity)
        {
            unitOfWork.RequestAsset.Update(entity);
            unitOfWork.Save();
        }
    }
}

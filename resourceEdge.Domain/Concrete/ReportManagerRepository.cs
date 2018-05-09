using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Concrete
{
    public class ReportManagerRepository : IReportManager
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void Insert(ReportManager manager)
        {
            unitOfWork.ReportManager.Insert(manager);
            unitOfWork.Save();
        }

        public IEnumerable<ReportManager> Get()
        {
            return unitOfWork.ReportManager.Get();
        }

        public ReportManager GetById(string userId)
        {
            return unitOfWork.GetDbContext().ReportManager.Find(userId);
        }

        public void Delete(string UserId)
        {
            unitOfWork.ReportManager.Delete(UserId);
            unitOfWork.Save();
        }

        public ReportManager GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void update(ReportManager entity)
        {
            throw new NotImplementedException();
        }

        public ReportManager GetByUserId(string userId)
        {
          var result =   unitOfWork.ReportManager.Get(filter: x => x.ManagerUserId == userId).FirstOrDefault();
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public IList<ReportManager> GetManagersByBusinessunit(int id)
        {
            var result = unitOfWork.ReportManager.Get(filter: x => x.BusinessUnitId == id).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public IList<ReportManager> GetReportmanagerCount(string userId)
        {
            var result = unitOfWork.ReportManager.Get(filter: x => x.ManagerUserId == userId).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }


        public IEnumerable<ReportManager> GetManagersByLocationAndGroup(int groupId, int locationId)
        {
            var result = unitOfWork.ReportManager.Get(filter: x => x.GroupId == groupId && x.LocationId == locationId);
            return result;
        }
    }
}

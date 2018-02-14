using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Concrete
{
    public class ReprtManagerRepository : IReportManager
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void Insert(ReportManagers manager)
        {
            unitOfWork.ReportManager.Insert(manager);
            unitOfWork.Save();
        }

        public IEnumerable<ReportManagers> Get()
        {
            return unitOfWork.ReportManager.Get();
        }

        public ReportManagers GetById(string userId)
        {
            return unitOfWork.GetDbContext().ReportManagers.Find(userId);
        }

        public void Delete(string UserId)
        {
            unitOfWork.ReportManager.Delete(UserId);
            unitOfWork.Save();
        }

        public ReportManagers GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void update(ReportManagers entity)
        {
            throw new NotImplementedException();
        }

        public ReportManagers GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}

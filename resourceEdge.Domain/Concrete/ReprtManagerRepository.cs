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
        public void AddReportMananger(ReportManagers manager)
        {
            unitOfWork.ReportManager.Insert(manager);
            unitOfWork.Save();
        }

        public IEnumerable<ReportManagers> GetReportManager()
        {
            return unitOfWork.ReportManager.Get();
        }

        public ReportManagers GetReportManagerById(string userId)
        {
            return unitOfWork.GetDbContext().ReportManagers.Find(userId);
        }

        public void RemoveReportManager(string UserId)
        {
            unitOfWork.ReportManager.Delete(UserId);
            unitOfWork.Save();
        }
    }
}

using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Abstracts
{
    public interface IReportManager
    {
        void AddReportMananger(ReportManagers manager);
        void RemoveReportManager(string UserId);
        IEnumerable<ReportManagers> GetReportManager();
        ReportManagers GetReportManagerById(string id);
    }
}

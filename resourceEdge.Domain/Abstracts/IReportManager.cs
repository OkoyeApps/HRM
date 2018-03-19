using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Abstracts
{
    public interface IReportManager : GenericInterface<ReportManager>
    {
        void Delete(string UserId);
        ReportManager GetById(string id);
        List<ReportManager> GetManagersByBusinessunit(int id);
        List<ReportManager> GetReportmanagerCount(string userId);
    }
}

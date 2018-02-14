using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Abstracts
{
    public interface IReportManager : GenericInterface<ReportManagers>
    {
        void Delete(string UserId);
        ReportManagers GetById(string id);
    }
}

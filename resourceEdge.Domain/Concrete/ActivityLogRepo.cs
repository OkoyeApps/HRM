using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class ActivityLogRepo : IActivityLog
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ActivityLog> Get()
        {
            throw new NotImplementedException();
        }

        public ActivityLog GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ActivityLog GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void Insert(ActivityLog entity)
        {
            unitOfWork.ActivityLogs.Insert(entity);
            unitOfWork.Save();
        }

        public void update(ActivityLog entity)
        {
            throw new NotImplementedException();
        }
    }
}

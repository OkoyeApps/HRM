using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace resourceEdge.webUi.Infrastructure.ActitivityLogs
{
    public class LoggingEvents : System.Web.HttpApplication
    {
        /// <summary>
        /// When writitng filters try not to use the unitofwork dbset because this can be in use by the underlying system 
        /// therefore use a new dbcontext to access the database especially when you are try to update an entry.
        /// </summary>
        private IActivityLog logRepo;
        EdgeDbContext unitofWork = new UnitOfWork().GetDbContext();
        AsyncGenericRepository<ActivityLog> UnitofWork = new AsyncGenericRepository<ActivityLog>(new EdgeDbContext());
      
        public LoggingEvents(IActivityLog logs)
        {
            logRepo = logs;
        }
        public void InsertActivityLogs(List<ActivityLog> activitylogsItem)
        {
            try
            {
                ActivityLog activityLogs = new ActivityLog();
                foreach (var item in activitylogsItem)
                {
                    activityLogs.actionname = item.actionname;
                    activityLogs.controllername = item.controllername;
                    activityLogs.myip = item.myip;
                    activityLogs.parameters = item.parameters;
                    activityLogs.requesturl = item.requesturl;
                    activityLogs.CreatedDate = DateTime.Now;
                    activityLogs.UserId = item.UserId;
                    activityLogs.UserName = item.UserName;
                }
                unitofWork.ActivityLog.Add(activityLogs);
                //unitofWork.Save();
                //logRepo.Insert(activityLogs);
                //UnitofWork.Insert(activityLogs);
                unitofWork.SaveChanges();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
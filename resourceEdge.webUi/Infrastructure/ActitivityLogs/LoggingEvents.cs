using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
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
        private IActivityLog logRepo;
      
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
                logRepo.Insert(activityLogs);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
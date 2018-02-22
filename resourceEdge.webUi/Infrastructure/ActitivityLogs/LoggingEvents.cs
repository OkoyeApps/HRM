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
        public void InsertActivityLogs(List<ActivityLogs> activitylogsItem)
        {
            try
            {
                ActivityLogs activityLogs = new ActivityLogs();
                foreach (var item in activitylogsItem)
                {
                    activityLogs.actionname = item.actionname;
                    activityLogs.controllername = item.controllername;
                    activityLogs.myip = item.myip;
                    activityLogs.parameters = item.parameters;
                    activityLogs.requesturl = item.requesturl;
                    activityLogs.CreatedDate = DateTime.Now;
                    activityLogs.dataparameter = item.dataparameter;
                    activityLogs.UserId = item.UserId;
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
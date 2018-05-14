using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
   public class ActivityLog
    {
        public int Id { get; set; }
        public string controllername { get; set; }
        public string actionname { get; set; }
        public string myip { get; set; }
        public string parameters { get; set; }
        public string HttpMethod { get; set; }
        public string requesturl { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

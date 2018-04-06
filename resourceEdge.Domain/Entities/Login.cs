using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
   public class Login
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string SessionID { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogOutTime { get; set; }
        public bool IsLogIn { get; set; }
        public bool IsLogOut { get; set; }
    }
}

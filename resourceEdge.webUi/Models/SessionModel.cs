using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace resourceEdge.webUi.Models
{
    public class SessionModel
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public int LocationId { get; set; }
        public int GroupId { get; set; }
        public int UnitId { get; set; }
        public DateTime IssuedDate { get; set; }
    }
}
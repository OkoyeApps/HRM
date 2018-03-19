using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
 public  class WeekDay
    {
        public int id { get; set; }
        public string DayName { get; set; }
        public string DayShortCode { get; set; }
        public string DayLongCode { get; set; }
        public string description { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> Isactive { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
  public class Location
    {
        public int Id { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string LocationHead1 { get; set; }
        public string LocationHead2 { get; set; }
        public string LocationHead3 { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
    public class Claim
    {
        public int ID { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}

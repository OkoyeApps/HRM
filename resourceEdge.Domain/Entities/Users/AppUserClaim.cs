using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
    public class AppUserClaim
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string AppUserId { get; set; }
        public int ClaimId { get; set; }
        public Claim Claim { get; set; }
        public AppUser AppUser { get; set; }
    }
}

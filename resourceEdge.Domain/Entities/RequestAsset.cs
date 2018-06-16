using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
    public class RequestAsset
    {
        public int Id { get; set; }
        public int AssetCategoryId { get; set; }
        public int AssetId { get; set; }
        public int GroupId { get; set; }
        public int LocationId { get; set; }
        public int Amount { get; set; }
        public DateTime? DueTime { get; set; }
        public string RequestedBy { get; set; }
        public string createdBy { get; set; }
        public string CreatedByFullName { get; set; }
        public string ModifiedBy { get; set; }
        public bool? IsResolved { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn {get; set; }
        public AssetCategory AssetCategory { get; set; }
        public Asset Asset { get; set; }
    }
}

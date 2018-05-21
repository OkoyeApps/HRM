using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
    public class Asset
    {
        public int ID { get; set; }
        public int GroupId { get; set; }
        public int LocationId { get; set; }
        public string Name { get; set; }
        public long SerialNumber { get; set; }
        public bool IsInUse { get; set; }
        public string ImageUrl { get; set; }
        public int AssetCategoryId { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public AssetCategory AssetCategory { get; set; }
        public Group Group { get; set; }
        public Location Location { get; set; }
    }
}

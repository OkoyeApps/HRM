using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
   public class AppraisalRating
    {
        public int Id { get; set; }
        //public string PaConfiguredId { get; set; }
        //public string PaInitializationId { get; set; }
        public Nullable<int> RatingValue { get; set; }
        public string RatingText { get; set; }
        public string CreatedBy { get; set; }
        //public string CreatedbyRole { get; set; }
        //public Nullable<int> CreatedbyGroup { get; set; }
        public string ModifiedBy { get; set; }
        //public Nullable<int> ModifiedbyRole { get; set; }
        //public Nullable<int> ModifiedbyGroup { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> Isactive { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
   public  class Questions
    {
        public int Id { get; set; }
        public string PaConfiguredId { get; set; }
        public string Question { get; set; }
        public string Description { get; set; }
        public int? BusinessUnitId { get; set; }
        public int? DepartmentId { get; set; }
        public Nullable<bool> Isactive { get; set; }
        public string Createdby { get; set; }
        public Nullable<int> CreatedbyRole { get; set; }
        public Nullable<int> CreatedbyGroup { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<int> ModifiedbyRole { get; set; }
        public Nullable<int> ModifiedbyGroup { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Nullable<bool> Isused { get; set; }
    }
}

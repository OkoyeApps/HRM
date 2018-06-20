using System;

namespace resourceEdge.Domain.Entities
{
    public class ActionMethod
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ControllerID { get; set; }
        public SystemController Controller { get; set; }
        public string CreatedBy { get; set; }
        public DateTime?  CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
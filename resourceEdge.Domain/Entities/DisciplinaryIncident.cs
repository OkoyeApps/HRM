﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
   public  class DisciplinaryIncident
    {
        public int Id { get; set; }
        public int RaisedBy { get; set; }
        public int BusinessUnit { get; set; }
        public int Department { get; set; }
        public string EmployeeName { get; set; }
        public int? JobTitle { get; set; }
        public string ReportingManager { get; set; }
        public DateTime DateOfOccurence { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int ViolationId { get; set; }
        public string ViolationDescription { get; set; }
        public int Verdict { get; set; }
        public int CorrectiveAction { get; set; }
        public int EmployeeAppeal { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public Violation Violation { get; set; }
    }
}
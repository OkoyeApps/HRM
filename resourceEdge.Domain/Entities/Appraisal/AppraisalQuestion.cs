using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
    public class AppraisalQuestion
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int? QuestionId { get; set; }
        public int? GeneralQuestionId { get; set; }
        public int? BusinessUnitId { get; set; }
        public int? DepartmentQuestionId { get; set; }
        public int Answer { get; set; }
        public string LineManager1 { get; set; }
        public string LinrManager2 { get; set; }
        public string LineManager3 { get; set; }
        public int LocationId { get; set; }
        public int GroupId { get; set; }
        public bool? L1Status { get; set; }
        public bool? L2Status { get; set; }
        public bool? L3Status { get; set; }
        public int? EditCount { get; set; }
        public bool? IsSubmitted { get; set; }
        public bool? IsAccepted { get; set; }
        public Question Question { get; set; }
        public GeneralQuestion GeneralQuestion { get; set; }
        public GeneralQuestion DepartmentQuestion { get; set; }
        public AppraisalConfiguration Configuration { get; set; }
    }
}

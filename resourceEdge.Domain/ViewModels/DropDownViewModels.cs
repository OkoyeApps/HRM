using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace resourceEdge.Domain.ViewModels
{
    class DropDownViewModels
    {
    }

    public class SystemViewModel
    {      
        public SelectList Groups { get; set; }
        public SelectList Location { get; set; }
        public SelectList BusinessUnits { get; set; }
        public SelectList Departments { get; set; }
        public SelectList EmploymentStatus { get; set; }
        public SelectList Parameter { get; set; }
        public SelectList Rating { get; set; }
        public SelectList Status { get; set; }
        public SelectList AppraisalMode { get; set; }
    }
    public class HrDropDownViewModel
    {
        public SelectList Groups { get; set; }
        public SelectList Location { get; set; }
        public SelectList Roles { get; set; }
        public SelectList EmploymentStatus { get; set; }
        public SelectList Prefix { get; set; }
        public SelectList Level { get; set; }
        public SelectList Job { get; set; }

    }

    public class AppraiseeDropDown
    {
        [Key,HiddenInput(DisplayValue =false)]
        public int ID { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public string RatingType { get; set; }
        public Dictionary<int,int> PreviousAnswer { get; set; }
        public  IEnumerable<GeneralQuestion> GeneralQuestion { get; set; }
         public IEnumerable<GeneralQuestion> DepartmentQuestion { get; set; }
        public Dictionary<int, int> GeneralPreviousAnswer { get; set; }
        public Dictionary<int, int> DepartmentPreviousAnswer { get; set; }

    }

    public class InterviewDropDown
    {
        public SelectList Requisition { get; set; }
        public SelectList interviewStatus { get; set; }
        public SelectList InterviewType { get; set; }

    }
    public class EditInterviewDropDown
    {
        public SelectList Location { get; set; }
        public SelectList Requisition { get; set; }
        public SelectList InterviewStatus { get; set; }
        public SelectList InterviewType { get; set; }
        public SelectList EligibleInterview { get; set; }
    }

}

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
        public SelectList BusinessUnits { get; set; }
        public SelectList Departments { get; set; }
        public SelectList EmploymentStatus { get; set; }
        public SelectList Parameter { get; set; }
        public SelectList Rating { get; set; }
        public SelectList Status { get; set; }
        public SelectList AppraisalMode { get; set; }
    }

    public class AppraiseeDropDown
    {
        [Key,HiddenInput(DisplayValue =false)]
        public int ID { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public string RatingType { get; set; }
        public Dictionary<int,int> PreviousAnswer { get; set; }
         
    }

}

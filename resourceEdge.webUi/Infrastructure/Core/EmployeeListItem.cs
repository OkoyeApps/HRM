using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace resourceEdge.webUi.Infrastructure.Core
{
    public class EmployeeListItem
    {
        public int Id { get; set; }
        public int empID { get; set; }
        public string userId { get; set; }
        [Required, EmailAddress]
        public string empEmail { get; set; }
        public int empRoleId { get; set; }
        public string FullName { get; set; }
        public string reportingManager1 { get; set; }
        public string reportingManager2 { get; set; }
        public string empStatusId { get; set; }
        public int businessunitId { get; set; }
        public int departmentId { get; set; }
        public int LocationId { get; set; }
        public int GroupId { get; set; }
        public string DepartmentName { get; set; }
        public string BusinessUnitName { get; set; }
        public string LocationName {get;set;}
        public string GroupName { get; set; }
        public BusinessUnit Units { get; set; }
        public Group Group { get; set; }
        public Location Location { get; set; }
        public Departments Department { get; set; }
    }
    public class EmloyeeDetailistItem
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string BusinessUnitName { get; set; }
        public string DepartmentName { get; set; }
        public string ImageUrl { get; set; }
        public bool? Login { get; set; }
        public int EmployeeRole { get; set; }
        public int EmployeeId { get; set; }
        public bool? IsUnitHead { get; set; }
        public string JobTitle { get; set; }
        public string PositionName { get; set; }
    }
    
}
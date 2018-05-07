using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace resourceEdge.webUi.Models.SystemModel
{

    public class BusinessUnitListItem
    {
        public int businessId { get; set; }
        public string BusinessName { get; set; }
    }


    public class DepartmentListItem
    {
        public int deptId { get; set; }
        public int businessUnitId { get; set; }
        public string deptName { get; set; }
    }
    public class PrefixesListItem
    {
        public int prefixId { get; set; }
        public string prefixName { get; set; }
    }

    public class EmploymentStatusListItem
    {
        public int empsId { get; set; }
        public string EmpStatusName { get; set; }
    }

    public class JobListItem
    {
        public int JobId { get; set; }
        public string JobName { get; set; }
    }
    public class PositionListItem
    {
        public int PositionId { get; set; }
        public string PositionName { get; set; }
    }

    public class IdentityCodeListItem
    {
        public int Employeeid { get; set; }
        public string EmployeeCode { get; set; }
        public string RequisitionCode { get; set; }


    }

    public class ReportManagersListItem
    {
        public int roleId { get; set; }
        public string RoleName { get; set; }
    }

    public class LocationListItem
    {
        public int LocationId { get; set; }
        public string Manager1 { get; set; }
        public string Manager2 { get; set; }
        public string Manager3 { get; set; }
        public int GroupId { get; set; }
        public string FullName1 { get; set; }
        public string FullName2 { get; set; }
        public string FullName3 { get; set; }
    }

    public class UnitHeadListItems
    {
        public string UserId { get; set; }
        public string UnitId { get; set; }
        public string FullName { get; set; }
    }

    public class RequisitionListItems
    {
        public int Id { get; set; }
        public string ReqCode { get; set; }
        public string Job { get; set; }
        public string Position { get; set; }
        public string BusinessUnitName { get; set; }
        public string DepartmentName { get; set; }
        public string RaisedBy { get; set; }
        public bool? Status { get; set; }
    }

}
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;
using resourceEdge.webUi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Infrastructure
{
    public static class Apimanager
    {
        ///As at the time of writing this code i was too quick to use the class and property way to return specific data.
        ///This was as a result of been too much in a hurry and i wrote a whole lot of the codes here even without running the application.
        ///For optimization and Maintance sake, The Linq select method should be used to return an anonymous object with specific properties in each entity.
        ///Even if i forget to change this later, anyone who maintains this code should please change it.



        private static UnitOfWork unitOfWork = new UnitOfWork();
        private static ApplicationDbContext db = new ApplicationDbContext();
        public class BusinessUnitListItem
        {
            public int businessId { get; set; }
            public string BusinessName { get; set; }
        }

        public class EmployeeListItem
        {
            public int empID { get; set; }
            public string userId { get; set; }
            public string empEmail { get; set; }
            public int empRoleId { get; set; }
            public string FullName { get; set; }
            public string reportingManager1 { get; set; }
            public string reportingManager2 { get; set; }
            public string empStatusId { get; set; }
            public int businessunitId { get; set; }
            public int departmentId { get; set; }

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

        //calls this method to check if there is an entry in the identityCode model
        public static bool CheckIdentityCodeExists()
        {
            var result = unitOfWork.identityCodes.Get();
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static List<BusinessUnitListItem> GetBusinessUnitList()
        {
            List<BusinessUnitListItem> Result = new List<BusinessUnitListItem>();
            BusinessUnitListItem listItem;
            foreach (var item in unitOfWork.BusinessUnit.Get())
            {
                listItem = new BusinessUnitListItem();
                listItem.businessId = item.BusId;
                listItem.BusinessName = item.unitname;
                Result.Add(listItem);
            }
         
            return Result;
        }

 
        public static List<DepartmentListItem> DepartmentList()
        {
            List<DepartmentListItem> Result = new List<DepartmentListItem>();
            DepartmentListItem listItem;
            foreach (var item in unitOfWork.Department.Get())
            {
                listItem = new DepartmentListItem();
                listItem.deptId = item.DeptId;
                listItem.deptName = item.deptname;
                listItem.businessUnitId = item.BusinessUnits.BusId;
                Result.Add(listItem);
            }
            return Result;
        }

        public static List<DepartmentListItem> DepartmentById(int? id)
        {
            if(id == null)
            {
                return null;
            }
            List<DepartmentListItem> Result = new List<DepartmentListItem>();
            DepartmentListItem listItem;
            foreach (var item in unitOfWork.GetDbContext().departments.Where(x => x.BunitId == id).OrderBy(x => x.deptname))
            {
                listItem = new DepartmentListItem();
                listItem.deptId = item.DeptId;
                listItem.deptName = item.deptname;
                listItem.businessUnitId = item.BunitId;
                Result.Add(listItem);
            }
            return Result;

        }
        public static List<PrefixesListItem> PrefixeList()
        {
            List<PrefixesListItem> Result = new List<PrefixesListItem>();
            PrefixesListItem listItem;
            foreach (var item in unitOfWork.prefix.Get())
            {
                listItem = new PrefixesListItem();
                listItem.prefixId = item.prefixId;
                listItem.prefixName = item.prefixName;
                Result.Add(listItem);
            }
            return Result;
        }

        public static List<EmploymentStatusListItem> empStatusList()
        {
            List<EmploymentStatusListItem> result = new List<EmploymentStatusListItem>();
            EmploymentStatusListItem listItem;
            foreach (var item in unitOfWork.employmentStatus.Get())
            {
                listItem = new EmploymentStatusListItem();
                listItem.empsId = item.empstId;
                listItem.EmpStatusName = item.employemntStatus;
                result.Add(listItem);
            }
            return result;
        }
        public static List<JobListItem> JobList()
        {
            List<JobListItem> Result = new List<JobListItem>();
            JobListItem listItem;
            foreach (var item in unitOfWork.jobTitles.Get())
            {
                listItem = new JobListItem();
                listItem.JobId = item.JobId;
                listItem.JobName = item.jobtitlename;
                Result.Add(listItem);
            }
            return Result;
        }
        public static List<PositionListItem> GetPositionList()
        {
            List<PositionListItem> Result = new List<PositionListItem>();
            PositionListItem listItem;
            foreach (var item in unitOfWork.positions.Get())
            {
                listItem = new PositionListItem();
                listItem.PositionId = item.PosId;
                listItem.PositionName = item.positionname;
                Result.Add(listItem);
            }
            return Result;
        }
        public static IdentityCodeListItem GetIdntityListByGroup(int groupId)
        {
            //List<IdentityCodeListItem> result = new List<IdentityCodeListItem>();
            IdentityCodeListItem listItem = new IdentityCodeListItem();
            try
            {
                var identityCode = unitOfWork.GetDbContext().identityCodes.Where(x=>x.GroupId == groupId).SingleOrDefault();
                if (identityCode != null)
                {
                        listItem.Employeeid = identityCode.codeId;
                        listItem.EmployeeCode = identityCode.employee_code;
                        listItem.RequisitionCode = identityCode.requisition_code;
                    return listItem;
                }
                else
                {
                    return listItem;
                }
            }
            catch (Exception ex)
            {
               throw ex;
            }
        }
        public static List<PositionListItem> GetPositionById(int id)
        {
            List<PositionListItem> result = new List<PositionListItem>();
            PositionListItem listItem;
            var positionsByJobID = unitOfWork.GetDbContext().positions.Where(x => x.Jobtitles.JobId == id).ToList();
            foreach (var item in positionsByJobID)
            {
                listItem = new PositionListItem();
                listItem.PositionId = item.PosId;
                listItem.PositionName = item.positionname;
                result.Add(listItem);
            }
            
            return result;
        }
        public static List<Employees> GetEmpByBusinessUnit(int id)
        {
            var employeeByUnit = unitOfWork.GetDbContext().employees.Where(x => x.businessunitId == id).ToList();
            if (employeeByUnit != null)
            {
                return employeeByUnit;
            }
            return null;
        }
        public static List<Employees> GetEligibleManagerBybBusinessUnit(int id)
        {
            var unit = unitOfWork.BusinessUnit.GetByID(id); //This gets the business unit by ID 
            var employeeByUnit = unitOfWork.GetDbContext().employees.Where(x => x.businessunitId == id && x.Location == unit.LocationId && x.empRoleId != 3 || x.empRoleId != 2 || x.empRoleId != 1).ToList();
            if (employeeByUnit != null)
            {
                return employeeByUnit;
            }
            //although i used 0 to check the location, it might not be best practice but for now its fine
            var employeeWithoutLocation = unitOfWork.GetDbContext().employees.Where(x => x.businessunitId == id && x.Location == 0 && x.empRoleId != 3 || x.empRoleId != 2 || x.empRoleId != 1).ToList();

            return null;
        }


        public static List<Employees> GetReportManagerBusinessUnit(int id)
        {
            var ReportManager = unitOfWork.GetDbContext().ReportManagers.Where(x => x.BusinessUnitId == id).FirstOrDefault();
            if (ReportManager != null)
            {
                var employee = unitOfWork.GetDbContext().employees.Where(x => x.businessunitId == ReportManager.BusinessUnitId && x.empRoleId == 2).ToList();
                if (employee != null)
                {
                    return employee;
                }
            }
            return null;
        }

        public static EmployeeLeaves getEmpLeaveByUserId(string userId)
        {
            var employee = unitOfWork.GetDbContext().EmpLeaves.Where(x => x.UserId == userId).SingleOrDefault();
            if (employee != null)
            {
                return employee;
            }
            return null;
        }

        public static EmployeeLeaveTypes LeaveTypeById(int id)
        {
            var leave = unitOfWork.GetDbContext().LeaveType.Where(x => x.id == id).SingleOrDefault();
            if (leave != null)
            {
                return leave;
            }
            return null;
        }
        public static List<Employees> GetEmployeeByDept(int dept)
        {
            return unitOfWork.GetDbContext().employees.Where(x => x.departmentId == dept).ToList();
        }

        public static Employees GetEmployeeByUserId(string userid)
        {
            var employee = unitOfWork.GetDbContext().employees.Where(x => x.userId == userid).SingleOrDefault();
            if (employee != null)
            {
                return employee;
            }
            return null;
        }

        public static List<EmployeeListItem> GetReportManagrbyUserId(string userId)
        {
            List<EmployeeListItem> managers = new List<EmployeeListItem>();
            var employee = UserManager.getEmployeeIdFromUserTable(userId);
            if (employee != null)
            {
                EmployeeListItem listItem;
                int BuintId = (Int32.Parse(employee.businessunitId));
                var Reportmanager = unitOfWork.GetDbContext().ReportManagers.Where(x => x.BusinessUnitId == BuintId).ToList();
                if (Reportmanager != null)
                {
                    foreach (var item in Reportmanager)
                    {
                        listItem = new EmployeeListItem();
                        var manager = GetEmployeeByUserId(item.managerId);
                        if (manager != null)
                        {
                            listItem.empID = item.employeeId;
                            listItem.businessunitId = item.BusinessUnitId;
                            listItem.departmentId = item.DepartmentId;
                            listItem.userId = item.managerId;
                            listItem.FullName = manager.FullName;
                            managers.Add(listItem);
                        }

                    }
                    return managers;
                }

            }
            return null;
        }

        public static List<LeaveRequest> GetEmployeePendingLeave(string userId)
        {
            var result = unitOfWork.GetDbContext().LeaveRequest.Where(x => x.LeaveStatus == null).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public static List<LeaveRequest> GetEmployeeApprovedLeave(string userId)
        {
            var result = unitOfWork.GetDbContext().LeaveRequest.Where(x => x.UserId == userId && x.LeaveStatus == true).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public static List<LeaveRequest> GetEmployeeDeniedLeave(string userId)
        {
            var result = unitOfWork.GetDbContext().LeaveRequest.Where(x => x.UserId == userId && x.LeaveStatus == false).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public static List<LeaveRequest> GetEmployeeAllLeave(string userId)
        {
            var result = unitOfWork.GetDbContext().LeaveRequest.Where(x => x.UserId == userId).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public static List<Employees> GetUnitMembersBySearch(string userId, string searchString)
        {
            var userUnitId = unitOfWork.employees.Get(x => x.userId == userId).FirstOrDefault();
            string searchparam = searchString.ToLower();
            List<Employees> TeamMembers = new List<Employees>();
            if (searchparam.StartsWith("tenece"))
            {
                var TeamByEmpId = db.Users.Where(x => x.businessunitId == userUnitId.businessunitId.ToString() && x.employeeId == searchparam).FirstOrDefault();
                if (TeamByEmpId != null)
                {
                    var TeamMember = unitOfWork.employees.Get(x => x.userId == TeamByEmpId.Id).SingleOrDefault();
                    TeamMembers.Add(TeamMember);
                    return TeamMembers;
                }
            }
            if (searchparam.Contains("@"))
            {
                TeamMembers = unitOfWork.employees.Get(X => X.businessunitId == userUnitId.businessunitId && X.empEmail.Contains(searchparam)).ToList();
                return TeamMembers;
            }
             TeamMembers = unitOfWork.employees.Get(x => x.businessunitId == userUnitId.businessunitId && x.FullName.Contains(searchparam)).ToList();
           if (TeamMembers != null)
            {
                return TeamMembers;
            }
            var a = GetBusinessunitByLocation(userUnitId.businessunitId);
            
            return null;

        }

        public static List<BusinessUnits> GetBusinessunitByLocation(int? location = null)
        {
            if (location == null)
            {
                var unitWithOutLocation = unitOfWork.BusinessUnit.Get(x => x.LocationId == null).ToList();
                if (unitWithOutLocation != null)
                {
                    return unitWithOutLocation ?? null;
                }
            }
            var units = unitOfWork.BusinessUnit.Get(x => x.LocationId == location).ToList();
            return units ?? null;
        }
        public static List<Location> GetLocationByGroup(int id)
        {
            var result = unitOfWork.GetDbContext().Location.Where(x => x.GroupId == id).ToList();
            return result ?? null;
        }
        public static List<Employees> GetAllHrsByGroup(int id)
        {
            var result = unitOfWork.GetDbContext().employees.Where(X => X.GroupId == id && X.empRoleId == 3).ToList();
            return result ?? null;
        }
        public static List<Months> GetAllMonths()
        {
           return unitOfWork.GetDbContext().Months.ToList();
        }

        public static List<MonthList> GetAllMonthList()
        {
            return unitOfWork.GetDbContext().MonthList.ToList();
        }
        public static List<WeekDays> GetWeekDays()
        {
            return unitOfWork.GetDbContext().WeekDays.ToList();
        }
        public static List<Weeks> GetWeeks()
        {
            return unitOfWork.GetDbContext().Weeks.ToList();
        }

    }

}

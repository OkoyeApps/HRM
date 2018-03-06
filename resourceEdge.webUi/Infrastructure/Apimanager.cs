using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
    public class Apimanager
    {
        ///As at the time of writing this code i was too quick to use the class and property way to return specific data.
        ///This was as a result of been too much in a hurry and i wrote a whole lot of the codes here even without running the application.
        ///For optimization and Maintance sake, The Linq select method should be used to return an anonymous object with specific properties in each entity.
        ///Even if i forget to change this later, anyone who maintains this code should please change it.


        private Rolemanager role;
        private ApplicationDbContext db;
        private ApplicationUserManager userManager;
        private UnitOfWork unitOfWork;
        public Apimanager()
        {
            db = new ApplicationDbContext();
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            unitOfWork = new UnitOfWork();
            role = new Rolemanager();
        }
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

        //calls this method to check if there is an entry in the identityCode model
        public bool CheckIdentityCodeExists()
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


        public List<BusinessUnitListItem> GetBusinessUnitList()
        {
            var result = unitOfWork.GetDbContext().businessunits.Select(x => new BusinessUnitListItem() { businessId = x.BusId, BusinessName = x.unitname }).ToList();
            return result ?? null;
        }

        public List<DepartmentListItem> DepartmentList()
        {
            var result = unitOfWork.GetDbContext().departments.Select(x => new DepartmentListItem() { deptId = x.DeptId, deptName = x.deptname, businessUnitId = x.BunitId }).ToList();
            return result ?? null;
        }

        public List<DepartmentListItem> DepartmentById(int? id)
        {
            if (id == null)
            {
                return null;
            }
            var result = unitOfWork.GetDbContext().departments.Where(x => x.BunitId == id).OrderBy(x => x.deptname).Select(x => new DepartmentListItem { deptId = x.DeptId, deptName = x.deptname, businessUnitId = x.BunitId }).ToList();
            return result ?? null;
        }
        public List<PrefixesListItem> PrefixeList()
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

        public List<EmploymentStatusListItem> empStatusList()
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
        public List<JobListItem> JobList()
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
        public List<PositionListItem> GetPositionList()
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
        public IdentityCodeListItem GetIdntityListByGroup(int groupId)
        {
            //List<IdentityCodeListItem> result = new List<IdentityCodeListItem>();
            IdentityCodeListItem listItem = new IdentityCodeListItem();
            try
            {
                var identityCode = unitOfWork.GetDbContext().identityCodes.Where(x => x.GroupId == groupId).SingleOrDefault();
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
        public List<PositionListItem> GetPositionById(int id)
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
        public List<EmployeeListItem> GetEmpByBusinessUnit(int id)
        {
            var employeeByUnit = unitOfWork.GetDbContext().employees.Where(x => x.businessunitId == id).Select(x=> new EmployeeListItem { userId = x.userId, FullName = x.FullName }).ToList();
            
            return employeeByUnit ?? null;
        }
        public List<Employees> GetEligibleManagerBybBusinessUnit(int id)
        {
            var unit = unitOfWork.BusinessUnit.GetByID(id); //This gets the business unit by ID 
            var employeeByUnit = unitOfWork.GetDbContext().employees.Where(x => x.businessunitId == id && x.LocationId == unit.LocationId && x.empRoleId != 3 && x.empRoleId != 2 && x.empRoleId != 1).ToList();
            if (employeeByUnit != null)
            {
                return employeeByUnit;
            }
            //although i used 0 to check the location, it might not be best practice but for now its fine
            var employeeWithoutLocation = unitOfWork.GetDbContext().employees.Where(x => x.businessunitId == id && x.LocationId == 0 && x.empRoleId != 3 || x.empRoleId != 2 || x.empRoleId != 1).ToList();

            return null;
        }


        public List<Employees> GetReportManagerBusinessUnit(int id)
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

        public EmployeeLeaves getEmpLeaveByUserId(string userId)
        {
            var employee = unitOfWork.GetDbContext().EmpLeaves.Where(x => x.UserId == userId).SingleOrDefault();
            return employee ?? null;
        }

        public EmployeeLeaveTypes LeaveTypeById(int id)
        {
            var leave = unitOfWork.GetDbContext().LeaveType.Where(x => x.id == id).SingleOrDefault();
            if (leave != null)
            {
                return leave;
            }
            return null;
        }
        public List<Employees> GetEmployeeByDept(int dept)
        {
            return unitOfWork.GetDbContext().employees.Where(x => x.departmentId == dept).ToList();
        }

        public Employees GetEmployeeByUserId(string userid)
        {
            var employee = unitOfWork.GetDbContext().employees.Where(x => x.userId == userid).SingleOrDefault();
            if (employee != null)
            {
                return employee;
            }
            return null;
        }

        public List<EmployeeListItem> GetReportManagrbyUserId(string userId)
        {
            List<EmployeeListItem> managers = new List<EmployeeListItem>();
            int? employeeBusinessUnit = unitOfWork.employees.Get(filter: x => x.userId == userId).Select(x => x.businessunitId).FirstOrDefault();
            if (employeeBusinessUnit != null)
            {
                var Reportmanager = unitOfWork.GetDbContext().ReportManagers.Where(x => x.BusinessUnitId == employeeBusinessUnit).
                    Select(x => new EmployeeListItem {
                        empID = x.employeeId,
                        businessunitId = x.BusinessUnitId,
                        departmentId = x.DepartmentId,
                        userId = x.managerId,
                        FullName = x.FullName,

                    }).ToList();

                    return Reportmanager;
                }
            return null;
            }

        public List<LeaveRequest> GetEmployeePendingLeave(string userId)
        {
            var result = unitOfWork.GetDbContext().LeaveRequest.Where(x => x.LeaveStatus == null).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public List<LeaveRequest> GetEmployeeApprovedLeave(string userId)
        {
            var result = unitOfWork.GetDbContext().LeaveRequest.Where(x => x.UserId == userId && x.LeaveStatus == true).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public List<LeaveRequest> GetEmployeeDeniedLeave(string userId)
        {
            var result = unitOfWork.GetDbContext().LeaveRequest.Where(x => x.UserId == userId && x.LeaveStatus == false).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public List<LeaveRequest> GetEmployeeAllLeave(string userId)
        {
            var result = unitOfWork.GetDbContext().LeaveRequest.Where(x => x.UserId == userId).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public List<Employees> GetUnitMembersBySearch(string userId, string searchString)
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

        /// <summary>
        /// All methods from this point are all appraisal specific.
        /// I understand that at some point they may be redundant and maybe repitition of codes.
        /// Taking a look into them we would find out that i used this approach just for clarity and
        /// nothing more. it could be refactored but for readability and reduced complexity i choose
        /// to do it this way...
        /// </summary>
        /// <param name="location"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<BusinessUnits> GetBusinessunitByLocation(int? location = null)
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

        public List<Location> GetLocationByGroup(int id)
        {
            var result = unitOfWork.GetDbContext().Location.Where(x => x.GroupId == id).ToList();
            return result ?? null;
        }
        public List<Employees> GetAllHrsByGroup(int id)
        {
            var result = unitOfWork.GetDbContext().employees.Where(X => X.GroupId == id && X.empRoleId == 3).ToList();
            return result ?? null;
        }

        public AppraisalPeriods GetPeriodForAppraisal(int appraisalModeId)
        {
            var result = unitOfWork.GetDbContext().AppraisalMode.Where(x => x.Id == appraisalModeId).SingleOrDefault();
            var period = unitOfWork.GetDbContext().AppraisalPeriods.ToList();
            if (result != null)
            {
                if (result.Name.StartsWith("Q"))
                {
                    return period.Find(x => x.Name.StartsWith("Q"));
                }
                if (result.Name.StartsWith("H"))
                {
                    return period.Find(x => x.Name.StartsWith("H"));
                }
                if (result.Name.StartsWith("Y"))
                {
                    return period.Find(x => x.Name.StartsWith("Y"));
                }
            }
            return null;
        }

        public Location GetUserLocation(string userId)
        {
            var employee = unitOfWork.GetDbContext().employees.Where(x => x.userId == userId).FirstOrDefault();
            if (employee != null && employee.LocationId != null)
            {
                return employee.Location;
            }
            return null;
        }

        public List<EmployeeListItem> GetDeptHeadByUnit(int id)
        {
            var employee = unitOfWork.GetDbContext().employees.Where(x=>x.businessunitId == id && x.IsDepthead == true).Select(x=> new EmployeeListItem { FullName = x.FullName, userId = x.userId }).ToList();
            
            return employee ?? null;
        }


        public List<EmployeeListItem> GetAllEmployessInGroup(int groupId)
        {
            var allEmployeeInGroup = unitOfWork.employees.Get(x => x.GroupId == groupId).Select(x => new EmployeeListItem { FullName = x.FullName, userId = x.userId }).ToList();
            return allEmployeeInGroup ?? null;
        }
        public LocationListItem GetLocationDetails(int id)
        {
            var result = unitOfWork.GetDbContext().Location.Where(m => m.Id == id).Select(x => new LocationListItem() { GroupId = x.GroupId, LocationId = x.Id, Manager1 = x.LocationHead1, Manager2 = x.LocationHead2, Manager3 = x.LocationHead3 }).FirstOrDefault();
            return result ?? null;
        }
        public LocationListItem GetLocationHeadDetails(int id)
        {
            var result = unitOfWork.GetDbContext().Location.Where(m => m.Id == id).Select(x => new LocationListItem() { GroupId = x.GroupId, LocationId = x.Id, Manager1 = x.LocationHead1, Manager2 = x.LocationHead2, Manager3 = x.LocationHead3 }).FirstOrDefault();
            var resultArray = new string[]
            {
               result.Manager1, result.Manager2, result.Manager3
            };
            Dictionary<string, string> ManagerWithFullname = new Dictionary<string, string>();
            foreach (var item in resultArray)
            {
                if (item != null)
                {
                    if (userManager.IsInRole(item, "Location Head"))
                    {
                        var check = unitOfWork.GetDbContext().employees.Where(x => x.LocationId == id && x.userId == item).Select(x => x.FullName).FirstOrDefault();
                        ManagerWithFullname.Add(item, check);
                    }
                }
            }
                if (result.Manager1 != null && result.Manager1.EndsWith("1") && ManagerWithFullname.ContainsKey(result.Manager1))
                {
                    result.FullName1 = ManagerWithFullname[result.Manager1];
                }
                if (result.Manager2 != null && result.Manager2.EndsWith("2") && ManagerWithFullname.ContainsKey(result.Manager2))
                {
                    result.FullName1 = ManagerWithFullname[result.Manager1];
                }
                if (result.Manager3 != null && result.Manager3.EndsWith("3") && ManagerWithFullname.ContainsKey(result.Manager3))
                {
                    result.FullName3 = ManagerWithFullname[result.Manager3];
                }

            return result ?? null;
        }

        //THis code should be optimized later for exception handling
        public List<UnitHeadListItems> GetUnitHeadsForAppraisal(string QueryString)
        {
           
            if (QueryString.ToLower() != "all")
            {
                int id = 0;
                int.TryParse(QueryString.ToString(), out id);
                var result = unitOfWork.GetDbContext().ReportManagers.Where(X => X.BusinessUnitId == id).Select(x => new UnitHeadListItems { UnitId = x.BusinessUnitId.ToString(), UserId = x.managerId, FullName = x.FullName }).ToList();
                return result ?? null;
            }
            if (QueryString.ToLower() == "all")
            {
                var result = unitOfWork.GetDbContext().ReportManagers.Select(x=> new UnitHeadListItems { UnitId = x.BusinessUnitId.ToString(), UserId = x.managerId, FullName = x.FullName }).ToList();
                return result ?? null;
            }
            return null;
        }

        public List<Months> GetAllMonths()
        {
            return unitOfWork.GetDbContext().Months.ToList();
        }

        public List<MonthList> GetAllMonthList()
        {
            return unitOfWork.GetDbContext().MonthList.ToList();
        }
        public List<WeekDays> GetWeekDays()
        {
            return unitOfWork.GetDbContext().WeekDays.ToList();
        }
        public List<Weeks> GetWeeks()
        {
            return unitOfWork.GetDbContext().Weeks.ToList();
        }

    }

}

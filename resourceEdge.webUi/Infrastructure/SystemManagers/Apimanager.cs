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
using resourceEdge.webUi.Infrastructure.Core;
using resourceEdge.webUi.Models.SystemModel;

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
            userManager = null;/* new ApplicationUserManager(new UserStore<AppUser>(db));*/
            unitOfWork = new UnitOfWork();
            role = new Rolemanager();
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
            var result = unitOfWork.GetDbContext().Businessunit.Select(x => new BusinessUnitListItem() { businessId = x.Id, BusinessName = x.unitname }).ToList();
            return result ?? null;
        }

        public List<DepartmentListItem> DepartmentList()
        {
            var result = unitOfWork.GetDbContext().departments.Select(x => new DepartmentListItem() { deptId = x.Id, deptName = x.deptname, businessUnitId = x.BusinessUnitsId }).ToList();
            return result ?? null;
        }

        public List<DepartmentListItem> DepartmentById(int? id)
        {
            if (id == null)
            {
                return null;
            }
            var result = unitOfWork.GetDbContext().departments.Where(x => x.BusinessUnitsId == id).OrderBy(x => x.deptname).Select(x => new DepartmentListItem { deptId = x.Id, deptName = x.deptname, businessUnitId = x.BusinessUnitsId }).ToList();
            return result ?? null;
        }

        public IEnumerable<DepartmentListItem> GetDepartmentByUnit(int unitId, int? Location= null, int? Group = null)
        {
            IEnumerable<DepartmentListItem> result = null;
            if (Location ==null && Group == null)
            {
                result = unitOfWork.GetDbContext().departments.Where(x => x.BusinessUnitsId == unitId).OrderBy(x => x.deptname).Select(x => new DepartmentListItem { deptId = x.Id, deptName = x.deptname, businessUnitId = x.BusinessUnitsId }).ToList();

            }else if(Location != null && Group != null)
            {
                result = unitOfWork.GetDbContext().departments.Where(x => x.BusinessUnitsId == unitId && x.LocationId == Location && x.GroupId == Group).OrderBy(x => x.deptname).Select(x => new DepartmentListItem { deptId = x.Id, deptName = x.deptname, businessUnitId = x.BusinessUnitsId }).ToList();
            }
            //result = unitOfWork.GetDbContext().departments.Where(x => x.BusinessUnitsId == unitId).OrderBy(x => x.deptname).Select(x => new DepartmentListItem { deptId = x.Id, deptName = x.deptname, businessUnitId = x.BusinessUnitsId }).ToList();
            return result;
        }

        public List<PrefixesListItem> PrefixeList()
        {
            List<PrefixesListItem> Result = new List<PrefixesListItem>();
            PrefixesListItem listItem;
            foreach (var item in unitOfWork.prefix.Get())
            {
                listItem = new PrefixesListItem();
                listItem.prefixId = item.Id;
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
                listItem.JobId = item.Id;
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
                listItem.PositionId = item.Id;
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
                var identityCode = unitOfWork.GetDbContext().IdentityCode.Where(x => x.GroupId == groupId).SingleOrDefault();
                if (identityCode != null)
                {
                    listItem.Employeeid = identityCode.Id;
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
            var positionsByJobID = unitOfWork.GetDbContext().Position.Where(x => x.Jobtitle.Id == id).ToList();
            foreach (var item in positionsByJobID)
            {
                listItem = new PositionListItem();
                listItem.PositionId = item.Id;
                listItem.PositionName = item.positionname;
                result.Add(listItem);
            }

            return result;
        }
        public List<EmployeeListItem> GetEmpByBusinessUnit(int id)
        {
            var employeeByUnit = unitOfWork.GetDbContext().Employee.Where(x => x.BusinessunitId == id).Select(x=> new EmployeeListItem { userId = x.userId, FullName = x.FullName }).ToList();
            
            return employeeByUnit ?? null;
        }

        /// <summary>
        /// this methods is used for the appraisal configurations.
        /// it removes any employee that is already a reportmanager which means 
        /// he is already a unit head
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EmployeeListItem> GetEmployeeForAppraisalConfiguration(int id, char formatter)
        {
            List<EmployeeListItem> employeeByUnit = new List<EmployeeListItem>();
            switch (formatter)
            {
                case 'U':
                    employeeByUnit = GetEmpByBusinessUnit(id);
                    break;
                case 'G':
                    employeeByUnit = unitOfWork.GetDbContext().Employee.Where(x => x.GroupId == id && (x.IsUnithead == false || x.IsUnithead == null)).Select(x => new EmployeeListItem { userId = x.userId, FullName = x.FullName }).ToList();
                    break;
            }   
            List<string> UserIdsToRemove = new List<string>();
            foreach (var item in employeeByUnit)
            {
                var isLocationHead = userManager.IsInRole(item.userId, "Location Head");
                var isUnitHead = userManager.IsInRole(item.userId, "Manager");
                if (isLocationHead || isUnitHead)
                {
                    UserIdsToRemove.Add(item.userId);
                }
            }
            if (UserIdsToRemove.Count > 0)
            {
                foreach (var item in UserIdsToRemove)
                {
                    var user = employeeByUnit.Where(x => x.userId == item).FirstOrDefault();
                    if (user!= null)
                    {
                    employeeByUnit.Remove(user);
                    }
                }
            }
            return employeeByUnit;
        }
        public IEnumerable<dynamic> GetEmployeeByDepartment(int groupId,int locationId, int deptId)
        {
            var Employees = unitOfWork.GetDbContext().Employee.Where(X=>X.LocationId == locationId && X.GroupId == groupId && X.DepartmentId ==deptId)
                .Select(x=> new { Id = x.userId, name = x.FullName });
            return Employees;
        }
        public List<Employee> GetEligibleManagerBybBusinessUnit(int id)
        {
            var unit = unitOfWork.BusinessUnit.GetByID(id); //This gets the business unit by ID 
            var employeeByUnit = unitOfWork.GetDbContext().Employee.Where(x => x.BusinessunitId == id && x.LocationId == unit.LocationId && x.empRoleId != 3 && x.empRoleId != 2 && x.empRoleId != 1).ToList();
            if (employeeByUnit != null)
            {
                return employeeByUnit;
            }
            //although i used 0 to check the location, it might not be best practice but for now its fine
            var employeeWithoutLocation = unitOfWork.GetDbContext().Employee.Where(x => x.BusinessunitId == id && x.LocationId == null && x.empRoleId != 3 || x.empRoleId != 2 || x.empRoleId != 1).ToList();

            return null;
        }


        public List<Employee> GetReportManagerBusinessUnit(int id)
        {
            var ReportManager = unitOfWork.GetDbContext().ReportManager.Where(x => x.BusinessUnitId == id).FirstOrDefault();
            if (ReportManager != null)
            {
                var employee = unitOfWork.GetDbContext().Employee.Where(x => x.BusinessunitId == ReportManager.BusinessUnitId && x.empRoleId == 2).ToList();
                if (employee != null)
                {
                    return employee;
                }
            }
            return null;
        }

        public EmployeeLeave getEmpLeaveByUserId(string userId)
        {
            var employee = unitOfWork.GetDbContext().EmployeeLeave.Where(x => x.UserId == userId).SingleOrDefault();
            return employee ?? null;
        }

        public EmployeeLeaveType LeaveTypeById(int id)
        {
            var leave = unitOfWork.GetDbContext().LeaveType.Where(x => x.id == id).SingleOrDefault();
            if (leave != null)
            {
                return leave;
            }
            return null;
        }
       
       
        public List<Employee> GetEmployeeByDept(int dept)
        {
            return unitOfWork.GetDbContext().Employee.Where(x => x.DepartmentId == dept).ToList();
        }

        public List<Employee> GetNonAllotedEmployeeByDept(int dept)
        {
            var Employees =  unitOfWork.GetDbContext().Employee.Where(x => x.DepartmentId == dept).ToList();
            List<Employee> EligibleEmployeeList = new List<Employee>();
            foreach (var item in Employees)
            {
                var eligibleEmployee = unitOfWork.GetDbContext().EmployeeLeave.Where(x => x.UserId == item.userId).FirstOrDefault();
                if (eligibleEmployee == null)
                {
                    EligibleEmployeeList.Add(item);
                }
            }
            return EligibleEmployeeList ?? null;
        }


        public Employee GetEmployeeByUserId(string userid)
        {
            var employee = unitOfWork.GetDbContext().Employee.Where(x => x.userId == userid).SingleOrDefault();
            if (employee != null)
            {
                return employee;
            }
            return null;
        }

        public List<EmployeeListItem> GetReportManagrbyUserId(string userId)
        {
            List<EmployeeListItem> managers = new List<EmployeeListItem>();
            int? employeeBusinessUnit = unitOfWork.employees.Get(filter: x => x.userId == userId).Select(x => x.BusinessunitId).FirstOrDefault();
            if (employeeBusinessUnit != null)
            {
                var Reportmanager = unitOfWork.GetDbContext().ReportManager.Where(x => x.BusinessUnitId == employeeBusinessUnit).
                    Select(x => new EmployeeListItem {
                        empID = x.employeeId,
                        businessunitId = x.BusinessUnitId,
                        departmentId = x.DepartmentId,
                        userId = x.ManagerUserId,
                        FullName = x.FullName
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

        public List<Employee> GetUnitMembersBySearch(string userId, string searchString)
        {
            var userUnitId = unitOfWork.employees.Get(x => x.userId == userId).FirstOrDefault();
            string searchparam = searchString.ToLower();
            List<Employee> TeamMembers = new List<Employee>();
            if (searchparam.StartsWith("tenece"))
            {
                var TeamByEmpId = db.Users.Where(x => x.BusinessunitId == userUnitId.BusinessunitId.ToString() && x.EmployeeId == searchparam).FirstOrDefault();
                if (TeamByEmpId != null)
                {
                    var TeamMember = unitOfWork.employees.Get(x => x.userId == TeamByEmpId.Id).SingleOrDefault();
                    TeamMembers.Add(TeamMember);
                    return TeamMembers;
                }
            }
            if (searchparam.Contains("@"))
            {
                TeamMembers = unitOfWork.employees.Get(X => X.BusinessunitId == userUnitId.BusinessunitId && X.empEmail.Contains(searchparam)).ToList();
                return TeamMembers;
            }
            TeamMembers = unitOfWork.employees.Get(x => x.BusinessunitId == userUnitId.BusinessunitId && x.FullName.Contains(searchparam)).ToList();
            if (TeamMembers != null)
            {
                return TeamMembers;
            }
            var a = GetBusinessunitByLocation(userUnitId.BusinessunitId);

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
        public List<BusinessUnit> GetBusinessunitByLocation(int? location = null)
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
        public List<Employee> GetAllHrsByGroup(int id)
        {
            var result = unitOfWork.GetDbContext().Employee.Where(X => X.GroupId == id && X.empRoleId == 3).ToList();
            return result ?? null;
        }

        public AppraisalPeriod GetPeriodForAppraisal(int appraisalModeId)
        {
            var result = unitOfWork.GetDbContext().AppraisalMode.Where(x => x.Id == appraisalModeId).SingleOrDefault();
            var period = unitOfWork.GetDbContext().AppraisalPeriod.ToList();
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
            var employee = unitOfWork.GetDbContext().Employee.Where(x => x.userId == userId).FirstOrDefault();
            if (employee != null && employee.LocationId != null)
            {
                return employee.Location;
            }
            return null;
        }

        public List<EmployeeListItem> GetDeptHeadByUnit(int id)
        {
            var employee = unitOfWork.GetDbContext().Employee.Where(x=>x.BusinessunitId == id && x.IsDepthead == true).Select(x=> new EmployeeListItem { FullName = x.FullName, userId = x.userId }).ToList();
            
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
                        var check = unitOfWork.GetDbContext().Employee.Where(x => x.LocationId == id && x.userId == item).Select(x => x.FullName).FirstOrDefault();
                        ManagerWithFullname.Add(item, check);
                    }
                }
            }
                if (result.Manager1 != null && ManagerWithFullname.ContainsKey(result.Manager1))
                {
                    result.FullName1 = ManagerWithFullname[result.Manager1];
                }
                if (result.Manager2 != null  && ManagerWithFullname.ContainsKey(result.Manager2))
                {
                    result.FullName2 = ManagerWithFullname[result.Manager2];
                }
                if (result.Manager3 != null && ManagerWithFullname.ContainsKey(result.Manager3))
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
                var result = unitOfWork.GetDbContext().ReportManager.Where(X => X.BusinessUnitId == id).Select(x => new UnitHeadListItems { UnitId = x.BusinessUnitId.ToString(), UserId = x.ManagerUserId, FullName = x.FullName }).ToList();
                return result ?? null;
            }
            if (QueryString.ToLower() == "all")
            {
                var result = unitOfWork.GetDbContext().ReportManager.Select(x=> new UnitHeadListItems { UnitId = x.BusinessUnitId.ToString(), UserId = x.ManagerUserId, FullName = x.FullName }).ToList();
                return result ?? null;
            }
            return null;
        }

        public IEnumerable<dynamic> GetinterviewerByRequisition(int Id)
        {
            var requisition = unitOfWork.GetDbContext().Requisition.Where(X => X.id == Id).FirstOrDefault();
            if (requisition != null)
            {
                var interviewers = unitOfWork.GetDbContext().Employee.Where(X => X.BusinessunitId == requisition.BusinessUnitId.Value && X.DepartmentId == requisition.DepartmentId)
                    .Select(X=>new { name = X.FullName, Id = X.userId });
                return interviewers;
            }
            return null;
        }

        public IEnumerable<dynamic> GetLocationsWithNoAdmin(int groupid)
        {
            var ExistingAdmins = unitOfWork.SystemAdmin.Get(filter: x => x.GroupId == groupid).Select(x=> new {groupId = x.GroupId, locationId = x.LocationId }).ToList();
            var Locations = unitOfWork.Locations.Get(X => X.GroupId == groupid).Select(x=> new { Id = x.Id, Name = x.State}).ToList();
            List<int> AllIdsToRemove = new List<int>();
            foreach (var item in Locations)
            {
                if (ExistingAdmins.Any(x=>x.locationId == item.Id))
                {
                    AllIdsToRemove.Add(item.Id);
                }
            }
            foreach (var item in AllIdsToRemove)
            {
                var currentLocation = Locations.Where(x => x.Id == item).FirstOrDefault();
                Locations.Remove(currentLocation);
            }
            return Locations;
        }

        public IEnumerable<dynamic> GetAssetByCategory(int categoryId)
        {
            var result = unitOfWork.Asset.Get(filter: x => x.AssetCategoryId == categoryId).Select(x => new {Id = x.ID, name = x.Name });
            return result;
        }

        public List<Month> GetAllMonths()
        {
            return unitOfWork.GetDbContext().Month.ToList();
        }

        public List<MonthList> GetAllMonthList()
        {
            return unitOfWork.GetDbContext().MonthList.ToList();
        }
        public List<WeekDay> GetWeekDays()
        {
            return unitOfWork.GetDbContext().WeekDay.ToList();
        }
        public List<Week> GetWeeks()
        {
            return unitOfWork.GetDbContext().Week.ToList();
        }

    }

}

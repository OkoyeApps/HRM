using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;
using resourceEdge.webUi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace resourceEdge.webUi.Infrastructure
{
    /// <summary>
    /// This class is used around the project as a service layer for interfaceing with the employee
    /// Each method within this class was used to assess distinct informations about the employee and
    /// any related class.
    /// NOte. that although some methods are not actually hitting the employee table
    /// the system considers some actions to be employee related.
    /// when using this class, know that it uses the unit of work to do most of its operations and thereby
    /// is serviced by a single DbContext.
    /// For modification i recommend sticking to this approach inorder to avoid exceptions and multiple DbContext
    /// In cases where the exeptions may occurs you can use the <c>GetDbContext()</c> method from the unit of work
    /// but i recommend strongly that when dealing with an entity that has to be updated ADHERE to this approach.
    /// </summary>


    public class EmployeeManager
    {
        UnitOfWork unitofWork = new UnitOfWork();
        private ILeaveManagement LeaveRepo;
        private IFiles FileRepo;
        private IEmployees EmployeeRepo;
        private IReportManager ReportManagerRepo;
        private IPayroll PayrollRepo;
        private ApplicationDbContext db = new ApplicationDbContext();
        private resourceEdge.webUi.ApplicationUserManager userManager;



        public EmployeeManager()
        {
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            db = new ApplicationDbContext();
        }
        public EmployeeManager(IEmployees eparam)
        {
            EmployeeRepo = eparam;
        }
        public EmployeeManager(IEmployees EParam, IReportManager RParam)
        {
            EmployeeRepo = EParam;
            ReportManagerRepo = RParam;
        }
        public EmployeeManager(IEmployees eParam, ILeaveManagement Lparam)
        {
            EmployeeRepo = eParam;
            LeaveRepo = Lparam;
        }
        public EmployeeManager(IEmployees EParam,IFiles fParam, ILeaveManagement lParam)
        {
            FileRepo = fParam;
            LeaveRepo = lParam;
            EmployeeRepo = EParam;
        }
        public EmployeeManager(IEmployees EParam, IFiles fParam, ILeaveManagement lParam, IPayroll payParam)
        {
            FileRepo = fParam;
            LeaveRepo = lParam;
            EmployeeRepo = EParam;
            PayrollRepo = payParam;
        }

        public EmployeeManager(IEmployees EParam, IFiles fParam, ILeaveManagement lParam, IReportManager RParam)
        {
            FileRepo = fParam;
            LeaveRepo = lParam;
            EmployeeRepo = EParam;
            ReportManagerRepo = RParam;
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


        /// <summary>
        /// Every part of this class is divided into sections.
        /// the methods are organized in order of return type.
        /// This approach was used for code readabilty.
        /// <para name="id"></para>
        /// <para name="userId"></para>
        /// </summary>
        /// <returns></returns>
        public List<Employees> GetAllEmployees()
        {
            //unitofWork.employees.Get().ToList();
            return EmployeeRepo.Get().ToList();
        }
        public List<Employees> GetEmpByBusinessUnit(int id)
        {
            //var employeeByUnit = unitofWork.GetDbContext().employees.Where(x => x.businessunitId == id).ToList();
            var employeeByUnit = EmployeeRepo.GetEmpByBusinessUnit(id);
            if (employeeByUnit != null)
            {
                return employeeByUnit;
            }
            return null;
        }
        public List<Employees> GetReportManagerBusinessUnit(int id)
        {
            //var context = unitofWork.GetDbContext();
            List<Employees> managers = new List<Employees>();
            //var ReportManager = context.ReportManagers.Where(x => x.BusinessUnitId == id).FirstOrDefault(); //Fix this when the reportmanager is fixed
            var ReportManagers = ReportManagerRepo.GetManagersByBusinessunit(id);
            if (ReportManagers != null)
            {
                var employees = EmployeeRepo.GetUnitHead(ReportManagers.FirstOrDefault().BusinessUnitId);
                if (employees != null)
                {
                    return employees;
                }              
                EmployeeRepo.GetReportManagers(ReportManagers.FirstOrDefault().managerId, ReportManagers.FirstOrDefault().BusinessUnitId);
                // var employee = context.employees.Where(x => x.businessunitId == ReportManager.BusinessUnitId && x.empRoleId == 2).ToList();
               // var employee = EmployeeRepo.GetReportManagers();
                //if (employee != null)
                //{
                //    return employee;
                //}
            }
            return null;
        }
        public Employees GetEmployeeByUserId(string userid)
        {
            //var employee = unitofWork.GetDbContext().employees.Where(x => x.userId == userid).SingleOrDefault();
            return EmployeeRepo.GetByUserId(userid);
        }
        //Change This later to the interface
        public bool CheckIfEmployeeExistByUserId(string userId)
        {
            //var employee = unitofWork.GetDbContext().employees.Any(x => x.userId == userId);
            return EmployeeRepo.CheckIfEmployeeExistByUserId(userId);
        }
        //Change This later to the interface
        public List<Employees> GetEmployeeByDept(int dept)
        {
            //return unitofWork.GetDbContext().employees.Where(x => x.departmentId == dept).ToList();
            return EmployeeRepo.GetEmployeeByDepts(dept);
        }

        public List<Employees> GetUnitHeads(int unitId)
        {
             //var unitHead = unitofWork.GetDbContext().employees.Where(x => x.businessunitId == unitId && x.IsUnithead == true).FirstOrDefault();
            return EmployeeRepo.GetUnitHead(unitId);
        }

        public List<Employees> GetHr()
        {
            //var hr = unitofWork.GetDbContext().employees.Where(x => x.empRoleId == 3 && x.businessunitId == unitId).SingleOrDefault();
                return EmployeeRepo.GetHrs();
        }

        public List<Employees> GetEmployeeUnitMembers(int unitId)
        {
            //var members = unitofWork.GetDbContext().employees.Where(x => x.businessunitId == unitId && x.IsUnithead != true).ToList();
            var members = EmployeeRepo.GetEmployeeUnitMembers(unitId);
            return members;
        }

        //Although this method originally called by the ApiManager, its still left here just if there is a need to use it
        public List<Employees> GetUnitMembersBySearch(string userId, string searchString)
        {
            //var userUnitId = unitofWork.GetDbContext().employees.Where(x => x.userId == userId).SingleOrDefault();
            var userUnitId = EmployeeRepo.GetByUserId(userId);
            List<Employees> TeamMembers = new List<Employees>();
            if (searchString.ToLower().StartsWith("tenece"))
            {
                var TeamByEmpId = db.Users.Where(x => x.businessunitId == userUnitId.businessunitId.ToString() && x.employeeId == searchString).FirstOrDefault();
                if (TeamByEmpId != null)
                {
                    //var TeamMember = unitofWork.GetDbContext().employees.Where(x => x.userId == TeamByEmpId.Id).SingleOrDefault();
                    var TeamMember = EmployeeRepo.GetByUserId(TeamByEmpId.Id);
                    TeamMembers.Add(TeamMember);
                    return TeamMembers;
                }
            }
           // TeamMembers = unitofWork.GetDbContext().employees.Where(x => x.businessunitId == userUnitId.businessunitId).Where(x => x.empEmail.Contains(searchString) || x.FullName.Contains(searchString)).ToList();
            TeamMembers = EmployeeRepo.GetEmpByBusinessUnit(userUnitId.businessunitId).Where(x => x.empEmail.Contains(searchString) || x.FullName.Contains(searchString)).ToList();
            if (TeamMembers != null)
            {
                return TeamMembers;
            }

            return null;

        }

        /// <summary>
        /// This section is the EmployeeLeaves section
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        //public EmployeeLeaves getEmpLeaveByUserId(string userId)
        //{
        //    //var employee = unitofWork.GetDbContext().EmpLeaves.Where(x => x.UserId == userId).SingleOrDefault();
        //    var employee = lea
        //    if (employee != null)
        //    {
        //        return employee;
        //    }
        //    return null;
        //}
        public EmployeeLeaveTypes LeaveTypeById(int id)
        {
            var leave = unitofWork.GetDbContext().LeaveType.Where(x => x.id == id).SingleOrDefault();
            if (leave != null)
            {
                return leave;
            }
            return null;
        }

        /// <summary>
        /// This section is the Report Manager section
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>

        public ReportManagers getReportManagerByUserId(string userId)
        {
            //var manager = unitofWork.GetDbContext().ReportManagers.Where(x => x.managerId == userid).SingleOrDefault();
            var manager = ReportManagerRepo.GetByUserId(userId);
            if (manager != null)
            {
                return manager;
            }
            return null;
        }

        public List<EmployeeListItem> GetReportManagrbyUserId(string userId)
        {
            List<EmployeeListItem> managers = new List<EmployeeListItem>();
            var employee = UserManager.getEmployeeIdFromUserTable(userId); //Check this check actually checks the user table
            if (employee != null)
            {
                EmployeeListItem listItem;
                int BuintId = (Int32.Parse(employee.businessunitId));
                //var Reportmanager = unitofWork.GetDbContext().ReportManagers.Where(x => x.BusinessUnitId == BuintId).ToList();
                var Reportmanager = ReportManagerRepo.GetManagersByBusinessunit(BuintId);
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
        public bool DoesReportManagerExist(string userId, int UnitId)
        {
            // var manager = unitofWork.GetDbContext().ReportManagers.Where(x => x.managerId == userId).ToList();
            var manager = ReportManagerRepo.GetManagersByBusinessunit(UnitId).Find(x=>x.managerId == userId);
            //if (manager.Count > 1)
            //{
            //    List<int> bUnit = new List<int>();
            //    int businessunit;
            //    foreach (var item in manager)
            //    {
            //        businessunit = item.BusinessUnitId;
            //        bUnit.Add(businessunit);
            //        if (!bUnit.Contains(businessunit))
            //        {
            //            return false;
            //        }
            //        return true;
            //    }

            //    return true;
            //}
            if (manager != null)
            {
                return true;
            }
            return false;
        }

        public class EmployeeEdit : EmployeeManager
        {

            /// <summary>
            /// This class was used to only service the Edit page for the employee. realy for abstractions
            /// This step was used because a lot of unrelated data were needed and instead of having 
            /// This section actually abstracts the code from the controller into this section
            /// </summary>
            /// <param name="model"></param>
            /// <param name="userId"></param>
            /// <param name="currentAvater"></param>
            /// <param name="filefullName"></param>
            /// <param name="File"></param>
            /// <param name="fileRepo"></param>
            /// 
            public EmployeeEdit()
            {
            }
            public EmployeeEdit(IEmployees eParam, IFiles fParam, ILeaveManagement lParam, IPayroll payParam) : base(eParam, fParam,lParam,payParam)
            {

            }
            public void AddOrUpdateAvater(Files model, string userId, Files currentAvater, string filefullName, HttpPostedFileBase File, IFiles fileRepo)
            {
                if (currentAvater == null)
                {

                    string fileName = Path.GetFileNameWithoutExtension(File.FileName);
                    string extenstion = Path.GetExtension(File.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssff") + extenstion;
                    model.FilePath = "~/Files/Avatars/" + fileName;
                    fileName = Path.Combine(filefullName + fileName);
                    model.FileType = FileType.Avatar;
                    File.SaveAs(fileName);
                    var file = new Files()
                    {
                        FileName = model.FileName,
                        FilePath = model.FilePath,
                        UserId = userId
                    };
                    fileRepo.Insert(model);
                }
                else
                {

                    string fileName = Path.GetFileNameWithoutExtension(File.FileName);
                    string extenstion = Path.GetExtension(File.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssff") + extenstion;
                    model.FilePath = "~/Files/Avatars/" + fileName;
                    fileName = Path.Combine(filefullName + fileName);
                    model.FileType = FileType.Avatar;
                    File.SaveAs(fileName);
                    currentAvater.FileName = model.FileName;
                    currentAvater.FilePath = model.FilePath;
                    fileRepo.update(currentAvater);
                }
            }

            public string getEmpAvatar(string userId)
            {
                //Update this method so you can check the file type later
                // return unitofWork.GetDbContext().Files.Where(x => x.UserId == userId && x.FileType == FileType.Avatar).Select(x => x.FilePath).SingleOrDefault();
                var avatar = FileRepo.GetFileByUserId(userId, FileType.Avatar);
                if (avatar != null)
                {
                    return avatar.FilePath.ToString();
                }
                return null;
            }

            public void AddORUpdateSalary(string userId, PayrollViewModel entity, Employees employee, ApplicationUser currentUser, string systemUserId)
            {
                var CurrentPayRoll = unitofWork.GetDbContext().Payroll.Where(x => x.UserId == userId).FirstOrDefault();

                if (CurrentPayRoll != null)
                {
                    CurrentPayRoll.BusinessUnit = entity.BusinessUnit;
                    CurrentPayRoll.Deduction = entity.Deduction;
                    CurrentPayRoll.LeaveAllowance = entity.LeaveAllowance;
                    CurrentPayRoll.Loan = entity.Loan;
                    CurrentPayRoll.Reimbursable = entity.Reimbursable;
                    CurrentPayRoll.Salary = entity.Salary;
                    CurrentPayRoll.Total = entity.Total;
                    PayrollRepo.update(CurrentPayRoll);
                    //unitofWork.PayRoll.Update(CurrentPayRoll);
                    //unitofWork.Save();
                }
                else
                {
                    EmpPayroll payroll = new EmpPayroll();
                    payroll.BusinessUnit = employee.businessunitId.ToString();
                    payroll.Department = employee.departmentId.ToString();
                    payroll.EmpName = employee.FullName;
                    payroll.UserId = currentUser.Id;
                    payroll.EmpStatus = employee.empStatusId;
                    payroll.Deduction = entity.Deduction;
                    payroll.LeaveAllowance = entity.LeaveAllowance;
                    payroll.Reimbursable = entity.Reimbursable;
                    payroll.ResignationDate = employee.dateOfLeaving.Value;
                    payroll.ResumptionDate = employee.dateOfJoining.Value;
                    payroll.Loan = entity.Loan;
                    payroll.Salary = entity.Salary;
                    payroll.Total = entity.Total;
                    payroll.CreatedBy = systemUserId;
                    payroll.ModifiedBy = systemUserId;
                    payroll.CreatedDate = DateTime.Now;
                    payroll.ModifiedDate = DateTime.Now;
                    payroll.Remarks = entity.Remarks;
                    PayrollRepo.Insert(payroll);
                    //unitofWork.PayRoll.Insert(payroll);
                    //unitofWork.Save();
                }
            }

        }


        //Modify this class later to use the unit of work pattern later in the code.
        public class EmployeeDetails : EmployeeManager
        {
            /// <summary>
            /// This class was used to only service the Details page for the employee.
            /// This step was used because a lot of unrelated data were needed and instead of having 
            /// to hit the db always and using viewBags, this approach was used.
            /// Although the objecct return by this class is realy verbose therefore you might not want to initialize it always
            /// </summary>
            /// <param name="Id"></param>
            /// <param name="unitId"></param>
            /// <returns></returns>
            public Tuple<Employees, ApplicationUser, Files, Jobtitles, Positions, EmpPayroll, List<LeaveRequest>> GetEmpDetails(int Id)
            {

                var employee = unitofWork.GetDbContext().employees.Find(Id);
                if (employee != null)
                {
                    var empUserDetails = userManager.FindById(employee.userId);
                    var job = unitofWork.GetDbContext().jobtitles.Find(employee.jobtitleId);
                    var position = unitofWork.GetDbContext().positions.Find(employee.positionId);
                    var avatar = unitofWork.GetDbContext().Files.Where(x => x.UserId == employee.userId && x.FileType == FileType.Avatar).FirstOrDefault();
                    var Salary = unitofWork.GetDbContext().Payroll.Where(x => x.UserId == employee.userId).SingleOrDefault();
                    var leave = unitofWork.GetDbContext().LeaveRequest.Where(x => x.UserId == employee.userId).ToList();
                    return Tuple.Create(employee, empUserDetails, avatar, job, position, Salary, leave);
                }
                return null;
            }

            public Tuple<Employees, ApplicationUser, Files> GetAllHrDetails(int unitId)
            {
                var members = unitofWork.GetDbContext().employees.Where(x => x.businessunitId == unitId && x.empRoleId == 3).FirstOrDefault();
                unitofWork.employees.Get();
                ApplicationUser HrUserDetail = new ApplicationUser();
                Files Avatar = new Files();
                if (members != null)
                {
                    Avatar = unitofWork.GetDbContext().Files.Where(x => x.UserId == members.userId && x.FileType == FileType.Avatar).FirstOrDefault();
                    HrUserDetail = userManager.FindById(members.userId);
                }
                return Tuple.Create(members, HrUserDetail, Avatar);
            }

            public Tuple<Employees, ApplicationUser, Files> GetAllUnitHeadDetails(int unitId)
            {
                var members = unitofWork.GetDbContext().employees.Where(x => x.businessunitId == unitId && x.IsUnithead == true).FirstOrDefault();
                ApplicationUser HrUserDetail = new ApplicationUser();
                Files Avatar = new Files();
                if (members != null)
                {
                    Avatar = unitofWork.GetDbContext().Files.Where(x => x.UserId == members.userId && x.FileType == FileType.Avatar).FirstOrDefault();
                    HrUserDetail = userManager.FindById(members.userId);
                }

                return Tuple.Create(members, HrUserDetail, Avatar);
            }
            public Tuple<List<Employees>, List<Files>, List<ApplicationUser>> GetTeamMembersWithAvatars(int unitId)
            {
                List<Files> Images = new List<Files>();
                List<Employees> TeamMembers = new List<Employees>();
                List<ApplicationUser> TeamMemberUserDetail = new List<ApplicationUser>();
                var members = unitofWork.GetDbContext().employees.Where(x => x.businessunitId == unitId && x.IsUnithead != true).ToList();
                if (members != null)
                {
                    foreach (var item in members)
                    {
                        var result = unitofWork.GetDbContext().Files.Where(x => x.UserId == item.userId && x.FileType == FileType.Avatar).FirstOrDefault();
                        var MemberUserDetail = userManager.FindById(item.userId);
                        Images.Add(result);
                        TeamMembers.Add(item);
                        TeamMemberUserDetail.Add(MemberUserDetail);
                    }
                }

                return Tuple.Create(TeamMembers, Images, TeamMemberUserDetail);
            }
            public Tuple<List<Employees>, List<Files>, List<ApplicationUser>, List<Logins>> GetAllEmployeesDetails()
            {
                var employee = unitofWork.GetDbContext().employees.ToList();
                List<Files> Images = new List<Files>();
                List<ApplicationUser> empUserDetails = new List<ApplicationUser>();
                List<Logins> AllLogins = new List<Domain.Entities.Logins>();
                foreach (var item in employee)
                {
                    var ImagList = unitofWork.GetDbContext().Files.Where(x => x.UserId == item.userId).FirstOrDefault();
                    var userlist = userManager.FindById(item.userId);
                    var loginList = unitofWork.GetDbContext().Logins.Where(x => x.userId == item.userId && x.IsLogOut == false).FirstOrDefault();
                    Images.Add(ImagList);
                    empUserDetails.Add(userlist);
                    AllLogins.Add(loginList);
                }
                return Tuple.Create(employee, Images, empUserDetails, AllLogins);
            }
        }



        //private bool disposed = false;

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            unitofWork.Dispose();
        //        }
        //    }
        //    this.disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose();
        //    // GC.SuppressFinalize(this);
        //}
    }
}
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;
using resourceEdge.webUi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using resourceEdge.webUi.Infrastructure.Core;

using resourceEdge.webUi.Models.SystemModel;
using resourceEdge.Domain.Concrete;

namespace resourceEdge.webUi.Infrastructure
{
    /// <summary>
    /// This class is used around the project as a service layer for interfaceing with the employee,
    /// Each method within this class was used to assess distinct informations about the employee and
    /// any related class.
    /// Note. that although some methods are not actually hitting the employee table
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
        private IMailDispatcher mailDispatchRepo;
        private ApplicationDbContext db = new ApplicationDbContext();
        private resourceEdge.webUi.ApplicationUserManager userManager;

        /// <summary>
        /// This class initializes Several constructors
        /// this is so because as a result of programming to the interface any implementing 
        /// class can provide only implementation of those related to the class
        /// <example>If the Employee class needs to implement this we only use the constructor that provides implementation
        /// of details related to the employee
        /// </example>
        /// </summary>

        public EmployeeManager()
        {
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            db = new ApplicationDbContext();
        }
        public EmployeeManager(IEmployees eparam)
        {
            EmployeeRepo = eparam;
        }
        public EmployeeManager(IEmployees EParam, IReportManager RParam, IMailDispatcher mailParam)
        {
            EmployeeRepo = EParam;
            ReportManagerRepo = RParam;
            mailDispatchRepo = mailParam;
        }
        public EmployeeManager(IEmployees eParam, ILeaveManagement Lparam)
        {
            EmployeeRepo = eParam;
            LeaveRepo = Lparam;
        }
        public EmployeeManager(IEmployees EParam, IFiles fParam, ILeaveManagement lParam)
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
        public EmployeeManager(IEmployees EParam, IFiles fParam, IReportManager RParam)
        {
            FileRepo = fParam;
           
            EmployeeRepo = EParam;
            ReportManagerRepo = RParam;
        }

        /// <summary>
        /// Every part of this class is divided into sections.
        /// the methods are organized in order of return type.
        /// This approach was used for code readabilty.
        /// <para name="id"></para>
        /// <para name="userId"></para>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EmployeeListItem> GetAllEmployees()
        {
            var result = unitofWork.employees.Get(includeProperties: "Department,Group,Level,Location,Businessunit").Select(x => new EmployeeListItem
            {
                BusinessUnitName = x.Businessunit.unitname,
                DepartmentName = x.Department.deptname,
                empEmail = x.empEmail,
                FullName = x.FullName,
                Id = x.ID,
                GroupName = x.Group.GroupName,
                LocationName = x.Location.State
            });
            return result;
        }
        public IEnumerable<EmployeeListItem> GetAllEmployeeByLocation(int locationId)
        {
            var result = unitofWork.employees.Get(filter: x => x.LocationId == locationId, includeProperties: "Department,Group,Level,Location,Businessunit").Select(x => new EmployeeListItem
            {
                BusinessUnitName = x.Businessunit.unitname,
                DepartmentName = x.Department.deptname,
                empEmail = x.empEmail,
                FullName = x.FullName,
                Id = x.ID,
                GroupName = x.Group.GroupName,
                LocationName = x.Location.State
            });
            return result;
        } 
        public EmployeeListItem GetEmployeeById(int id)
        {
            var result = unitofWork.employees.Get(filter: x => x.ID == id, includeProperties: "Department,Group,Level,Location,Businessunit").Select(x => new EmployeeListItem
            {
                BusinessUnitName = x.Businessunit.unitname,
                DepartmentName = x.Department.deptname,
                empEmail = x.empEmail,
                FullName = x.FullName,
                Id = x.ID,
                GroupName = x.Group.GroupName,
                LocationName = x.Location.State,
                 businessunitId = x.BusinessunitId,
                 departmentId = x.DepartmentId,
                  PhoneNumber = x.PhoneNumber
               
            }).FirstOrDefault();
            return result;
        }
        public List<Employee> GetEmpByBusinessUnit(int id)
        {
            var employeeByUnit = EmployeeRepo.GetEmpByBusinessUnit(id);
            if (employeeByUnit != null)
            {
                return employeeByUnit;
            }
            return null;
        }

        public string GetGroupName(int id)
        {
            var result = unitofWork.Groups.GetByID(id).GroupName;
            return result ?? null;
        }
        public string GetUserIdFromEmployeeTable(int Id)
        {
            var user = unitofWork.employees.GetByID(Id);
            return user != null ? user.userId : null;
        }
        public List<Employee> GetReportManagerBusinessUnit(int id)
        {
            //var context = unitofWork.GetDbContext();
            List<Employee> managers = new List<Employee>();
            //var ReportManager = context.ReportManagers.Where(x => x.BusinessUnitId == id).FirstOrDefault(); //Fix this when the reportmanager is fixed
            var ReportManagers = ReportManagerRepo.GetManagersByBusinessunit(id);
            if (ReportManagers != null)
            {
                foreach (var item in ReportManagers)
                {
                    managers = EmployeeRepo.GetUnitHead(item.BusinessUnitId);
                }
                if (managers.Count > 0)
                {
                    return managers;
                }
                var result =   EmployeeRepo.GetReportManagers(ReportManagers.FirstOrDefault().BusinessUnitId);
                return result;
            }
            return null;
        }
        public IEnumerable<Jobtitle> GetJobsByGroupForHr(int groupId)
        {
            var jobs = unitofWork.jobTitles.Get(filter: x => x.GroupId == groupId);
            return jobs;
        }
        public Employee GetEmployeeByUserId(string userid)
        {
            return EmployeeRepo.GetByUserId(userid);
        }

        public Employee CheckIfEmployeeExistByUserId(string userId)
        {

            return EmployeeRepo.CheckIfEmployeeExistByUserId(userId);
        }

        public List<Employee> GetEmployeeByDept(int dept)
        {
            return EmployeeRepo.GetEmployeeByDepts(dept);
        }

        public List<Employee> GetUnitHeads(int unitId)
        {
            return EmployeeRepo.GetUnitHead(unitId);
        }

        public List<Employee> GetHr()
        {
            return EmployeeRepo.GetHrs();
        }

        public List<Employee> GetEmployeeUnitMembers(int unitId, int locationId)
        {
            var members = unitofWork.employees.Get(filter: x => x.BusinessunitId == unitId && x.LocationId == locationId).ToList();
            return members ?? null;
        }

        //Although this method originally called by the ApiManager, its still left here just if there is a need to use it
        public List<Employee> GetUnitMembersBySearch(string userId, string searchString)
        {
            var userUnitId = EmployeeRepo.GetByUserId(userId);
            List<Employee> TeamMembers = new List<Employee>();
            if (searchString.ToLower().StartsWith("tenece"))
            {
                var TeamByEmpId = db.Users.Where(x => x.BusinessunitId == userUnitId.BusinessunitId.ToString() && x.EmployeeId == searchString).FirstOrDefault();
                if (TeamByEmpId != null)
                {
                    var TeamMember = EmployeeRepo.GetByUserId(TeamByEmpId.Id);
                    TeamMembers.Add(TeamMember);
                    return TeamMembers;
                }
            }
            TeamMembers = EmployeeRepo.GetEmpByBusinessUnit(userUnitId.BusinessunitId).Where(x => x.empEmail.Contains(searchString) || x.FullName.Contains(searchString)).ToList();
            if (TeamMembers != null)
            {
                return TeamMembers;
            }

            return null;

        }
        //This code was used to service the employee Creation if manager role was created immediately
        //this was not used in the assign reportmanager action Method becausethe repository was invoked
        public bool AssignReportManager(ReportManager manager)
        {
            try
            {
                ReportManagerRepo.Insert(manager);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        /// <summary>
        /// This section is the EmployeeLeaves section
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public EmployeeLeaveType LeaveTypeById(int id)
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

        public List<EmployeeListItem> GetReportManagrbyUserId(string userId)
        {
            var employee = UserManagement.getEmployeeIdFromUserTable(userId); //Check if this check actually checks the user table
            if (employee != null)
            {
                int BuintId = int.Parse(employee.BusinessunitId);
                var Reportmanager = ReportManagerRepo.GetManagersByBusinessunit(BuintId).Select(x => new EmployeeListItem
                {
                    empID = x.employeeId,
                    businessunitId = x.BusinessUnitId,
                    departmentId = x.DepartmentId,
                    userId = x.ManagerUserId,
                    FullName = x.FullName
                }).ToList();
                return Reportmanager ?? null;
            }
            return null;
        }
        public List<ReportManager> ExistingReportManager(string userId, int UnitId)
        {
            var validManager = ReportManagerCount(userId);
            if (validManager != true)
            {
                var manager = ReportManagerRepo.GetManagersByBusinessunit(UnitId);
                if (manager != null)
                {
                    return manager.ToList();
                }
            }
            return null;
        }
        public bool ReportManagerCount(string userId)
        {
            var result = ReportManagerRepo.GetReportmanagerCount(userId);
            if (result.Count >= 2)
            {
                return true;
            }
            return false;
        }

        public int ReportManagerForUnitCount(int unitId)
        {
            var result = unitofWork.ReportManager.Get(filter: x => x.BusinessUnitId == unitId).Count();
            return result;
        }

        public bool AddEmployeeToMailDispatch(string userName, string password, string sender, string GroupName, string Fullname)
        {
            try
            {
                MailDispatcher mail = new MailDispatcher()
                {
                    Delivered = false,
                    Message = userName + ',' + password,
                    Reciever = userName,
                    Sender = sender,
                    Subject = "Account Detail",
                    Type = Domain.Infrastructures.MailType.Account,
                    GroupName = GroupName,
                    FullName = Fullname
                };
            unitofWork.MailDispatch.Insert(mail);
                unitofWork.Save();
            return true;
            }catch(Exception ex)
            {
                return false;
                    throw ex;
                
            }
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
            public EmployeeEdit(IEmployees eParam, IFiles fParam, ILeaveManagement lParam, IPayroll payParam) : base(eParam, fParam, lParam, payParam)
            {

            }
            public void AddOrUpdateAvater(Domain.Entities.File model, string userId, Domain.Entities.File currentAvater, string filefullName, HttpPostedFileBase File, IFiles fileRepo)
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
                    var file = new Domain.Entities.File()
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
                var avatar = FileRepo.GetFileByUserId(userId, FileType.Avatar);
                if (avatar != null)
                {
                    return avatar.FilePath.ToString();
                }
                return null;
            }

            public void AddORUpdateSalary(string userId, PayrollViewModel entity, Employee employee, ApplicationUser currentUser, string systemUserId)
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
                }
                else
                {
                    EmpPayroll payroll = new EmpPayroll();
                    payroll.BusinessUnit = employee.BusinessunitId.ToString();
                    payroll.Department = employee.DepartmentId.ToString();
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
                }
            }

        }

        //Also know that this method is in the ApiManager. i left it there because i don't want the Apimanager to be dependent on any part of the system.
        //So i went ahead to duplicate the code...
        public LocationListItem GetLocationHeadDetails(int groupId, int LocationId)
        {
            var result = unitofWork.GetDbContext().Location.Where(m => m.Id == LocationId && m.GroupId == groupId).Select(x => new LocationListItem() { GroupId = x.GroupId, LocationId = x.Id, Manager1 = x.LocationHead1, Manager2 = x.LocationHead2, Manager3 = x.LocationHead3 }).FirstOrDefault();
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
                        var check = unitofWork.GetDbContext().Employee.Where(x => x.LocationId == LocationId && x.userId == item).Select(x => x.FullName).FirstOrDefault();
                        ManagerWithFullname.Add(item, check);
                    }
                }
            }
            if (result.Manager1 != null && ManagerWithFullname.ContainsKey(result.Manager1))
            {
                result.FullName1 = ManagerWithFullname[result.Manager1];
            }
            if (result.Manager2 != null && ManagerWithFullname.ContainsKey(result.Manager2))
            {
                result.FullName1 = ManagerWithFullname[result.Manager1];
            }
            if (result.Manager3 != null && ManagerWithFullname.ContainsKey(result.Manager3))
            {
                result.FullName3 = ManagerWithFullname[result.Manager3];
            }

            return result ?? null;
        }
        //This is a method identitcal to this that is also found in the Apimanager.
        //It is looks same but is different in that the other one returns a location listItem and this return an Enumerable of Employeelist item.
        //the other was used to service only the ApiManaget because of seperation of concerns i.e there should be no relationship between the Apimanager and any sub-system.

        public IEnumerable<EmployeeListItem> GetLocationHeadsDetails(int LocationId)
        {
            List<EmployeeListItem> EmployeeListItem = new List<EmployeeListItem>();
            var result = unitofWork.GetDbContext().Location.Where(m => m.Id == LocationId).Select(x => new LocationListItem() { GroupId = x.GroupId, LocationId = x.Id, Manager1 = x.LocationHead1, Manager2 = x.LocationHead2, Manager3 = x.LocationHead3 }).FirstOrDefault();
            var resultArray = new string[]
            {
               result.Manager1, result.Manager2, result.Manager3
            };
            foreach (var item in resultArray)
            {
                var employee = unitofWork.employees.Get(x => x.userId == item).FirstOrDefault();
                if (employee != null)
                {
                EmployeeListItem emp = new EmployeeListItem()
                {
                    userId = employee.userId,
                    FullName = employee.FullName
                };
                EmployeeListItem.Add(emp);
                }
            }
            return EmployeeListItem;
        }

        public List<EmployeeListItem> GetEmployeesByLocation(int id)
        {
            var result = EmployeeRepo.GetAllEmployeesByLocation(id)
                .Select(x => new EmployeeListItem()
                    {
                        businessunitId = x.BusinessunitId, FullName = x.FullName, userId = x.userId, GroupId = x.GroupId
                    }).ToList();
            result.ForEach(x =>x.Units = unitofWork.BusinessUnit.GetByID(x.businessunitId));
            result.ForEach(x => x.Group = unitofWork.Groups.GetByID(x.GroupId));
            return  result ?? null;
        }

        public List<QuestionViewModel> KpiQuestions(string userId)
        {
            var result = unitofWork.Questions.Get(filter: x => x.UserIdForQuestion == userId, includeProperties: "Group, BusinessUnit")
                .Select(X=> new QuestionViewModel
                { Question = X.EmpQuestion, Description = X.Description, Approved = X.Approved, UserId = X.UserIdForQuestion, Id = X.Id }).ToList();
            return result ?? null;
        }
        public bool ApproveQuestion(string userId=null, int? QstId = null)
        {
            try
            {
                if (string.IsNullOrEmpty(userId) && QstId == null)
                {
                    var AllQuestions = unitofWork.Questions.Get().ToList();
                    AllQuestions.ForEach(x => x.Approved = true);
                    AllQuestions.ForEach(X => unitofWork.Questions.Update(X));
                    unitofWork.Save();
                    return true;
                    //Approve all request
                }
                else if (userId != null && QstId == null)
                {
                    //approve all user Request
                    var allUserQuestion = unitofWork.Questions.Get(filter: x => x.UserIdForQuestion == userId).ToList();
                    allUserQuestion.ForEach(x => x.Approved = true);
                    allUserQuestion.ForEach(x => unitofWork.Questions.Update(x));
                    unitofWork.Save();
                    return true;
                }
                else if (!string.IsNullOrEmpty(userId) && QstId != null)
                {
                    var userQuestion = unitofWork.Questions.Get(filter: x => x.UserIdForQuestion == userId && x.Id == QstId.Value).FirstOrDefault();
                    userQuestion.Approved = true;
                    unitofWork.Questions.Update(userQuestion);
                    unitofWork.Save();
                    return true;
                    //Approve specific user Request
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return false;
        }

        public bool RejectQuestion(string userId = null, int? QstId = null)
        {
            try
            {
                if (string.IsNullOrEmpty(userId) && QstId == null)
                {
                    var AllQuestions = unitofWork.Questions.Get().ToList();
                    AllQuestions.ForEach(x => x.Approved = false);
                    AllQuestions.ForEach(X => unitofWork.Questions.Update(X));
                    unitofWork.Save();
                    return true;
                    //Approve all request
                }
                else if (userId != null && QstId == null)
                {
                    //approve all user Request
                    var allUserQuestion = unitofWork.Questions.Get(filter: x => x.UserIdForQuestion == userId).ToList();
                    allUserQuestion.ForEach(x => x.Approved = false);
                    allUserQuestion.ForEach(x => unitofWork.Questions.Update(x));
                    unitofWork.Save();
                    return true;
                }
                else if (!string.IsNullOrEmpty(userId) && QstId != null)
                {
                    var userQuestion = unitofWork.Questions.Get(filter: x => x.UserIdForQuestion == userId && x.Id == QstId.Value).FirstOrDefault();
                    userQuestion.Approved = false;
                    unitofWork.Questions.Update(userQuestion);
                    unitofWork.Save();
                    return true;
                    //Approve specific user Request
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
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
            /// 


            public Tuple<Employee, ApplicationUser, Domain.Entities.File, Jobtitle, Position, EmpPayroll, List<LeaveRequest>> GetEmpDetails(int Id)
            {

                var employee = unitofWork.GetDbContext().Employee.Find(Id);
                if (employee != null)
                {
                    var empUserDetails = userManager.FindById(employee.userId);
                    var job = unitofWork.jobTitles.GetByID(employee.JobTitleId);
                    var position = unitofWork.positions.GetByID(employee.PositionId);
                    var avatar = unitofWork.Files.Get(filter: x => x.UserId == employee.userId && x.FileType == FileType.Avatar).FirstOrDefault();
                    var Salary = unitofWork.PayRoll.Get(filter: x => x.UserId == employee.userId).SingleOrDefault();
                    var leave = unitofWork.LRequest.Get(filter: x => x.UserId == employee.userId).ToList();
                    return Tuple.Create(employee, empUserDetails, avatar, job, position, Salary, leave);
                }
                return null;
            }

            public Tuple<Employee, ApplicationUser, Domain.Entities.File> GetAllHrDetails(int unitId)
            {
                var members = unitofWork.employees.Get(filter: x => x.BusinessunitId == unitId && x.empRoleId == 3).FirstOrDefault();
                ApplicationUser HrUserDetail = new ApplicationUser();
                Domain.Entities.File Avatar = new Domain.Entities.File();
                if (members != null)
                {
                    Avatar = unitofWork.Files.Get(filter: x => x.UserId == members.userId && x.FileType == FileType.Avatar).FirstOrDefault();
                    HrUserDetail = userManager.FindById(members.userId);
                }
                return Tuple.Create(members, HrUserDetail, Avatar);
            }

            public Tuple<Employee, ApplicationUser, Domain.Entities.File> GetAllUnitHeadDetails(int unitId)
            {
                var members = unitofWork.employees.Get(filter: x => x.BusinessunitId == unitId && x.IsUnithead == true).FirstOrDefault();
                ApplicationUser HrUserDetail = new ApplicationUser();
                Domain.Entities.File Avatar = new Domain.Entities.File();
                if (members != null)
                {
                    Avatar = unitofWork.Files.Get(filter: x => x.UserId == members.userId && x.FileType == FileType.Avatar).FirstOrDefault();
                    HrUserDetail = userManager.FindById(members.userId);
                }

                return Tuple.Create(members, HrUserDetail, Avatar);
            }
            public Tuple<List<Employee>, List<Domain.Entities.File>, List<ApplicationUser>> GetTeamMembersWithAvatars(int unitId)
            {
                List<Domain.Entities.File> Images = new List<Domain.Entities.File>();
                List<Employee> TeamMembers = new List<Employee>();
                List<ApplicationUser> TeamMemberUserDetail = new List<ApplicationUser>();
                var members = unitofWork.employees.Get(x => x.BusinessunitId == unitId && x.IsUnithead != true).ToList();
                if (members != null)
                {
                    foreach (var item in members)
                    {
                        var result = unitofWork.Files.Get(filter: x => x.UserId == item.userId && x.FileType == FileType.Avatar).FirstOrDefault();
                        var MemberUserDetail = userManager.FindById(item.userId);
                        Images.Add(result);
                        TeamMembers.Add(item);
                        TeamMemberUserDetail.Add(MemberUserDetail);
                    }
                }

                return Tuple.Create(TeamMembers, Images, TeamMemberUserDetail);
            }
            public List<EmloyeeDetailistItem> GetAllEmployeesDetails()
            {
                var employeeQuery = unitofWork.employees.Get();

                List<EmloyeeDetailistItem> employeeListItem = new List<EmloyeeDetailistItem>();
                EmloyeeDetailistItem listItem;
                List<Employee> employees = new List<Employee>();
                foreach (var item in employeeQuery)
                {
                    listItem = new EmloyeeDetailistItem();
                    var ImagList = unitofWork.Files.Get(filter: x => x.UserId == item.userId && x.FileType == FileType.Avatar).Select(x=>x.FilePath).FirstOrDefault();
                    var userlist = userManager.FindById(item.userId);
                    bool? loginList = unitofWork.Logins.Get(filter: x => x.UserID == item.userId && x.IsLogOut == false).Select(x=>x.IsLogIn).LastOrDefault();
                    var employeeUnit = unitofWork.BusinessUnit.GetByID(item.BusinessunitId).unitname;
                    var employeeDept = unitofWork.Department.GetByID(item.DepartmentId).deptname;
                    listItem.BusinessUnitName = employeeUnit;
                    listItem.DepartmentName = employeeDept;
                    listItem.IsUnitHead = item.IsUnithead;
                    listItem.EmployeeId = item.ID;
                    listItem.Login = loginList ?? false;
                    listItem.UserId = userlist.Id;
                    listItem.ImageUrl = ImagList;
                    listItem.FullName = item.FullName;
                    employeeListItem.Add(listItem);
                }
                return employeeListItem;
            }
        }

    }

}
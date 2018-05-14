using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.Abstracts;
using resourceEdge.webUi.Infrastructure;
using resourceEdge.webUi.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using resourceEdge.webUi.Infrastructure.Handlers;
using resourceEdge.webUi.Infrastructure.Core;
using resourceEdge.webUi.Infrastructure.SystemManagers;

namespace resourceEdge.webUi.Controllers
{
   // [Authorize(Roles = "System Admin,HR")]
    [CustomAuthorizationFilter(Roles ="HR")]
    public class HRController : Controller
    {
        IEmployees empRepo;
        IBusinessUnits BunitsRepo;
        IReportManager ReportRepo;
        IEmploymentStatus statusRepo;
        IFiles FileRepo;
        ILevels levelRepo;
        ILocation LocationRepo;
        IGroups GroupRepo;
        IDepartments DepartmentRepo;
        IQuestions QuestionRepo;
        EmployeeManager employeeManager;
        Rolemanager RoleManager;
        EmployeeManager.EmployeeDetails empdetail;
        ApplicationDbContext db;
        ApplicationUserManager userManager;
        ConfigurationManager ConfigManager;
        DropDownManager dropDownManager;

        public HRController(IEmployees empParam, IBusinessUnits busParam, IReportManager rParam, Rolemanager RoleParam,
            ApplicationDbContext dbParam, IEmploymentStatus SParam, IFiles FParam, ILocation LRepo, ILevels levelParam, IGroups gParam,
            IDepartments deptParam, IMailDispatcher Mailparam, IQuestions Qparam)
        {
            db = dbParam;
            empRepo = empParam;
            BunitsRepo = busParam;
            ReportRepo = rParam;
            RoleManager = RoleParam;
            db = dbParam;
            statusRepo = SParam;
            FileRepo = FParam;
            levelRepo = levelParam;
            LocationRepo = LRepo;
            GroupRepo = gParam;
            DepartmentRepo = deptParam;
            QuestionRepo = Qparam;
            empdetail = new EmployeeManager.EmployeeDetails();
            employeeManager = new EmployeeManager(empParam, rParam, Mailparam);
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            ConfigManager = new ConfigurationManager();
            dropDownManager = new DropDownManager();
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                userManager = value;
            }
        }
        public ActionResult Index()
        {
            return View("Index");
        }
        public ActionResult allEmployee()
        {
            ViewBag.Avartar = GetAllEmpImage();
            var employees = empRepo.Get().ToList();
            ViewBag.employeeDetails = empdetail.GetAllEmployeesDetails();
            return View(employees.ToList());
        }
        [ChildActionOnly]
        private List<File> GetAllEmpImage()
        {
            return FileRepo.Get().ToList();
        }

        public ActionResult EmpDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employees = empRepo.GetById(id.Value);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        public List<IdentityRole> GetRoles()
        {
            var roles = RoleManager.GetRoles().ToList();
            return roles;
        }
        //[ChildActionOnly]
        public ActionResult Create(string returnUrl)
        {
            ViewBag.PageTitle = "Create Employee";
            var UserFromSession = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            ViewBag.Groups = GroupRepo.GetById(UserFromSession.GroupId).GroupName;
            ViewBag.Locations =ConfigManager.GetLocationByGroupId(UserFromSession.GroupId);
            ViewBag.roles = dropDownManager.GetRole();
            ViewBag.code = ConfigManager.GetIdentityCode(UserFromSession.GroupId);
            ViewBag.EmpStatus = dropDownManager.GetEmploymentStatus();
            ViewBag.prefix = dropDownManager.GetPrefix();
            ViewBag.businessUnits = dropDownManager.GetBusinessUnit();
            ViewBag.jobTitles = dropDownManager.GetJobtitle();
            ViewBag.Levels = dropDownManager.GetLevel();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeViewModel employees, string returnUrl)
        {
            Employee realEmployee = new Employee();
            ReportManager manager = null;
            var RealUserId = employees.identityCode + employees.empUserId;
            var EmployeeIdExist = Infrastructure.UserManagement.checkEmployeeId(RealUserId, employees.empEmail);
            var validDate = validateDates(employees.dateOfJoining, employees.dateOfLeaving);
            if (ModelState.IsValid)
            {
                if (EmployeeIdExist != true)
                {
                    if (validDate != false)
                    {
                        var UserFromSession = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
                        var unitDetail = BunitsRepo.GetById(employees.businessunitId);
                        realEmployee.businessunitId = employees.businessunitId;
                        realEmployee.createdby = User.Identity.GetUserId();
                        realEmployee.dateOfJoining = employees.dateOfJoining;
                        realEmployee.dateOfLeaving = employees.dateOfLeaving;
                        realEmployee.DepartmentId = employees.departmentId;
                        realEmployee.empEmail = employees.empEmail;
                        realEmployee.FullName = employees.FirstName + " " + employees.lastName;
                        realEmployee.empStatusId = employees.empStatusId;
                        realEmployee.isactive = true;
                        realEmployee.jobtitleId = employees.jobtitleId;
                        realEmployee.modeofEmployement = employees.modeofEmployement;
                        realEmployee.modifiedby = User.Identity.GetUserId();
                        realEmployee.officeNumber = employees.officeNumber;
                        realEmployee.positionId = employees.positionId;
                        realEmployee.prefixId = employees.prefixId;
                        realEmployee.yearsExp = employees.yearsExp;
                        realEmployee.LevelId = employees.Level;
                        realEmployee.LocationId = unitDetail.LocationId.Value;
                        realEmployee.GroupId = UserFromSession.GroupId;
                        realEmployee.isactive = true;
                        var CreatedDate = realEmployee.createddate = DateTime.Now;
                        var modifiedDate = realEmployee.modifieddate = DateTime.Now;
                        try
                        {
                            var newCreatedUser = await Infrastructure.UserManagement.CreateUser(employees.empEmail, employees.empRoleId.ToString(), employees.empStatusId, employees.FirstName, employees.lastName, employees.officeNumber,
                                 RealUserId, employees.jobtitleId.ToString(), null, User.Identity.GetUserId(), User.Identity.GetUserId(), employees.modeofEmployement.ToString(),
                                  employees.dateOfJoining, null, true, employees.departmentId.ToString(), employees.businessunitId.ToString(), UserFromSession.GroupId);
                            if (newCreatedUser.Item1.Id != null)
                            {

                                var role = db.Roles.Find(employees.empRoleId.ToString());
                                if (role.Name.ToLower() == "manager")
                                {
                                    realEmployee.IsUnithead = true;
                                    manager = new ReportManager();
                                }
                                realEmployee.empRoleId = employees.empRoleId;
                                realEmployee.userId = newCreatedUser.Item1.Id;
                                empRepo.Insert(realEmployee);
                                if (manager != null)
                                {
                                    manager.BusinessUnitId = employees.businessunitId;
                                    manager.DepartmentId = employees.departmentId;
                                    manager.employeeId = realEmployee.empID;
                                    manager.FullName = realEmployee.FullName;
                                    manager.ManagerUserId = realEmployee.userId;
                                    manager.GroupId = realEmployee.GroupId;
                                    manager.LocationId = realEmployee.LocationId.Value;
                                    employeeManager.AssignReportManager(manager);
                                }
                                var groupName = employeeManager.GetGroupName(employees.GroupId);
                                employeeManager.AddEmployeeToMailDispatch(employees.empEmail, newCreatedUser.Item2, "noreply@tenece.com", groupName, realEmployee.FullName);
                            }
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Error = "Sorry Employee was not created. Please try Again";
                            throw ex;
                        }
                        this.AddNotification("Employee Created Successfully", NotificationType.SUCCESS);
                        return RedirectToAction("Create");
                    }
                    this.AddNotification("Sorry, Please the entry date must not be less than or equal to the Exit Date Please try Again", NotificationType.WARNING);
                    return RedirectToAction("Create");
                }
                this.AddNotification($"Sorry Employee with this Id { employees.empUserId } already exist in the System Please try Again", NotificationType.ERROR);
                return Redirect("Create");
            }
            return RedirectToAction("Create");

        }
        [NonAction]
        public bool validateDates(DateTime? dateOfJoining, DateTime? dateOfLeaveing)
        {
            if (dateOfJoining != null)
            {
                if (dateOfLeaveing != null)
                {
                    if (dateOfJoining > dateOfLeaveing || (dateOfJoining > DateTime.Now))
                    {
                        return false;
                    }
                    else if (dateOfJoining < dateOfLeaveing)
                    {
                        return true;
                    }
                }
                return true;
            }
            return false;
        }
        public ActionResult AssignReportManager()
        {
            var CurrentEmployee = empRepo.GetByUserId(User.Identity.GetUserId());
            ViewBag.PageTitle = "Assign Manager";
            ViewBag.businessUnits = new SelectList(BunitsRepo.GetUnitsByLocation(CurrentEmployee.LocationId.Value).OrderBy(x => x.Id), "Id", "unitname", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignReportManager(reportmanagerViewModel model)
        {
            ReportManager manager = new ReportManager();
            var existingManager = employeeManager.ExistingReportManager(model.ManagerId, int.Parse(model.BunitId));
            if (ModelState.IsValid)
            {
                if (existingManager.Count <= 2)
                {
                    manager.BusinessUnitId = int.Parse(model.BunitId);
                    manager.ManagerUserId = model.ManagerId;
                    var employee = employeeManager.CheckIfEmployeeExistByUserId(model.ManagerId);
                    if (employee != null)
                    {
                        if (!UserManager.IsInRole(employee.userId, "Manager") &&  employee.IsUnithead != true )
                        {
                           
                            var result = empRepo.GetByUserId(model.ManagerId);
                            manager.employeeId = result.empID;
                            manager.DepartmentId = result.DepartmentId;
                            manager.FullName = result.FullName;
                            result.IsUnithead = true;
                            result.empRoleId = 2;
                         
                            var resultRole = userManager.RemoveFromRole(result.userId, "Employee"); //Fix this later and make sure the adding and removing is workin well.
                            //if (resultRole.Succeeded)
                            //{
                                userManager.AddToRole(result.userId, "Manager");
                                ReportRepo.Insert(manager);
                                empRepo.update(result); //fix this later by making this update the employee table
                            //}
                            this.AddNotification("Report manager added!", NotificationType.SUCCESS);
                            return RedirectToAction("AssignReportManager");
                        }
                        this.AddNotification("Oops! this employee is already a reporting manager, Please you can kindly assign someone else", NotificationType.WARNING);
                        return RedirectToAction("AssignReportManager");
                    }
                    else
                    {
                        this.AddNotification("No manager found with the specified Id, Please make sure you are not editing the request", NotificationType.ERROR);
                        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                    }
                }
                this.AddNotification("Cannot Assign More Than two managers to a Business Unit", NotificationType.WARNING);
                return RedirectToAction("AssignReportManager");
            }

           // ViewBag.businessUnits = new SelectList(BunitsRepo.Get().OrderBy(x => x.Id), "Id", "unitname", "Id");
           this.AddNotification("Make sure that the report Manager does not exist already for the business unit", NotificationType.ERROR);
            return RedirectToAction("AssignReportManager");
        }

        public ActionResult DeleteReportManager(string userId)
        {
            var result = ReportRepo.GetById(userId);
            if (result != null)
            {
                ReportRepo.Delete(result.ManagerUserId.ToString());
                return RedirectToAction("allEmployee");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
        }

        [Authorize]
        public ActionResult AssignDepartmentHead()
        {
            var UserFromSession = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            if (UserFromSession != null)
            {
                ViewBag.businessUnits = new SelectList(BunitsRepo.GetUnitsByLocation(UserFromSession.LocationId).OrderBy(x => x.Id), "Id", "unitname", "Id");
            }
            ViewBag.PageTitle = "Assign Department Head";
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AssignDepartmentHead(DepartmentHeadViewModel model)
        {
            int unitId = 0;
            int deptId = 0;
            if (ModelState.IsValid)
            {
                int.TryParse(model.BunitId, out unitId);
                int.TryParse(model.DeptId, out deptId);
                var user = employeeManager.GetEmployeeByUserId(model.userId);
                if (user != null && unitId != 0 && deptId != 0)
                {
                    if (user.IsDepthead != true)
                    {
                        user.IsDepthead = true;
                        empRepo.update(user);
                        this.AddNotification("", NotificationType.SUCCESS);
                        return RedirectToAction("AssignDepartmentHead");
                    }
                    this.AddNotification("Sorry this employee is already the Departmental Head", NotificationType.WARNING);
                    return RedirectToAction("AssignDepartmentHead");
                }
            }
            this.AddNotification("Something went wrong, please try again", NotificationType.ERROR);
            return RedirectToAction("AssignDepartmentHead");
        }

        public ActionResult AddSystemAdmin()
        {
            ViewBag.Groups = new SelectList(GroupRepo.Get().OrderBy(X => X.Id), "Id", "GroupName", "Id");
            ViewBag.PageTitle = "Add Admin";
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddSystemAdmin(EmployeeListItem model)
        {
            if (model.empEmail != null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Email = model.empEmail,
                    UserName = model.empEmail,
                    LocationId = model.LocationId,
                    GroupId = model.GroupId,
                    UserfullName = "System Admin"
                    
                };
                UserManager.Create(user, "1234567");
                UserManager.AddToRole(user.Id, "System Admin");
                ModelState.Clear();
                this.AddNotification("Successfully Created!", NotificationType.SUCCESS);
                return RedirectToAction("AddSystemAdmin");
            }
            this.AddNotification("Something went wrong please try Again", NotificationType.ERROR);
            return RedirectToAction("AddSystemAdmin");
        }

        public ActionResult AddHr()
        {
            //ViewBag.Groups = new SelectList(GroupRepo.Get().OrderBy(X => X.Id), "Id", "GroupName", "Id");
            var UserFromSession = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            var employees = employeeManager.GetEmployeesByLocation(UserFromSession.LocationId);
            ViewBag.allEmployees = new SelectList(employees.OrderBy(x => x.empEmail), "userId", "empEmail");
            ViewBag.PageTitle = "Add Admin";
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddHr(string userId)
        {

            if ( userId != null)
            {
                userManager.AddToRole(userId, "HR");
                ModelState.Clear();
                this.AddNotification("Successfully Created!", NotificationType.SUCCESS);
                return RedirectToAction("AddHr");
            }
            this.AddNotification("Something went wrong please try Again", NotificationType.ERROR);
            return RedirectToAction("AddHr");
        }


        public ActionResult AllQuestions()
        {
            ViewBag.PageTitle = "Questions";
            var questions = QuestionRepo.GetAllQuestionsEagerly("Location,BusinessUnit").GroupBy(x => x.UserFullName);
            List<Question> Questions = new List<Domain.Entities.Question>();
            foreach (var items in questions)
            {
                foreach (var item in items)
                {
                    var existingUser = Questions.Find(x => x.UserFullName == items.Key);
                    if (existingUser == null)
                    {
                        var question = new Question()
                        {
                            BusinessUnitId = item.BusinessUnitId,
                            BusinessUnit = item.BusinessUnit,
                            Location = item.Location,
                            UserFullName = items.Key
                        };
                        Questions.Add(question);
                    }
                }
            }
            return View(Questions);
        }

        [Authorize(Roles = "HR")]
        public ActionResult AddedQuestions()
        {
            ViewBag.PageTitle = "Add Questions";
            var usersessionObject =(SessionModel) Session["_ResourceEdgeTeneceIdentity"];
            var result = employeeManager.GetEmployeesByLocation(usersessionObject.LocationId);
            return View(result);
        }
       
        public ActionResult ViewQuestion(string ID)
        {
            ViewBag.PageTitle =$"{employeeManager.GetEmployeeByUserId(ID).FullName} Questions";
            ViewBag.ID = ID;
            var result = employeeManager.KpiQuestions(ID);
            return View(result);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ApproveQuestion(string userId=null, int? QstId=null)
        {
            bool result = false;
            if (string.IsNullOrEmpty(userId) && QstId == null)
            {
                //Approve all request
                result =  employeeManager.ApproveQuestion();
            }
            else if (userId != null && QstId == null)
            {
                //approve all user Request
              result= employeeManager.ApproveQuestion(userId);
                
            }
            else if (!string.IsNullOrEmpty(userId) && QstId != null)
            {
                //Approve specific user Request
               result =  employeeManager.ApproveQuestion(userId, QstId);
            }
            if (result != false)
            {
                this.AddNotification("Successfuly Approved Question(s)", NotificationType.SUCCESS);
                return RedirectToAction("AddedQuestions");
            }
            this.AddNotification("Question could not be approved please try again", NotificationType.ERROR);
            return RedirectToAction("AddedQuestion");

        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult RejectQuestion(string userId=null, int? QstId = null)
        {
            bool result = false;
            if (string.IsNullOrEmpty(userId) && QstId == null)
            {
                //Approve all request
                result = employeeManager.RejectQuestion();
            }
            else if (userId != null && QstId == null)
            {
                //approve all user Request
                result = employeeManager.RejectQuestion(userId);

            }
            else if (!string.IsNullOrEmpty(userId) && QstId != null)
            {
                //Approve specific user Request
                result = employeeManager.RejectQuestion(userId, QstId);
            }
            if (result != false)
            {
                this.AddNotification("Successfully Rejected Question(s)", NotificationType.SUCCESS);
                return RedirectToAction("AddedQuestions");
            }
            this.AddNotification("Question could not be Rejected please try again", NotificationType.ERROR);
            return RedirectToAction("AddedQuestion");
        }
        public ActionResult Questions(string department, string id = null)
        {
            
            if (id == null)
            {

            }
            else
            {

            }
            return View();
        }
    }

}


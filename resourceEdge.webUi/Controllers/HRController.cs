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

namespace resourceEdge.webUi.Controllers
{
    [Authorize(Roles = "System Admin,HR")]
    public class HRController : Controller
    {
        IEmployees empRepo;
        IBusinessUnits BunitsRepo;
        IReportManager ReportRepo;
        IEmploymentStatus statusRepo;
        EmployeeManager employeeManager;
        Rolemanager RoleManager;
        EmployeeManager.EmployeeDetails empdetail;
        IFiles FileRepo;
        ILevels levelRepo;
        ILocation LocationRepo;
        IGroups GroupRepo;
        IDepartments DepartmentRepo;
        ApplicationDbContext db;
        private ApplicationUserManager userManager;
        Apimanager Apimanager;

        public HRController(IEmployees empParam, IBusinessUnits busParam, IReportManager rParam, Rolemanager RoleParam, 
            ApplicationDbContext dbParam, IEmploymentStatus SParam, IFiles FParam, ILocation LRepo, ILevels levelParam, IGroups gParam, IDepartments deptParam)
        {
            db = dbParam;
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            empRepo = empParam;
            BunitsRepo = busParam;
            ReportRepo = rParam;
            employeeManager = new EmployeeManager(empParam,rParam);
            RoleManager = RoleParam;
            db = dbParam;
            statusRepo = SParam;
            FileRepo = FParam;
            empdetail = new EmployeeManager.EmployeeDetails();
            levelRepo = levelParam;
            LocationRepo = LRepo;
            GroupRepo = gParam;
            DepartmentRepo = deptParam;
            Apimanager = new Apimanager();
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
        private List<Files> GetAllEmpImage()
        {
           return  FileRepo.Get().ToList();
        }

        public ActionResult EmpDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = empRepo.GetById(id.Value);
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
        [ChildActionOnly]
        public ActionResult Create(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            ViewBag.EmpStatus = new SelectList(statusRepo.Get().Select(x => new { name = x.employemntStatus, id = x.empstId }), "id", "name", "id");
            ViewBag.roles = new SelectList(GetRoles().OrderBy(x => x.Name).Where(u => !u.Name.Contains("System Admin") && !u.Name.Contains("Management")).Select(x => new { name = x.Name, id = x.Id }), "Id", "name", "Id");
            ViewBag.prefix = new SelectList(Apimanager.PrefixeList(), "prefixId", "prefixName", "prefixId");
            ViewBag.businessUnits = new SelectList(BunitsRepo.Get().OrderBy(x => x.BusId), "BusId", "unitname", "BusId");
            ViewBag.jobTitles = new SelectList(Apimanager.JobList().OrderBy(x => x.JobName), "JobId", "JobName", "JobId");
            ViewBag.Levels = new SelectList(levelRepo.Get().OrderBy(x=>x.levelNo), "Id","LevelNo","Id");
            ViewBag.Locations = new SelectList(LocationRepo.Get().OrderBy(x => x.State), "Id", "State", "Id");
            ViewBag.Groups = new SelectList(GroupRepo.Get().OrderBy(X => X.Id), "Id", "GroupName", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeViewModel employees, string returnUrl)
        {
            Employees realEmployee = new Employees();
            ReportManagers manager = null;
            var RealUserId = employees.identityCode + employees.empUserId;
            var EmployeeIdExist = Infrastructure.UserManager.checkEmployeeId(RealUserId, employees.empEmail);
            var validDate = validateDates(employees.dateOfJoining.Value, employees.dateOfLeaving.Value);
            if (ModelState.IsValid)
            {
                if (EmployeeIdExist != true)
                {
                    if (validDate != false)
                    {
                        var unitDetail = BunitsRepo.GetById(employees.businessunitId);
                        realEmployee.businessunitId = employees.businessunitId;
                        realEmployee.createdby = User.Identity.GetUserId();
                        realEmployee.dateOfJoining = employees.dateOfJoining;
                        realEmployee.dateOfLeaving = employees.dateOfLeaving;
                        realEmployee.departmentId = employees.departmentId;
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
                        realEmployee.GroupId = employees.GroupId;
                        realEmployee.isactive = true;
                        var CreatedDate = realEmployee.createddate = DateTime.Now;
                        var modifiedDate = realEmployee.modifieddate = DateTime.Now;
                        try
                        {
                            var newCreatedUser = await Infrastructure.UserManager.CreateUser(employees.empEmail, employees.empRoleId.ToString(), employees.empStatusId, employees.FirstName, employees.lastName, employees.officeNumber,
                                 RealUserId, employees.jobtitleId.ToString(), null, null, User.Identity.GetUserId(), employees.modeofEmployement.ToString(),
                                  employees.dateOfJoining, null, true, employees.departmentId.ToString(), employees.businessunitId.ToString());
                            if (newCreatedUser.Id != null)
                            {
                               var role =  db.Roles.Find(employees.empRoleId.ToString());
                                if (role.Name.ToLower() == "manager" )
                                {
                                    realEmployee.IsUnithead = true;
                                    manager = new ReportManagers();
                                }
                                realEmployee.empRoleId = employees.empRoleId;
                                realEmployee.userId = newCreatedUser.Id;
                                empRepo.Insert(realEmployee);
                                if (manager != null)
                                {
                                    manager.BusinessUnitId = employees.businessunitId;
                                    manager.DepartmentId = employees.departmentId;
                                    manager.employeeId = realEmployee.empID;
                                    manager.FullName = realEmployee.FullName;
                                    manager.managerId = realEmployee.userId;
                                    employeeManager.AssignReportManager(manager);
                                }
                               
                            }
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Error = "Sorry Employee was not created. Please try Again";
                            throw ex;
                        }
                        ViewBag.Success = "Employee Created Successfully";
                        ModelState.AddModelError("", "Employee Created Successfully");
                        return Redirect(returnUrl);
                    }
                    ViewBag.Error = "Sorry, Please the entry date must not be less than or equal to the Exit Date Please try Again";
                    ModelState.AddModelError("", "Sorry, Please the entry date must not be less than or equal to the Exit Date Please try Again");
                    return Redirect(returnUrl);
                }
                ViewBag.Error = $"Sorry Employee with this Id { employees.empID } already exist in the System Please try Again";
                ModelState.AddModelError("", "Sorry Employee with this Id { employees.empID } already exist in the System Please try Again");
                //return Redirect(returnUrl);
            }
            ViewBag.Error = $"The employee {RealUserId} already exist in the system";
            ViewBag.EmpStatus = new SelectList(statusRepo.Get().Select(x => new { name = x.employemntStatus, id = x.empstId }).OrderBy(x => x.name), "id", "name", "id");
            ViewBag.roles = new SelectList(GetRoles().OrderBy(x => x.Name).Where(u => !u.Name.Contains("System Admin") && !u.Name.Contains("Management") && !u.Name.Contains("Manager")).Select(x=>new { name = x.Name, id = x.Id }), "Id", "name", "Id");
            ViewBag.prefix = new SelectList(Apimanager.PrefixeList(), "prefixId", "prefixName", "prefixId");
            ViewBag.businessUnits = new SelectList(BunitsRepo.Get().OrderBy(x => x.BusId), "BusId", "unitname", "BusId");
            ViewBag.jobTitles = new SelectList(Apimanager.JobList().OrderBy(x => x.JobName), "JobId", "JobName", "JobId");  
            return Redirect(returnUrl);

        }

        [ChildActionOnly]
        public bool validateDates(DateTime dateOfJoining, DateTime dateOfLeaveing)
        {
            if (dateOfJoining > dateOfLeaveing)
            {
                return false;
            }
            else if(dateOfJoining < dateOfLeaveing)
            {
                return true;
            }
            return false;
        }
        public ActionResult AssignReportManager()
        {
            var CurrentEmployee = empRepo.GetByUserId(User.Identity.GetUserId());
            ViewBag.businessUnits = new SelectList(BunitsRepo.GetUnitsByLocation(CurrentEmployee.LocationId.Value).OrderBy(x => x.BusId), "BusId", "unitname", "BusId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignReportManager(reportmanagerViewModel model)
        {
            ReportManagers manager = new ReportManagers();
            var existingManager = employeeManager.ExistingReportManager(model.ManagerId, int.Parse(model.BunitId));
            if (ModelState.IsValid && existingManager == null)
            {
                if (existingManager.Count < 2)
                {
                    manager.BusinessUnitId = int.Parse(model.BunitId);
                    manager.managerId = model.ManagerId;
                    var employee = employeeManager.CheckIfEmployeeExistByUserId(model.ManagerId);
                    if (employee != null && employee.empID != 2)
                    {
                        var result = empRepo.GetByUserId(model.ManagerId);
                        manager.employeeId = result.empID;
                        manager.DepartmentId = result.departmentId;
                        manager.FullName = result.FullName;
                        result.IsUnithead = true;
                        result.empRoleId = 2;
                        empRepo.update(result); //fix this later by making this update the employee table
                        var resultRole = userManager.RemoveFromRole(result.userId, "Employee"); //Fix this later and make sure the adding and removing is workin well.
                        if (resultRole.Succeeded)
                        {
                            userManager.AddToRole(result.userId, "Manager");
                            ReportRepo.Insert(manager);
                        }
                        return RedirectToAction("allEmployee");
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                    }
                }
                ViewBag.Error = "Cannot Assign More Than two managers to a Business Unit";  
            }

                ViewBag.businessUnits = new SelectList(BunitsRepo.Get().OrderBy(x => x.BusId), "BusId", "unitname", "BusId");
                ViewBag.Error = "Make sure that the report Manager does not exist already for the business unit";
                ViewBag.Warning = "Also make sure that you don't add an employee twice for particlar business unit";
                return RedirectToAction("AssignReportManager");
        }

        public ActionResult DeleteReportManager(string userId)
        {
            var result = ReportRepo.GetById(userId);
            if (result != null)
            {
                ReportRepo.Delete(result.managerId.ToString());
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
            var UserFromSession = (SessionModel) Session["_ResourceEdgeTeneceIdentity"];
            if (UserFromSession != null)
            {
                ViewBag.businessUnits = new SelectList(BunitsRepo.GetUnitsByLocation(UserFromSession.LocationId).OrderBy(x => x.BusId), "BusId", "unitname", "BusId");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AssignDepartmentHead(DepartmentHeadViewModel model)
        {
            return View();
        }
    }

}


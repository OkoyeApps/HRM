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
    public class HRController : Controller
    {
        IEmployees empRepo;
        IBusinessUnits BunitsRepo;
        IReportManager ReportRepo;
        IEmploymentStatus statusRepo;
        EmployeeManager employeeManager;
        Rolemanager RoleManager;

        ApplicationDbContext db;
        private ApplicationUserManager userManager;

        public HRController(IEmployees empParam, IBusinessUnits busParam, IReportManager rParam, Rolemanager RoleParam, EmployeeManager EParam, ApplicationDbContext dbParam, IEmploymentStatus SParam)
        {
            db = dbParam;
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            empRepo = empParam;
            BunitsRepo = busParam;
            ReportRepo = rParam;
            employeeManager = EParam;
            RoleManager = RoleParam;
            db = dbParam;
            statusRepo = SParam;
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
            
            var employees = empRepo.getEmployees();

            return View(employees.ToList());
        }

        public ActionResult EmpDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = empRepo.getEmployeesById(id);
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
            var result = statusRepo.GetEmployementStatus().Select(x => new { name = x.employemnt_status, id = x.empstId });
            ViewBag.EmpStatus = new SelectList(statusRepo.GetEmployementStatus().Select(x => new { name = x.employemnt_status, id = x.empstId }), "id", "name", "id");
            ViewBag.roles = new SelectList(GetRoles().OrderBy(x => x.Name).Where(u => !u.Name.Contains("System Admin") && !u.Name.Contains("Management")).Select(x => new { name = x.Name, id = x.Id }), "Id", "name", "Id");
            ViewBag.prefix = new SelectList(Apimanager.PrefixeList(), "prefixId", "prefixName", "prefixId");
            ViewBag.businessUnits = new SelectList(BunitsRepo.GetBusinessUnit().OrderBy(x => x.BusId), "BusId", "unitname", "BusId");
            ViewBag.jobTitles = new SelectList(Apimanager.JobList().OrderBy(x => x.JobName), "JobId", "JobName", "JobId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeViewModel employees, string returnUrl)
        {
            Employees realEmployee = new Employees();
            var RealUserId = employees.identityCode + employees.empUserId;
            var EmployeeIdExist = Infrastructure.UserManager.checkEmployeeId(RealUserId);
            if (ModelState.IsValid && EmployeeIdExist != true)
            {
                realEmployee.businessunitId = employees.businessunitId.Value;
                realEmployee.createdby = null;
                realEmployee.dateOfJoining = employees.dateOfJoining;
                realEmployee.dateOfLeaving = employees.dateOfLeaving;
                realEmployee.departmentId = employees.departmentId.Value;
                realEmployee.empEmail = employees.empEmail;
                realEmployee.FullName = employees.FirstName + " " +  employees.lastName;
                realEmployee.empStatusId = employees.empStatusId;
                realEmployee.isactive = true;
                realEmployee.jobtitleId = employees.jobtitleId.Value;
                realEmployee.modeofEmployement = employees.modeofEmployement;
                realEmployee.modifiedby = null;
                realEmployee.officeNumber = employees.officeNumber;
                realEmployee.positionId = realEmployee.positionId;
                realEmployee.prefixId = employees.prefixId;
                realEmployee.yearsExp = employees.yearsExp;
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
                        realEmployee.empRoleId = employees.empRoleId;
                        realEmployee.userId = newCreatedUser.Id;
                        empRepo.addEmployees(realEmployee);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return Redirect(returnUrl);
            }
            TempData["Error"] = String.Format($"The employee id already { employees.empUserId} exist in the system");
            ViewBag.EmpStatus = new SelectList(statusRepo.GetEmployementStatus().Select(x => new { name = x.employemnt_status, id = x.empstId }).OrderBy(x => x.name), "id", "name", "id");
            ViewBag.roles = new SelectList(GetRoles().OrderBy(x => x.Name).Where(u => !u.Name.Contains("System Admin") && !u.Name.Contains("Management") && !u.Name.Contains("Manager")).Select(x=>new { name = x.Name, id = x.Id }), "Id", "name", "Id");
            ViewBag.prefix = new SelectList(Apimanager.PrefixeList(), "prefixId", "prefixName", "prefixId");
            ViewBag.businessUnits = new SelectList(BunitsRepo.GetBusinessUnit().OrderBy(x => x.BusId), "BusId", "unitname", "BusId");
            ViewBag.jobTitles = new SelectList(Apimanager.JobList().OrderBy(x => x.JobName), "JobId", "JobName", "JobId");  
            return Redirect(returnUrl);

        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = empRepo.getEmployeesById(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "empID,userId,emp_email,date_of_joining,date_of_leaving,reporting_manager,emp_status_id,businessunit_id,department_id,jobtitle_id,position_id,years_exp,holiday_group,prefix_id,extension_number,office_number,office_faxnumber,createdby,modifiedby,createddate,modifieddate,isactive,is_orghead,modeofEmployement")] Employees employees)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    empRepo.UpdateEmployees(employees);
                    TempData["Success"] = "New Employee Created";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            TempData["Error"] = "please make sure all fields are filled";
            return View(employees);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = empRepo.getEmployeesById(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employees employees = empRepo.getEmployeesById(id);
            TempData["Success"] = "Employee deleted successfully";
            return RedirectToAction("Index");
        }


        public ActionResult AssignReportManager()
        {
            ViewBag.businessUnits = new SelectList(BunitsRepo.GetBusinessUnit().OrderBy(x => x.BusId), "BusId", "unitname", "BusId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignReportManager(reportmanagerViewModel model)
        {
            ReportManagers manager = new ReportManagers();
            //var existingManager = ReportRepo.GetReportManagerById(model.userId);
            var existingManager = employeeManager.DoesReportManagerExist(model.userId, int.Parse(model.BunitId));
            if (ModelState.IsValid && existingManager == false)
            {
                manager.BusinessUnitId = int.Parse(model.BunitId);
                manager.managerId = model.userId;
                var employee = employeeManager.CheckIfEmployeeExistByUserId(model.userId);
                if (employee != false)
                {
                    var result = empRepo.GetEmployeeByUserid(model.userId);
                    manager.employeeId = result.empID;
                    manager.DepartmentId = result.departmentId;
                    manager.FullName = result.FullName;
                    result.IsUnithead = true;
                    result.empRoleId = 2;
                    empRepo.UpdateEmployees(result); //fix this later by making this update the employee table
                   var resultRole =  userManager.RemoveFromRole(result.userId, "Employee"); //Fix this later and make sure the adding and removing is workin well.
                    if (resultRole.Succeeded)
                    {
                        userManager.AddToRole(result.userId, "Manager");
                        ReportRepo.AddReportMananger(manager);
                    }
                    return RedirectToAction("allEmployee");
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
            }
            else
            {
                ViewBag.businessUnits = new SelectList(BunitsRepo.GetBusinessUnit().OrderBy(x => x.BusId), "BusId", "unitname", "BusId");
                TempData["Error"] = "Make sure that the report Manager does not exist already for the business unit";
                TempData["Warning"] = "Also make sure thatyou don't add an employee twice for particlar business unit";
                return View();
            }
        }

        public ActionResult DeleteReportManager(string userId)
        {
            var result = ReportRepo.GetReportManagerById(userId);
            if (result != null)
            {
                ReportRepo.RemoveReportManager(result.managerId.ToString());
                return RedirectToAction("allEmployee");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
        }


//Call the Dispose method later
        public void Dispose(bool disposing)
        {
            if (disposing)
            {
               Dispose();
            }
            if (empRepo != null)
            {
                empRepo = null;
            }
            if (BunitsRepo != null)
            {
                BunitsRepo = null;
            }
            if (ReportRepo != null)
            {
                ReportRepo = null;
            }
            if (employeeManager != null)
            {
                employeeManager = null;
            }
            if (RoleManager != null)
            {
                RoleManager = null;
            }
            this.Dispose();
        }
    }
    //protected override void Dispose(bool disposing)
    //{
    //    if (disposing)
    //    {
    //        if ( RoleManager != null)
    //        {
    //            RoleManager.Dispose();
    //            RoleManager = null;
    //        }

    //        if (employeeManager != null)
    //        {
    //            employeeManager.Dispose();
    //            employeeManager = null;
    //        }
    //        if (userManager != null)
    //        {
    //            userManager.Dispose();
    //            userManager = null;
    //        }
    //    }

    //    base.Dispose(disposing);
    //}
}


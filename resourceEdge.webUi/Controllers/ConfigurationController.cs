using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using Microsoft.AspNet.Identity.Owin;
using resourceEdge.webUi.Models;
using Microsoft.AspNet.Identity;
using System.Net;
using resourceEdge.webUi.Infrastructure;

namespace resourceEdge.webUi.Controllers
{
    //[RoutePrefix("configurations")]
    public class ConfigurationController : Controller
    {
        private ApplicationUserManager userManager;
        IBusinessUnits BusinessRepo;
        IDepartments DeptRepo;
        IidentityCodes IdentityRepo;
        IJobtitles JobRepo;
        IPositions positionRepo;
        IPrefixes prefixRepo;
        IEmploymentStatus statusRepo;
        ILeaveManagement leaveRepo;
        public ApplicationUserManager UserManager
        {
            get {return userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set  { userManager = value; }
        }
        public ApplicationUser LoggedInUser() {return UserManager.FindById(User.Identity.GetUserId());}

        public ConfigurationController(IBusinessUnits BParam, IDepartments DParam, IidentityCodes idCodes, IJobtitles jParam, IPositions pParam, IPrefixes prparam, IEmploymentStatus status, ILeaveManagement lParam)
        {
            this.BusinessRepo = BParam;
            this.DeptRepo = DParam;
            this.IdentityRepo = idCodes;
            this.JobRepo = jParam;
            this.positionRepo = pParam;
            this.prefixRepo = prparam;
            this.statusRepo = status;
            this.leaveRepo = lParam;
        }
        public ActionResult Index()
        {
            return View("Configuration");
        }

        public ActionResult AllCodes()
        {
            return View(IdentityRepo.GetIdentityCodes());
        }
        public ActionResult AddCode(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return PartialView(new IdentityCodes());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Route("addCode")]
        public ActionResult AddCode(IdentityCodes codes, string returnUrl)
        {
            IdentityCodes code = codes;
            if (ModelState.IsValid)
            {
                code.createddate = DateTime.Now;
                //code.modifiedBy = int.Parse(LoggedInUser().Id);
                //code.createdBy = int.Parse(LoggedInUser().Id);
                code.createdBy = null;
                code.createdBy = null;
                IdentityRepo.addIdentityCode(code);
                ModelState.Clear();
                TempData["Success"] = "Code created successfully";
                return Redirect(returnUrl);
            }
            else
            {
                TempData["Error"] = "Something went wrong. please make sure you fill all the appropriate details";
                return PartialView(codes);
            }
        }
        public PartialViewResult EditCode(int id = 1)
        {
            var code = IdentityRepo.GetIdentityById(id);
            return PartialView(code);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCode([Bind(Include = "employee_code,backgroundagency_code,vendors_code,staffing_code,users_code,requisition_code")] IdentityCodes code)
        {
            if (ModelState.IsValid)
            {
                IdentityRepo.updateIdentityCode(code);
                return View("AddCode");
            }
            else
            {
                TempData["Error"] = "Something went wrong. please make sure you fill all the appropriate details";
                return View();
            }
        }
        public PartialViewResult addPrefix(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return PartialView(new Prefixes());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addPrefix(Prefixes model, string returnUrl)
        {

            Prefixes prefixes = model;
            if (ModelState.IsValid)
            {
                prefixes.createdby = null;
                prefixes.createddate = DateTime.Now;
                prefixes.modifiedby = null;
                prefixes.isactive = true;
                prefixRepo.addprefixes(prefixes);
                TempData["Success"] = string.Format($"{model.prefixName} has been created");
                return Redirect(returnUrl);
            }
            else
            {
                 TempData["Error"] = "Something went wrong. please make sure you fill all the appropriate details";
                 return View(model);
            }

        }
        public ActionResult AllBusinessUnits()
        {
            return View(BusinessRepo.GetBusinessUnit());
        }
        public ActionResult addBusinessUnits()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addBusinessUnits(BusinessUnits units)
        {
            BusinessUnits unit = units;
            if (ModelState.IsValid)
            {
                unit.unithead = "";
                unit.createdby = null;
                unit.createddate = DateTime.Now;
                unit.modifiedby = null;
                unit.modifieddate = DateTime.Now;
                unit.isactive = true;
                BusinessRepo.addbusinessunit(unit);
                TempData["Success"] = string.Format($"{unit.unitname} has been created");
                return RedirectToAction("AllBusinessUnits");
            }
            else
            {
                TempData["Error"] = "Something went wrong. please make sure you fill all the appropriate details";
                return View();
            }
        }

        public ActionResult EditUnit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var unit = BusinessRepo.GetBusinessUnitById(id);
            if (unit == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            else
            {
                return PartialView(unit);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUnit(BusinessUnits units)
        {
            if (ModelState.IsValid)
            {
                TempData["Success"] = string.Format($"{units.unitname} has been modified");
                BusinessRepo.UpdateBusinessunit(units);
                return RedirectToAction("AllBusinessUnits");
            }
            else
            {
                TempData["Error"] = "Something went wrong. please make sure you fill all the appropriate details";
                return View(units);
            }
        }

        public ActionResult unitDetail(int id)
        {
            BusinessUnits unit = BusinessRepo.GetBusinessUnitById(id);
            return View(unit);
        }
        public ActionResult deleteUnit(int id)
        {
            BusinessUnits unit = BusinessRepo.GetBusinessUnitById(id);
            var unitName = unit.unitname;
            if (unit == null)
            {
                return HttpNotFound();
            }
                TempData["unitName"] = string.Format($"{unitName} has been deleted");
                BusinessRepo.RemoveBusinessunit(id);
                return RedirectToAction("AllBusinessUnits");
        }

        public ActionResult AllDepartment()
        {        
            return View(DeptRepo.Getdepartment());
        }

        public ActionResult addDepartment()
        {
            ViewBag.businessUnits = new SelectList(BusinessRepo.GetBusinessUnit().OrderBy(x => x.unitname), "BusId", "unitname");
            return View(new Departments());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addDepartment(Departments dept)
        {
            if (ModelState.IsValid)
            {
                Departments depts = dept;
                depts.createdby = null;
                dept.modifiedby = null;
                depts.modifieddate = DateTime.Now;
                DeptRepo.addepartment(depts);
                TempData["Success"] = string.Format($"{dept.deptname} has been created");
                return RedirectToAction("AllDepartment");
            }

                ViewBag.businessUnits = new SelectList(BusinessRepo.GetBusinessUnit().OrderBy(x => x.unitname), "BusId", "unitname", "BusId");
                TempData["Error"] = "Something went wrong. please make sure you fill all the appropriate details";
                return View(dept);
        }

        public ActionResult EditDepartment(int id)
        {
            Departments dept = DeptRepo.GetdepartmentById(id);
            if (dept == null)
            {
                return HttpNotFound();
            }
            else
            {
                return PartialView(dept);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDepartment(Departments dept)
        {
            if (ModelState.IsValid)
            {
                TempData["Success"] = String.Format($"{dept.deptname} has been Updated");
                DeptRepo.Updatedepartment(dept);
                return View("AllDepartment");
            }
                TempData["Error"] = "Something went wrong. please make sure you fill all the appropriate details";
                return View(dept);
        }

        public ActionResult DeptDetail(int id)
        {
            Departments dept = DeptRepo.GetdepartmentById(id);
            return View(dept);
        }

        [HttpPost]
        public ActionResult DeleteDepartment(int id)
        {
            Departments dept = DeptRepo.GetdepartmentById(id);
            if (dept == null)
            {
                return HttpNotFound();
            }
            else
            {
                TempData["Success"] = string.Format($"{dept.deptname} has been deleted");
                DeptRepo.DeleteDepartment(id);
                return RedirectToAction("AllDepartment");
            }
        }

        public PartialViewResult allJob()
        {
            return PartialView(JobRepo.GetJobTitles());
        }

        public PartialViewResult AddJobTitle(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return PartialView(new Jobtitles());
        }

        //Finish THe job and position creations and remember that position depends on job
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddJobTitle(Jobtitles jobs, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Jobtitles job = jobs;
                job.createdby = null;
                job.modifieddate = DateTime.Now;
                JobRepo.addJobTitles(job);
                TempData["Success"] = string.Format($"{job.jobtitlename} has been created");
                ModelState.Clear();
                return Redirect(returnUrl);
            }

                TempData["Error"] = "Something went wrong. please make sure you fill all the appropriate details";
                return PartialView(jobs);
        }

        public ActionResult AllPosition()
        {
            return PartialView(positionRepo.GetPosition());
        }

        public PartialViewResult addPosition(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            ViewBag.jobTitles = new SelectList(Apimanager.JobList().OrderBy(x => x.JobName), "JobId", "JobName", "JobId");
            return PartialView(new Positions());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addPosition(Positions model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Positions position = model;
                    position.createdby = null; // int.Parse(User.Identity.GetUserId());
                    position.modifiedby = null; //int.Parse(User.Identity.GetUserId());
                    position.modifieddate = DateTime.Now;
                    positionRepo.AddPosition(position);
                    ModelState.Clear();
                    TempData["Success"] = string.Format($"{position.positionname} has been created");
                    return Redirect(returnUrl);
                }
                ViewBag.jobTitles = new SelectList(Apimanager.JobList().OrderBy(x => x.JobName), "JobId", "JobName", "JobId");
                TempData["Error"] = "Something went wrong. please make sure you fill all the appropriate details";
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public PartialViewResult addEmploymentStatus(string returnUrl)
        {
            ViewBag.status = returnUrl;
            return PartialView(new employeeStatusViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addEmploymentStatus(employeeStatusViewModel model, string returnUrl)
        {
           
            try
            {
                if (ModelState.IsValid)
                {
                    EmploymentStatus status = new EmploymentStatus();
                    status.employemnt_status = model.employemnt_status;
                    status.createdby = null;
                    status.createddate = DateTime.Now;
                    status.modifiedby = null;
                    status.modifieddate = DateTime.Now;
                    status.isactive = true;
                    statusRepo.AddEmploymentStatus(status);
                    ModelState.Clear();
                    TempData["Success"] = string.Format($"{model.employemnt_status} has been created");
                    return Redirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong.\n please make sure your data's are valid \n if the problem persist contact the system Administrator");
                    TempData["Error"] = "Something went wrong. please make sure you fill all the appropriate details";
                    return PartialView(model);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult AllLeaveType()
        {
            return View(leaveRepo.GetLeaveTypes());
        }
        public ActionResult AddLeaveType()
        {  
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLeaveType(LeaveTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                EmployeeLeaveTypes leaveType = new EmployeeLeaveTypes();
                leaveType.leavetype = model.leavetype;
                leaveType.leavecode = model.leavecode;
                leaveType.leavepreallocated = model.leavepreallocated.ToString();
                leaveType.description = model.description;
                leaveType.numberofdays = model.numberofdays;
                leaveType.createdby = User.Identity.GetUserId();
                leaveType.modifiedby = User.Identity.GetUserId();
                leaveType.createddate = DateTime.Now;
                leaveType.modifieddate = DateTime.Now;
                leaveType.isactive = true;
                leaveRepo.AddLeaveTypes(leaveType);
               return RedirectToAction("AllLeaveType");
            }
            ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            TempData["Error"] = "Something went wrong try again later";
            return View(model);
        }
    }
}

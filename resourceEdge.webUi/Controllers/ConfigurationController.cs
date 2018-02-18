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
   [Authorize(Roles = "System Admin,HR")]
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
        ILevels levelRepo;
        ICareers careerRepo;
        ILocation LocationRepo;
        ConfigurationManager ConfigManager;
        public ApplicationUserManager UserManager
        {
            get {return userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set  { userManager = value; }
        }
        public ApplicationUser LoggedInUser() {return UserManager.FindById(User.Identity.GetUserId());}

        public ConfigurationController()
        {

        }

        public ConfigurationController(IBusinessUnits BParam, IDepartments DParam, IidentityCodes idCodes, IJobtitles jParam, 
            IPositions pParam, IPrefixes prparam, IEmploymentStatus status, ILeaveManagement lParam, ILevels levelParam, 
            ILocation locationParam, ICareers CareerParam)
        {
            this.BusinessRepo = BParam;
            this.DeptRepo = DParam;
            this.IdentityRepo = idCodes;
            this.JobRepo = jParam;
            this.positionRepo = pParam;
            this.prefixRepo = prparam;
            this.statusRepo = status;
            this.leaveRepo = lParam;
            this.careerRepo = CareerParam;
            this.levelRepo = levelParam;
            LocationRepo = locationParam;
            ConfigManager = new ConfigurationManager(BParam);
        }
        public ActionResult Index()
        {
            return View("Configuration");
        }

        public ActionResult AllCodes()
        {
            return View(IdentityRepo.Get());
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
                code.modifiedBy = User.Identity.GetUserId();
                code.createdBy = User.Identity.GetUserId();
                code.createdBy = null;
                code.createdBy = null;
                IdentityRepo.Insert(code);
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
            var code = IdentityRepo.GetById(id);
            return PartialView(code);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCode([Bind(Include = "employee_code,backgroundagency_code,vendors_code,staffing_code,users_code,requisition_code")] IdentityCodes code)
        {
            if (ModelState.IsValid)
            {
                IdentityRepo.update(code);
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
        public ActionResult addPrefix(prefixViewModel model, string returnUrl)
        {

            
            if (ModelState.IsValid)
            {
                Prefixes prefixes = new Prefixes()
                {
                    prefixName = model.prefixName,
                    prefixId = model.prefixId,
                    createdby = null,
                    createddate = DateTime.Now,
                    modifiedby = null,
                    isactive = true
                };
             
                prefixRepo.Insert(prefixes);
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
            return View(BusinessRepo.Get());
        }
        public ActionResult addBusinessUnits()
        {
            ViewBag.Locations = new SelectList(LocationRepo.Get().OrderBy(x => x.State), "Id", "State", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addBusinessUnits(BusinessUnitsVIewModel model)
        {
           try
            {
                if (ModelState.IsValid)
                {
                    var existingUnit = ConfigManager.DoesUnitExstInLocation(model.LocationId.Value, model.unitname);
                    int? location = null;
                    BusinessUnits unit = new BusinessUnits();
                    if (model.LocationId.HasValue)
                    {
                        location = model.LocationId.Value;
                    }
                    if (existingUnit)
                    {
                        //ModelState.AddModelError("", "Please Unit alreasy existing with same name in this location");
                        ViewBag.Error = "Please Unit alreasy existing with same name in this location. Kindly try using another name or a different Location";
                        ViewBag.Locations = new SelectList(LocationRepo.Get().OrderBy(x => x.State), "Id", "State", "Id");
                        return View(model);
                    }
                    unit.LocationId = location ?? null;
                    unit.descriptions = model.descriptions;
                    unit.unitcode = model.unitcode;
                    unit.unitname = model.unitname;
                    unit.startdate = unit.startdate;
                    unit.createdby = User.Identity.GetUserId();
                    unit.createddate = DateTime.Now;
                    unit.modifiedby = User.Identity.GetUserId();
                    unit.modifieddate = DateTime.Now;
                    unit.isactive = true;
                    BusinessRepo.Insert(unit);

                    TempData["Success"] = string.Format($"{unit.unitname} has been created");
                    return RedirectToAction("AllBusinessUnits");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                throw ex;
            }
            
                TempData["Error"] = "Something went wrong. please make sure you fill all the appropriate details";
            ViewBag.Locations = new SelectList(LocationRepo.Get().OrderBy(x => x.State), "Id", "State", "Id");
            return View();
        }

        public ActionResult EditUnit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var unit = BusinessRepo.GetById(id.Value);
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
                BusinessRepo.update(units);
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
            BusinessUnits unit = BusinessRepo.GetById(id);
            return View(unit);
        }
        public ActionResult deleteUnit(int id)
        {
            BusinessUnits unit = BusinessRepo.GetById(id);
            var unitName = unit.unitname;
            if (unit == null)
            {
                return HttpNotFound();
            }
                TempData["unitName"] = string.Format($"{unitName} has been deleted");
                BusinessRepo.Delete(id);
                return RedirectToAction("AllBusinessUnits");
        }

        public ActionResult AllDepartment()
        {        
            return View(DeptRepo.Getdepartment());
        }

        public ActionResult addDepartment()
        {
            ViewBag.businessUnits = new SelectList(BusinessRepo.Get().OrderBy(x => x.unitname), "BusId", "unitname");
            return View(new Departments());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addDepartment(DepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Departments depts = new Departments()
                {
                    BunitId = model.BunitId.Value,
                    deptcode = model.deptcode,
                    deptname = model.deptname,
                    startdate = model.StartDate,
                    CreatedDate = model.CreatedDate,
                    descriptions = model.descriptions,
                    CreatedBy = User.Identity.GetUserId(),
                    ModifiedBy = User.Identity.GetUserId(),
                    ModifiedDate = DateTime.Now,
                    Isactive = true

                };
                DeptRepo.addepartment(depts);
                TempData["Success"] = string.Format($"{depts.deptname} has been created");
                return RedirectToAction("AllDepartment");
            }

                ViewBag.businessUnits = new SelectList(BusinessRepo.Get().OrderBy(x => x.unitname), "BusId", "unitname", "BusId");
                TempData["Error"] = "Something went wrong. please make sure you fill all the appropriate details";
                return View(model);
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
                return View(dept);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDepartment(Departments dept)
        {
            if (ModelState.IsValid)
            {
                TempData["Success"] = string.Format($"{dept.deptname} has been Updated");
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
            return PartialView(JobRepo.Get());
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
                JobRepo.Insert(job);
                TempData["Success"] = string.Format($"{job.jobtitlename} has been created");
                ModelState.Clear();
                return Redirect(returnUrl);
            }

                TempData["Error"] = "Something went wrong. please make sure you fill all the appropriate details";
                return PartialView(jobs);
        }

        public ActionResult AllPosition()
        {
            return PartialView(positionRepo.Get());
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
                    positionRepo.Insert(position);
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
                    status.employemntStatus = model.employemnt_status;
                    status.createdby = null;
                    status.createddate = DateTime.Now;
                    status.modifiedby = null;
                    status.modifieddate = DateTime.Now;
                    status.isactive = true;
                    statusRepo.Insert(status);
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
                ModelState.Clear();
               return RedirectToAction("AllLeaveType");
            }
            ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            TempData["Error"] = "Something went wrong try again later";
            return View(model);
        }

        public ActionResult AddLevel(string retunUrl)
        {
            ViewBag.returnUrl = retunUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLevel(LevelsViewModel model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Levels level = new Levels();
                    level.LevelName = model.LevelName;
                    level.levelNo = model.levelNo;
                    level.EligibleYears = model.EligibleYears;
                    level.CreatedBy = User.Identity.GetUserId();
                    level.ModifiedBy = User.Identity.GetUserId();
                    level.CreatedOn = DateTime.Now;
                    level.ModifiedOn = DateTime.Now;
                    levelRepo.Insert(level);
                    ModelState.Clear();
                    return Redirect(returnUrl);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
                //throw ex;
            }
            ModelState.AddModelError("", "Please review the form and resend");
            return View(model);
        }

        public ActionResult AddLocation(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLocation(LocationViewModel model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Location location = new Location();
                    location.City = model.City;
                    location.Country = model.Country;
                    location.State = model.State;
                    location.Address1 = model.Address1;
                    location.Address2 = model.Address2;
                    location.CreatedBy = User.Identity.GetUserId();
                    location.ModifiedBy = User.Identity.GetUserId();
                    location.CreatedOn = DateTime.Now;
                    location.ModifiedOn = DateTime.Now;
                    LocationRepo.Insert(location);
                    ModelState.Clear();
                    return Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                throw ex;
            }
            ModelState.AddModelError("", "please refill the form and make try submitting again");
            return View(model);
        }
        public ActionResult AddCareer(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCareer(CareerViewModel model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Careers career = new Careers();
                    career.CareerName = model.CareerName;
                    career.ShortCode = model.ShortCode;
                    career.CreatedBy = User.Identity.GetUserId();
                    career.ModifiedBy = User.Identity.GetUserId();
                    career.CreatedOn = DateTime.Now;
                    career.ModifiedOn = DateTime.Now;
                    careerRepo.Insert(career);
                    ModelState.Clear();
                    return Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                throw ex;
            }
            ModelState.AddModelError("", "please refill the form and make try submitting again");
            return View(model);
        }
    }
}

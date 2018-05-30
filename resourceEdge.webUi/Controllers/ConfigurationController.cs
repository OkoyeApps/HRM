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
using resourceEdge.webUi.Infrastructure.Handlers;
using resourceEdge.webUi.Infrastructure.Core;
using resourceEdge.webUi.Infrastructure.SystemManagers;

namespace resourceEdge.webUi.Controllers
{
    [CustomAuthorizationFilter]
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
        IGroups GroupRepo;
        ConfigurationManager ConfigManager;
        DropDownManager DropDown;
        public ApplicationUserManager UserManager
        {
            get { return userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { userManager = value; }
        }
        public ConfigurationController(IBusinessUnits BParam, IDepartments DParam, IidentityCodes idCodes, IJobtitles jParam,
            IPositions pParam, IPrefixes prparam, IEmploymentStatus status, ILeaveManagement lParam, ILevels levelParam,
            ILocation locationParam, ICareers CareerParam, IGroups Gparam)
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
            this.GroupRepo = Gparam;
            ConfigManager = new ConfigurationManager(BParam);
            DropDown = new DropDownManager();
        }
        public ActionResult Index()
        {
            return View("Configuration");
        }
        [CustomAuthorizationFilter(Roles = "Super Admin")]
        public ActionResult AllCodes()
        {
            ViewBag.PageTitle = "All Identity Codes";
            var result = ConfigManager.GetAllIdentityCodes();
            return View(result);
        }
        [CustomAuthorizationFilter(Roles = "Super Admin")]
        public ActionResult AddCode(string returnUrl, string previousUrl)
        {

            ViewBag.returnUrl = returnUrl;
            ViewBag.previousUrl = previousUrl;
            ViewBag.PageTitle = "Add Code";
            ViewBag.Layout = "~/Views/Shared/Layouts/_HrLayout.cshtml";
            ViewBag.Groups = DropDown.GetGroup();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Route("addCode")]
        public ActionResult AddCode(IdentityCode codes, string returnUrl)
        {
            IdentityCode code = codes;
            var existngCode = ConfigManager.DoesIdentityCodeExistForGroup(code.GroupId);
            if (existngCode != true)
            {
                if (ModelState.IsValid)
                {
                    code.createddate = DateTime.Now;
                    code.modifiedBy = User.Identity.GetUserId();
                    code.createdBy = User.Identity.GetUserId();
                    code.createdBy = null;
                    code.createdBy = null;
                    IdentityRepo.Insert(code);
                    ModelState.Clear();
                    this.AddNotification("Code created successfully", NotificationType.SUCCESS);
                    return Redirect(Request.UrlReferrer.AbsolutePath);
                }

            }
            this.AddNotification($"Sorry Identity Code has been Added for this Group already", NotificationType.ERROR);
            return RedirectToAction("AddCode");
        }
        [CustomAuthorizationFilter(Roles = "Super Admin")]
        public ActionResult EditCode(int id = 1)
        {
            ViewBag.PageTitle = "Edit Identity Code";
            var code = IdentityRepo.GetById(id);
            return View(code);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCode([Bind(Include = "employee_code,backgroundagency_code,vendors_code,staffing_code,users_code,requisition_code")] IdentityCode code, int id)
        {
            if (ModelState.IsValid)
            {
                var codeToUpdate = IdentityRepo.GetById(id);
                if (codeToUpdate != null)
                {
                    var result = ConfigManager.ChangeAllEmployeeCodes(codeToUpdate.employee_code, code.employee_code);
                    if (result != false)
                    {
                        codeToUpdate.employee_code = code.employee_code;
                        codeToUpdate.requisition_code = code.requisition_code;
                        codeToUpdate.users_code = code.users_code;
                        codeToUpdate.vendors_code = code.vendors_code;
                        codeToUpdate.backgroundagency_code = code.backgroundagency_code;
                        codeToUpdate.modifiedBy = User.Identity.GetUserId();
                        codeToUpdate.modifieddate = DateTime.Now;
                        IdentityRepo.update(codeToUpdate);
                        this.AddNotification($"|{ Request.Url.AbsolutePath}", NotificationType.SUCCESS);
                    }
                }
                return RedirectToAction("AllCodes");
            }
            else
            {
                this.AddNotification($"Something went wrong. please make sure you fill all the appropriate details|{ Request.Url.AbsolutePath}", NotificationType.ERROR);
                return View();
            }
        }
        [CustomAuthorizationFilter(Roles = "System Admin,HR")]
        public ActionResult AllPrefix()
        {
            ViewBag.PageTitle = "All Prefixes";
            return View(ConfigManager.GetAllPrefix());
        }
        [CustomAuthorizationFilter(Roles = "System Admin,HR")]
        public ActionResult addPrefix(string returnUrl, string previousUrl)
        {
            ViewBag.returnUrl = returnUrl;
            ViewBag.previousUrl = previousUrl;
            ViewBag.PageTitle = "Add Prefix";
            ViewBag.Layout = "~/Views/Shared/Layouts/_HrLayout.cshtml";
            return View(new prefixViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addPrefix(FormCollection collection, prefixViewModel model, string returnUrl, string previousUrl)
        {
            if (!collection.AllKeys.Contains("prefixName[0]"))
            {
                if (ModelState.IsValid)
                {
                    Prefix prefixes = new Prefix()
                    {
                        prefixName = model.prefixName,
                        createdby = null,
                        createddate = DateTime.Now,
                        modifiedby = null,
                        isactive = true
                    };
                    prefixRepo.Insert(prefixes);
                    this.AddNotification($"Prefix(es) added", NotificationType.SUCCESS);
                    return Redirect(Request.UrlReferrer.AbsolutePath);

                }
            }
            else
            {
                var result = ConfigManager.AddorUpdatePrefix(collection);
                if (result != false)
                {
                    this.AddNotification("Prefix(es) added", NotificationType.SUCCESS);
                    return RedirectToAction("addPrefix");
                }
            }
            this.AddNotification($"Oops! Operation failed, please make sure all required fields are filled and failure continues, please contact your system administrator", NotificationType.ERROR);
            return View(model);
        }
        [CustomAuthorizationFilter(Roles = "System Admin,HR")]
        public ActionResult EditPrefix(int id)
        {
            var prefix = prefixRepo.GetById(id);
            if (prefix != null)
            {
                ViewBag.PageTitle = "Edit Unit";
                return View(prefix);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditPrefix(Prefix model)
        {
            var oldPrefix = prefixRepo.GetById(model.Id);
            if (oldPrefix != null)
            {
                oldPrefix.prefixName = model.prefixName;
                oldPrefix.modifiedby = User.Identity.GetUserId();
                oldPrefix.modifieddate = DateTime.Now;
                prefixRepo.update(oldPrefix);
                this.AddNotification("Yay! Prefix Updated", NotificationType.SUCCESS);
                return RedirectToAction("AllPrefix");
            }
            this.AddNotification("Oops! Operation failed, please make sure all required fields are filled and failure continues, please contact your system administrator", NotificationType.ERROR);
            return View(model);
        }
        [CustomAuthorizationFilter(Roles = "System Admin,HR, Super Admin")]
        public ActionResult AllBusinessUnits()
        {
            ViewBag.PageTitle = "All Business Units";
            return View(ConfigManager.GetAllBusinessUnit());
        }
        [CustomAuthorizationFilter(Roles = "Super Admin, System Admin")]
        public ActionResult addBusinessUnits()
        {
            ViewBag.PageTitle = "Add Business Unit";
            ViewBag.Locations = DropDown.GetLocation();
            ViewBag.Groups = DropDown.GetGroup();
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
                    if (model.startdate.HasValue)
                    {
                        if (model.startdate.Value.Date > DateTime.Now.Date)
                        {
                            this.AddNotification("Please the system does not recognise units created for the future. please try again with a date less than or equal to today", NotificationType.ERROR);
                            return RedirectToAction("addBusinessUnits");
                        }
                    }
                    var existingUnit = ConfigManager.DoesUnitExstInLocation(model.LocationId.Value, model.unitname);
                    int? location = null;
                    BusinessUnit unit = new BusinessUnit();
                    if (model.LocationId.HasValue)
                    {
                        location = model.LocationId.Value;
                    }
                    if (existingUnit)
                    {

                        this.AddNotification($"Please Unit already exist with same name for this location. Kindly try using another name or a different Location", NotificationType.ERROR);
                        ViewBag.Locations = DropDown.GetLocation();
                        return View(model);
                    }
                    unit.LocationId = location ?? null;
                    unit.descriptions = model.descriptions;
                    unit.unitcode = model.unitcode;
                    unit.unitname = model.unitname;
                    unit.startdate = model.startdate;
                    unit.GroupId = model.GroupId;
                    unit.createdby = User.Identity.GetUserId();
                    unit.createddate = DateTime.Now;
                    unit.modifiedby = User.Identity.GetUserId();
                    unit.modifieddate = DateTime.Now;
                    unit.isactive = true;
                    BusinessRepo.Insert(unit);

                    this.AddNotification($"Unit added, Now you can add departments to this unit", NotificationType.SUCCESS);
                    return RedirectToAction("addBusinessUnits");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                throw ex;
            }

            this.AddNotification($"Oops! Operation failed, please make sure all required fields are filled and failure continues, please contact your system administrator", NotificationType.ERROR);
            ViewBag.PageTitle = "Add Business Unit";
            ViewBag.Locations = DropDown.GetLocation();
            ViewBag.Groups = DropDown.GetGroup();
            return View(model);
        }
        [CustomAuthorizationFilter(Roles = "Super Admin")]
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
                ViewBag.PageTitle = "Edit Unit";
                return View(unit);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditUnit(BusinessUnit units)
        {
            if (ModelState.IsValid)
            {
                var currentUnit = BusinessRepo.GetById(units.Id);
                if (currentUnit != null)
                {
                    currentUnit.unitcode = units.unitcode;
                    currentUnit.unitname = units.unitname;
                    currentUnit.startdate = units.startdate != null ? units.startdate : currentUnit.startdate;
                    currentUnit.modifiedby = User.Identity.GetUserId();
                    currentUnit.modifieddate = DateTime.Now;
                    this.AddNotification($"", NotificationType.SUCCESS);
                    BusinessRepo.update(currentUnit);
                    return RedirectToAction("AllBusinessUnits");
                }
            }
            this.AddNotification($"Oops! Operation failed, please make sure all required fields are filled and failure continues, please contact your system administrator", NotificationType.ERROR);
            return View(units);
        }

        public ActionResult unitDetail(int id)
        {
            BusinessUnit unit = BusinessRepo.GetById(id);
            return View(unit);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult deleteUnit(int id)
        {
            BusinessUnit unit = BusinessRepo.GetById(id);
            var unitName = unit.unitname;
            if (unit == null)
            {
                return HttpNotFound();
            }
            this.AddNotification($"Deleted!", NotificationType.SUCCESS);
            BusinessRepo.Delete(id);
            return RedirectToAction("AllBusinessUnits");
        }
        [CustomAuthorizationFilter(Roles = "System Admin,HR, Super Admin")]
        public ActionResult AllDepartment()
        {
            ViewBag.PageTitle = "All Departments";
            var result = ConfigManager.GetAllDepartment();
            return View(result);
        }
        [CustomAuthorizationFilter(Roles = "System Admin,HR")]
        public ActionResult addDepartment()
        {
            var userFromSession = (SessionModel)Session[""];
            ViewBag.businessUnits = DropDown.GetBusinessUnit(userFromSession.GroupId, userFromSession.LocationId);
            ViewBag.PageTitle = "Add Department";
            return View(new DepartmentViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addDepartment(DepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingDept = ConfigManager.DoesDeptartmentExistInLocation(model.BunitId, model.deptname);
                if (existingDept)
                {
                    this.AddNotification("Please Department already exist with same name for this Unit. Kindly try using another name or a different Location", NotificationType.ERROR);
                    //ViewBag.businessUnits = DropDown.GetBusinessUnit();
                    //ViewBag.Locations = DropDown.GetLocation();
                    //return RedirectToAction("addDepartment");
                    return View(model);
                }
                if (model.StartDate.HasValue && model.StartDate.Value.Date > DateTime.Now.Date)
                {
                    this.AddNotification("Sorry, please system does not allow departments to be created in the Future", NotificationType.ERROR);
                    return RedirectToAction("addDepartment");
                }
                Departments depts = new Departments()
                {
                    BusinessUnitsId = model.BunitId,
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
                this.AddNotification($"Department added!", NotificationType.SUCCESS);
                return RedirectToAction("addDepartment");
            }

            ViewBag.businessUnits = DropDown.GetBusinessUnit();
            this.AddNotification($"Oops! Operation failed, please make sure all required fields are filled and failure continues, please contact your system administrator", NotificationType.ERROR);
            return View(model);
        }
        [CustomAuthorizationFilter(Roles = "System Admin,HR")]
        public ActionResult EditDepartment(int id)
        {
            Departments dept = DeptRepo.GetdepartmentById(id);
            if (dept == null)
            {
                return HttpNotFound();
            }
            else
            {
                ViewBag.PageTitle = "Edit Department";
                return View(dept);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditDepartment(Departments dept)
        {
            if (ModelState.IsValid)
            {
                var oldDepartment = DeptRepo.GetdepartmentById(dept.Id);
                if (oldDepartment != null)
                {
                    oldDepartment.deptcode = dept.deptcode;
                    oldDepartment.deptname = dept.deptname;
                    oldDepartment.ModifiedBy = User.Identity.GetUserId();
                    oldDepartment.ModifiedDate = DateTime.Now;
                    this.AddNotification($"Yay Edited!", NotificationType.SUCCESS);
                    DeptRepo.Updatedepartment(oldDepartment);
                }
                return RedirectToAction("AllDepartment");
            }
            this.AddNotification($"Oops! Operation failed, please make sure all required fields are filled and failure continues, please contact your system administrator", NotificationType.ERROR);
            return View(dept);
        }
        [CustomAuthorizationFilter(Roles = "System Admin,HR")]
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
                this.AddNotification($"", NotificationType.SUCCESS);
                DeptRepo.DeleteDepartment(id);
                return RedirectToAction("AllDepartment");
            }
        }
        [CustomAuthorizationFilter(Roles = "System Admin,HR")]
        public ActionResult allJob()
        {
            ViewBag.PageTitle = "All Jobs";
            return View(ConfigManager.GetAllJob());
        }
        [CustomAuthorizationFilter(Roles = "System Admin,HR")]
        public ActionResult AddJobTitle(string returnUrl, string previousUrl)
        {
            ViewBag.returnUrl = returnUrl;
            ViewBag.previousUrl = previousUrl;
            ViewBag.PageTitle = "Add Job Title";
            ViewBag.Layout = "~/Views/Shared/Layouts/_HrLayout.cshtml";
            ViewBag.Groups = DropDown.GetGroup();
            return View(new Jobtitle());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddJobTitle(string returnUrl, FormCollection collection = null, Jobtitle jobs = null)
        {
            if (!collection.AllKeys.Contains("jobtitlename[0]"))
            {
                if (ModelState.IsValid || string.IsNullOrEmpty(jobs.GroupId.ToString()))
                {
                    if (User.IsInRole("HR"))
                    {
                        var userSessionDetail = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
                        if (userSessionDetail != null)
                        {
                            jobs.GroupId = userSessionDetail.GroupId;
                            Jobtitle job = jobs;
                            job.createdby = null;
                            job.modifieddate = DateTime.Now;
                            JobRepo.Insert(job);
                            ModelState.Clear();
                        }
                    }
                    this.AddNotification($"Job Added!", NotificationType.SUCCESS);
                    return Redirect(Request.UrlReferrer.AbsolutePath);
                }
            }
            else
            {
                var result = ConfigManager.AddOrUpdateJobs(collection);

                if (result)
                {
                    this.AddNotification($"Job Added", NotificationType.SUCCESS);
                    return RedirectToAction("AddJobTitle");
                }
            }

            this.AddNotification($"Oops! Operation failed, please make sure all required fields are filled and failure continues, please contact your system administrator", NotificationType.ERROR);

            return RedirectToAction("AddJobTitle");
        }
        [CustomAuthorizationFilter(Roles = "System Admin,HR")]
        public ActionResult EditJob(int id)
        {
            var job = JobRepo.GetById(id);
            if (job != null)
            {
                ViewBag.PageTitle = "Edit Job";
                return View(job);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditJob(Jobtitle job)
        {
            var oldJob = JobRepo.GetById(job.Id);
            if (oldJob != null)
            {
                oldJob.jobpayfrequency = job.jobpayfrequency;
                oldJob.jobpaygradecode = job.jobpaygradecode;
                oldJob.jobtitlecode = job.jobtitlecode;
                oldJob.jobtitlename = job.jobtitlename;
                oldJob.minexperiencerequired = job.minexperiencerequired;
                oldJob.modifiedby = User.Identity.GetUserId(); ;
                oldJob.modifieddate = DateTime.Now;
                JobRepo.update(oldJob);
                this.AddNotification("Yay! Job Edited", NotificationType.SUCCESS);
                return RedirectToAction("allJob");
            }
            this.AddNotification("Oops! Operation failed, please make sure all required fields are filled and failure continues, please contact your system administrator", NotificationType.ERROR);
            return View(job);
        }
        public ActionResult AllPosition()
        {
            ViewBag.PageTitle = "All Positions";
            return View(ConfigManager.GetAllPosition());
        }
        [CustomAuthorizationFilter(Roles = "System Admin,HR")]
        public ActionResult addPosition(string returnUrl, string previousUrl)
        {
            ViewBag.returnUrl = returnUrl;
            ViewBag.previousUrl = previousUrl;
            ViewBag.PageTitle = "Add Position";
            ViewBag.Layout = "~/Views/Shared/Layouts/_HrLayout.cshtml";
            ViewBag.jobTitles = DropDown.GetJobtitle();
            return View(new Position());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addPosition(Position model = null, FormCollection collection = null, string returnUrl = null)
        {
            if (!collection.AllKeys.Contains("positionname[0]"))
            {
                if (ModelState.IsValid)
                {
                    Position position = model;
                    position.createdby = User.Identity.GetUserId();
                    position.modifiedby = User.Identity.GetUserId();
                    position.modifieddate = DateTime.Now;
                    positionRepo.Insert(position);
                    ModelState.Clear();
                    this.AddNotification($"Position Added!", NotificationType.SUCCESS);
                    return Redirect(Request.UrlReferrer.AbsolutePath);
                }
            }
            else
            {
                var result = ConfigManager.AddOrUpdatePosition(collection);
                if (result)
                {
                    this.AddNotification($"Position Added!", NotificationType.SUCCESS);
                    return RedirectToAction("addPosition");
                }
            }
            ViewBag.jobTitles = DropDown.GetJobtitle();
            this.AddNotification($"Oops! Operation failed, please make sure all required fields are filled and failure continues, please contact your system administrator", NotificationType.ERROR);
            return RedirectToAction("addPosition");
        }
        [CustomAuthorizationFilter(Roles = "System Admin,HR")]
        public ActionResult EditPosition(int id)
        {
            var position = positionRepo.GetById(id);
            if (position != null)
            {
                ViewBag.PageTitle = "Edit Position";
                return View(position);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditPosition(Position model)
        {
            var oldPosition = positionRepo.GetById(model.Id);
            if (oldPosition != null)
            {
                oldPosition.positionname = model.positionname;
                oldPosition.modifiedby = User.Identity.GetUserId();
                oldPosition.modifieddate = DateTime.Now;
                positionRepo.update(oldPosition);
                this.AddNotification("Position Updated", NotificationType.SUCCESS);
                return RedirectToAction("AllPosition");
            }
            this.AddNotification("Oops! Operation failed, please make sure all required fields are filled and failure continues, please contact your system administrator", NotificationType.ERROR);
            return View(model);
        }
        [CustomAuthorizationFilter(Roles = "System Admin,HR")]
        public ActionResult allEmploymentStatus()
        {
            ViewBag.PageTitle = "All Employment Status";
            var result = ConfigManager.GetAllEmploymentStatus();
            return View(result);
        }
        [CustomAuthorizationFilter(Roles = "System Admin,HR")]
        [HttpGet]
        public ActionResult addEmploymentStatus(string returnUrl, string previousUrl)
        {
            ViewBag.status = returnUrl;
            ViewBag.previousUrl = previousUrl;
            ViewBag.PageTitle = "Add Employement Status";
            ViewBag.Layout = "~/Views/Shared/Layouts/_HrLayout.cshtml";
            return View(new employeeStatusViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addEmploymentStatus(employeeStatusViewModel model = null, FormCollection collection = null)
        {

            if (!collection.AllKeys.Contains("employementStatus[0]"))
            {
                if (ModelState.IsValid)
                {
                    EmploymentStatus status = new EmploymentStatus();
                    status.employemntStatus = model.employemntStatus;
                    status.createdby = null;
                    status.createddate = DateTime.Now;
                    status.modifiedby = null;
                    status.modifieddate = DateTime.Now;
                    status.isactive = true;
                    statusRepo.Insert(status);
                    ModelState.Clear();
                    this.AddNotification($"Employment status added!", NotificationType.SUCCESS);
                    return Redirect(Request.UrlReferrer.AbsolutePath);
                }
            }
            else
            {
                var result = ConfigManager.AddorUpdateEmploymentStatus(collection);
                if (result)
                {
                    this.AddNotification($"Employement status added!", NotificationType.SUCCESS);
                    return RedirectToAction("addEmploymentStatus");
                }
            }
            // ModelState.AddModelError($"", "Something went wrong.\n please make sure your data's are valid \n if the problem persist contact the system Administrator");
            this.AddNotification($"Oops! Operation failed, please make sure all required fields are filled and failure continues, please contact your system administrator", NotificationType.ERROR);
            return RedirectToAction("addEmploymentStatus");
        }
        [CustomAuthorizationFilter(Roles = "System Admin,HR")]
        public ActionResult EditEmpStatus(int id)
        {
            var stat = statusRepo.GetById(id);
            if (stat != null)
            {
                ViewBag.PageTitle = "Edit Employment Status";
                return View(stat);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditEmpstatus(EmploymentStatus stat)
        {
            var oldStat = statusRepo.GetById(stat.empstId);
            if (oldStat != null)
            {
                oldStat.employemntStatus = stat.employemntStatus;
                oldStat.modifiedby = User.Identity.GetUserId(); ;
                oldStat.modifieddate = DateTime.Now;
                statusRepo.update(oldStat);
                this.AddNotification("Yay Edited!", NotificationType.SUCCESS);
                return RedirectToAction("allEmployementStatus");
            }
            this.AddNotification("Oops! Operation failed, please make sure all required fields are filled and failure continues, please contact your system administrator", NotificationType.ERROR);
            return View(stat);
        }
        [CustomAuthorizationFilter(Roles = "System Admin,HR")]
        public ActionResult AllLeaveType()
        {
            ViewBag.PageTitle = "All Types of available Leaves";
            return View(ConfigManager.GetAllLeaveType());
        }
        [CustomAuthorizationFilter(Roles = "System Admin,HR")]
        public ActionResult AddLeaveType()
        {
            ViewBag.PageTitle = "Add Leave Type";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLeaveType(LeaveTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                EmployeeLeaveType leaveType = new EmployeeLeaveType();
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
                this.AddNotification($"Leave type Added!", NotificationType.SUCCESS);
                return RedirectToAction("AddLeaveType");
            }
            this.AddNotification("Oops! please make sure all required fields are filled and failure continues, please contact your system administrator", NotificationType.ERROR);
            return View(model);
        }
        public ActionResult EditLeaveType(int id)
        {
            var stat = leaveRepo.GetLeaveTypeById(id);
            if (stat != null)
            {
                ViewBag.PageTitle = "Edit Leave Type";
                return View(stat);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditLeaveType(EmployeeLeaveType leave)
        {
            var oldLeave = leaveRepo.GetLeaveTypeById(leave.id);
            if (oldLeave != null)
            {
                oldLeave.leavecode = leave.leavecode;
                oldLeave.leavetype = leave.leavetype;
                oldLeave.numberofdays = leave.numberofdays;
                oldLeave.modifiedby = User.Identity.GetUserId();
                oldLeave.modifieddate = DateTime.Now;
                leaveRepo.UpdateLeaveType(oldLeave);
                this.AddNotification("Yay! Leave Edited", NotificationType.SUCCESS);
                return RedirectToAction("AllLeaveType");
            }
            this.AddNotification("Oops! Operation failed, please make sure all required fields are filled and failure continues, please contact your system administrator", NotificationType.ERROR);
            return View("AllLeaveType");
        }
        [CustomAuthorizationFilter(Roles = "System Admin,HR")]
        public ActionResult AddLevel(string retunUrl, string previousUrl)
        {
            ViewBag.returnUrl = retunUrl;
            ViewBag.previousUrl = previousUrl;
            ViewBag.PageTitle = "Add Level";
            ViewBag.Layout = "~/Views/Shared/Layouts/_HrLayout.cshtml";
            ViewBag.Groups = DropDown.GetGroup();

            return View();
        }
        [CustomAuthorizationFilter(Roles = "System Admin,HR")]
        public ActionResult AllLevel()
        {
            ViewBag.PageTitle = "All Levels";
            return View(ConfigManager.GetAllLevel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLevel(LevelsViewModel model = null, FormCollection collection = null)
        {
            if (!collection.AllKeys.Contains("LevelName[0]"))
            {
                if (ModelState.IsValid)
                {
                    var userSessionDetail = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
                    var validLevel = ConfigManager.ValidateLevel(model.LevelName, model.levelNo, userSessionDetail.GroupId);
                    if (!validLevel) //Actually this check in logic is the not operation of the operation, therefore if it is valid then it checks it with the false value.
                    {
                        Level level = new Level();
                        level.LevelName = model.LevelName;
                        level.levelNo = model.levelNo;
                        level.EligibleYears = model.EligibleYears;
                        level.CreatedBy = User.Identity.GetUserId();
                        level.ModifiedBy = User.Identity.GetUserId();
                        level.CreatedOn = DateTime.Now;
                        level.ModifiedOn = DateTime.Now;
                        level.GroupId = userSessionDetail.GroupId;
                        levelRepo.Insert(level);
                        ModelState.Clear();
                        this.AddNotification("Level Added", NotificationType.SUCCESS);
                        return Redirect(Request.UrlReferrer.AbsolutePath);
                    }
                    this.AddNotification("Oops! please this level or the name used already exist in the system", NotificationType.ERROR);
                    ViewBag.Groups = DropDown.GetGroup();
                    return View(model);
                }
            }
            else
            {
                var result = ConfigManager.AddOrUpdateLevel(collection);
                if (result)
                {
                    this.AddNotification($"Level Added!", NotificationType.SUCCESS);
                    return RedirectToAction("AddLevel");
                }
            }
            this.AddNotification("Oops! Operation failed, please make sure all required fields are filled and failure continues, please contact your system administrator", NotificationType.ERROR);
            ViewBag.Groups = DropDown.GetGroup();
            return View(model);
        }
        [CustomAuthorizationFilter(Roles = "System Admin,HR")]
        public ActionResult Editlevel(int id)
        {
            var level = levelRepo.GetById(id);
            if (level != null)
            {
                ViewBag.PageTitle = "Edit Level";
                return View(level);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Editlevel(Level level)
        {
            var oldLevel = levelRepo.GetById(level.Id);
            if (oldLevel != null)
            {
                oldLevel.LevelName = level.LevelName;
                oldLevel.levelNo = level.levelNo;
                oldLevel.EligibleYears = level.EligibleYears;
                oldLevel.ModifiedBy = User.Identity.GetUserId();
                oldLevel.ModifiedOn = DateTime.Now;
                levelRepo.update(oldLevel);
                this.AddNotification("Yay! Level Updated", NotificationType.SUCCESS);
                return RedirectToAction("AllLevel");
            }
            this.AddNotification("Something went wrong, please try again", NotificationType.ERROR);
            return View("AllLevel");
        }
        [CustomAuthorizationFilter(Roles = "System Admin, Super Admin")]
        public ActionResult Alllocation()
        {
            ViewBag.PageTitle = "All Locations";
            return View(ConfigManager.GetAllLocation());
        }
        [CustomAuthorizationFilter(Roles = "System Admin, Super Admin")]
        public ActionResult AddLocation(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;

            ViewBag.pageTitle = "Add Location";
            ViewBag.Layout = "~/Views/Shared/Layouts/_HrLayout.cshtml";
            ViewBag.Groups = DropDown.GetGroup();
            return View();
        }
        [CustomAuthorizationFilter(Roles = "System Admin, Super Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLocation(LocationViewModel model = null, FormCollection collection = null)
        {
            if (!collection.AllKeys.Contains("Country[0]"))
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
                    location.GroupId = model.GroupId;
                    LocationRepo.Insert(location);
                    ModelState.Clear();
                    this.AddNotification("Location Added", NotificationType.SUCCESS);
                    return Redirect(Request.UrlReferrer.AbsolutePath);
                }
            }
            else
            {
                var result = ConfigManager.AddOrUpdateLocation(collection);
                if (result)
                {
                    this.AddNotification($"Location Added!", NotificationType.SUCCESS);
                    return RedirectToAction("AddLocation");
                }
            }
            this.AddNotification($"Oops! Operation failed, please make sure all required fields are filled and failure continues, please contact your system administrator", NotificationType.ERROR);
            ViewBag.Groups = DropDown.GetGroup();
            return View(model);
        }
        [CustomAuthorizationFilter(Roles = "System Admin, Super Admin")]
        public ActionResult Editlocation(int id)
        {
            var level = LocationRepo.GetById(id);
            if (level != null)
            {
                ViewBag.PageTitle = "Edit Location";
                return View(level);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Editlocation(Location location)
        {
            var oldLocation = LocationRepo.GetById(location.Id);
            if (oldLocation != null)
            {
                oldLocation.State = location.State;
                oldLocation.Address1 = location.Address1;
                oldLocation.City = location.City;
                oldLocation.Country = location.Country;
                oldLocation.Address2 = location.Address2;
                oldLocation.ModifiedBy = User.Identity.GetUserId();
                oldLocation.ModifiedOn = DateTime.Now;
                LocationRepo.update(oldLocation);
                this.AddNotification("Yay! Location Updated", NotificationType.SUCCESS);
                return RedirectToAction("Alllocation");
            }
            this.AddNotification("Oops! Operation failed, please make sure all required fields are filled and failure continues, please contact your system administrator", NotificationType.ERROR);
            return View("AllLevel");
        }

        public ActionResult AddCareer(string returnUrl, string previousUrl)
        {
            ViewBag.returnUrl = returnUrl;
            ViewBag.previousUrl = previousUrl;
            ViewBag.PageTitle = "Add Career";
            ViewBag.Layout = "~/Views/Shared/Layouts/_HrLayout.cshtml";
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
                    Career career = new Career();
                    career.CareerName = model.CareerName;
                    career.ShortCode = model.ShortCode;
                    career.CreatedBy = User.Identity.GetUserId();
                    career.ModifiedBy = User.Identity.GetUserId();
                    career.CreatedOn = DateTime.Now;
                    career.ModifiedOn = DateTime.Now;
                    careerRepo.Insert(career);
                    ModelState.Clear();
                    this.AddNotification($"Career Added!", NotificationType.SUCCESS);
                    return Redirect(Request.UrlReferrer.AbsolutePath);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                throw ex;
            }

            this.AddNotification($"Something went wrong, please try again", NotificationType.ERROR);
            return View(model);
        }
        [CustomAuthorizationFilter(Roles = "System Admin,Super Admin")]
        public ActionResult AllGroup()
        {
            ViewBag.PageTitle = "All Groups";
            return View(ConfigManager.GetAllGroup().OrderBy(x=>x.GroupName));
        }

        [CustomAuthorizationFilter(Roles = "System Admin, Super Admin")]
        public ActionResult AddGroup(string returnUrl, string previousUrl)
        {
            ViewBag.returnUrl = returnUrl;
            ViewBag.previousUrl = previousUrl;
            ViewBag.Layout = "~/Views/Shared/Layouts/_HrLayout.cshtml";
            ViewBag.PageTitle = "Add  Group";
            return View();
        }
        [CustomAuthorizationFilter(Roles = "System Admin, Super Admin"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddGroup(GroupViewModel model, string returnUrl)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    Group Group = new Group();
                    Group.GroupName = model.GroupName;
                    Group.Descriptions = model.Descriptions;
                    Group.CreatedBy = User.Identity.GetUserId();
                    Group.ModifiedBy = User.Identity.GetUserId();
                    Group.CreatedDate = DateTime.Now;
                    Group.ModifiedDate = DateTime.Now;
                    GroupRepo.Insert(Group);
                    ModelState.Clear();
                    this.AddNotification($"Group Added", NotificationType.SUCCESS);
                    return RedirectToAction("AllGroup");
                }
            }
            catch (Exception ex)
            {
                new Exception("Oops please this operation failed", ex.InnerException);
                ModelState.AddModelError("", ex.Message);
                throw ex;
            }
            this.AddNotification($"Something went wrong, Please try again", NotificationType.ERROR);
            return View(model);
        }
        [CustomAuthorizationFilter(Roles = "System Admin, Super Admin")]
        public ActionResult EditGroup(int id)
        {
            var group = GroupRepo.GetById(id);
            if (group != null)
            {
                ViewBag.PageTitle = "Edit Group";
                return View(group);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditGroup(Group group)
        {
            var oldGroup = GroupRepo.GetById(group.Id);
            if (oldGroup != null)
            {
                oldGroup.GroupName = group.GroupName;
                oldGroup.ModifiedBy = User.Identity.GetUserId(); ;
                oldGroup.ModifiedDate = DateTime.Now;
                GroupRepo.update(oldGroup);
                this.AddNotification("Yay Group Edited!", NotificationType.SUCCESS);
                return RedirectToAction("AllGroup");
            }
            this.AddNotification("Oops! Something went wrong, Please try again", NotificationType.ERROR);
            return View("AllGroup");
        }
        [NonAction]
        public bool validateDates(DateTime? dateOfJoining, DateTime? dateOfLeaveing)
        {
            if (dateOfJoining != null && dateOfJoining <= DateTime.Now)
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
    }
}

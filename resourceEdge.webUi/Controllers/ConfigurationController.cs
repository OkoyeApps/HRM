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
        IGroups GroupRepo;
        ConfigurationManager ConfigManager;
        Apimanager Apimanager;
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
            Apimanager = new Apimanager();
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
            ViewBag.PageTitle = "Add Code";
            ViewBag.Layout = "~/Views/Shared/Layouts/_HrLayout.cshtml";
            ViewBag.Groups = new SelectList(GroupRepo.Get().OrderBy(X => X.Id), "Id", "GroupName", "Id");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Route("addCode")]
        public ActionResult AddCode(IdentityCode codes, string returnUrl)
        {
            IdentityCode code = codes;
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
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Create", "HR");
            }
            else
            {
                TempData["Error"] = "Something went wrong. please make sure you fill all the appropriate details";
                return View(codes);
            }
        }
        public ActionResult EditCode(int id = 1)
        {
            var code = IdentityRepo.GetById(id);
            return View(code);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCode([Bind(Include = "employee_code,backgroundagency_code,vendors_code,staffing_code,users_code,requisition_code")] IdentityCode code)
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
        public ActionResult addPrefix(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            ViewBag.PageTitle = "Add Prefix";
            ViewBag.Layout = "~/Views/Shared/Layouts/_HrLayout.cshtml";
            return View(new prefixViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addPrefix(prefixViewModel model, string returnUrl)
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
                this.AddNotification("Prefix added Successfully", NotificationType.SUCCESS);
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Create", "HR");

            }
            else
            {
                this.AddNotification("Something went wrong. please make sure you fill all the appropriate details", NotificationType.ERROR);
                return View(model);
            }

        }
        public ActionResult AllBusinessUnits()
        {
            return View(BusinessRepo.Get());
        }
        public ActionResult addBusinessUnits()
        {
            ViewBag.PageTitle = "Add Business Unit";
            ViewBag.Locations = new SelectList(LocationRepo.Get().OrderBy(x => x.State), "Id", "State", "Id");
            ViewBag.Groups = new SelectList(GroupRepo.Get().OrderBy(x => x.GroupName), "Id", "GroupName", "Id");
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
                    BusinessUnit unit = new BusinessUnit();
                    if (model.LocationId.HasValue)
                    {
                        location = model.LocationId.Value;
                    }
                    if (existingUnit)
                    {
                    
                        this.AddNotification("Please Unit alreasy existing with same name in this location. Kindly try using another name or a different Location", NotificationType.ERROR);
                        ViewBag.Locations = new SelectList(LocationRepo.Get().OrderBy(x => x.State), "Id", "State", "Id");
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

                    this.AddNotification("Business unit Successfully Added", NotificationType.SUCCESS);
                    return RedirectToAction("addBusinessUnits");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                throw ex;
            }

            this.AddNotification("Something went wrong. please make sure you fill all the appropriate details", NotificationType.ERROR);
            ViewBag.Locations = new SelectList(LocationRepo.Get().OrderBy(x => x.State), "Id", "State", "Id");
            return View(model);
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
                return View(unit);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUnit(BusinessUnit units)
        {
            if (ModelState.IsValid)
            {
                this.AddNotification($"{units.unitname} has been modified", NotificationType.SUCCESS);
                BusinessRepo.update(units);
                return RedirectToAction("AllBusinessUnits");
            }
            else
            {
                this.AddNotification("Something went wrong. please make sure you fill all the appropriate details", NotificationType.ERROR);
                return View(units);
            }
        }

        public ActionResult unitDetail(int id)
        {
            BusinessUnit unit = BusinessRepo.GetById(id);
            return View(unit);
        }
        public ActionResult deleteUnit(int id)
        {
            BusinessUnit unit = BusinessRepo.GetById(id);
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
            ViewBag.businessUnits = new SelectList(BusinessRepo.Get().OrderBy(x => x.unitname), "Id", "unitname");
            ViewBag.PageTitle = "Add Department";
            return View(new DepartmentViewModel());
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
                this.AddNotification($"{depts.deptname} has been created", NotificationType.SUCCESS);
                return RedirectToAction("addDepartment");
            }

            ViewBag.businessUnits = new SelectList(BusinessRepo.Get().OrderBy(x => x.unitname), "Id", "unitname", "Id");
            this.AddNotification("Something went wrong. please make sure you fill all the appropriate details", NotificationType.ERROR);
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
                this.AddNotification($"{dept.deptname} has been Updated", NotificationType.SUCCESS);
                DeptRepo.Updatedepartment(dept);
                return View("AllDepartment");
            }
            this.AddNotification("Something went wrong. please make sure you fill all the appropriate details", NotificationType.ERROR);
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
                this.AddNotification($"{dept.deptname} has been deleted", NotificationType.SUCCESS);
                DeptRepo.DeleteDepartment(id);
                return RedirectToAction("AllDepartment");
            }
        }

        public ActionResult allJob()
        {
            return View(JobRepo.Get());
        }

        public ActionResult AddJobTitle(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            ViewBag.PageTitle = "Add Job Title";
            ViewBag.Layout = "~/Views/Shared/Layouts/_HrLayout.cshtml";
            ViewBag.Groups = new SelectList(GroupRepo.Get().OrderBy(X => X.Id), "Id", "GroupName", "Id");
            return View(new Jobtitle());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddJobTitle(Jobtitle jobs = null, FormCollection collection = null, string returnUrl = null)
        {
            if (jobs != null && !collection.AllKeys.Contains("jobtitlename[0]"))
            {
                if (ModelState.IsValid)
                {
                    Jobtitle job = jobs;
                    job.createdby = null;
                    job.modifieddate = DateTime.Now;
                    JobRepo.Insert(job);
                    this.AddNotification($"{job.jobtitlename} has been created", NotificationType.SUCCESS);
                    ModelState.Clear();
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("AddJobTitle");
                }
            }
            else
            {
                var result = ConfigManager.AddOrUpdateJobs(collection);
                if (result != false && returnUrl != null)
                {
                    this.AddNotification($"Operation Successful!", NotificationType.SUCCESS);
                    return Redirect(returnUrl);
                }
            }

            this.AddNotification("Something went wrong. please make sure you fill all the appropriate details", NotificationType.ERROR);
            return View(jobs);
        }

        public ActionResult AllPosition()
        {
            return View(positionRepo.Get());
        }

        public ActionResult addPosition(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            ViewBag.PageTitle = "Add Position";
            ViewBag.Layout = "~/Views/Shared/Layouts/_HrLayout.cshtml";
            ViewBag.jobTitles = new SelectList(Apimanager.JobList().OrderBy(x => x.JobName), "JobId", "JobName", "JobId");
            return View(new Position());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addPosition(Position model = null, FormCollection collection = null, string returnUrl = null)
        {
            if (model != null)
            {
                if (ModelState.IsValid)
                {
                    Position position = model;
                    position.createdby = User.Identity.GetUserId();
                    position.modifiedby = User.Identity.GetUserId(); 
                    position.modifieddate = DateTime.Now;
                    positionRepo.Insert(position);
                    ModelState.Clear();
                    this.AddNotification($"{position.positionname} has been created", NotificationType.SUCCESS);
                    if (returnUrl != null)
                    {
                        return RedirectToAction(returnUrl);
                    }
                    return RedirectToAction("addPosition");
                }
            }
            else
            {
                var result = ConfigManager.AddOrUpdatePosition(collection);
                if (result != false && returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else if (result != false && returnUrl == null)
                {
                    return RedirectToAction("Create", "HR");
                }
            }
            ViewBag.jobTitles = new SelectList(Apimanager.JobList().OrderBy(x => x.JobName), "JobId", "JobName", "JobId");
            this.AddNotification("Something went wrong. please make sure you fill all the appropriate details", NotificationType.ERROR);
            return View(model);
        }



        [HttpGet]
        public ActionResult addEmploymentStatus(string returnUrl)
        {
            ViewBag.status = returnUrl;
            ViewBag.PageTitle = "Add Employement Status";
            ViewBag.Layout = "~/Views/Shared/Layouts/_HrLayout.cshtml";
            return View(new employeeStatusViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addEmploymentStatus(employeeStatusViewModel model = null, FormCollection collection = null, string returnUrl = null)
        {

            if (model != null)
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
                    this.AddNotification($"{model.employemntStatus} has been created", NotificationType.SUCCESS);
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("addEmploymentStatus");
                }
            }
            else
            {
                var result = ConfigManager.AddorUpdateEmploymentStatus(collection);
                if (result != false && returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else if (result != false && returnUrl == null)
                {
                    return RedirectToAction("Create", "HR");
                }
            }
            ModelState.AddModelError("", "Something went wrong.\n please make sure your data's are valid \n if the problem persist contact the system Administrator");
            this.AddNotification("Something went wrong. please make sure you fill all the appropriate details", NotificationType.ERROR);
            return View(model);
        }


    public ActionResult AllLeaveType()
    {
        ViewBag.PageTitle = "All Types of available Leaves";
        return View(leaveRepo.GetLeaveTypes());
    }
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
            this.AddNotification("Operation Successful!", NotificationType.SUCCESS);
        }
        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
        this.AddNotification("Something went wrong try again later", NotificationType.ERROR);
        return View(model);
    }

    public ActionResult AddLevel(string retunUrl)
    {
        ViewBag.returnUrl = retunUrl;
        ViewBag.PageTitle = "Add Level";
        ViewBag.Layout = "~/Views/Shared/Layouts/_HrLayout.cshtml";
        ViewBag.Groups = new SelectList(GroupRepo.Get().OrderBy(X => X.Id), "Id", "GroupName", "Id");

            return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AddLevel(LevelsViewModel model = null, FormCollection collection = null, string returnUrl = null)
    {
        if (model != null)
        {

            if (ModelState.IsValid)
            {
                Level level = new Level();
                level.LevelName = model.LevelName;
                level.levelNo = model.levelNo;
                level.EligibleYears = model.EligibleYears;
                level.CreatedBy = User.Identity.GetUserId();
                level.ModifiedBy = User.Identity.GetUserId();
                level.CreatedOn = DateTime.Now;
                level.ModifiedOn = DateTime.Now;
                level.GroupId = model.GroupId;
                levelRepo.Insert(level);
                ModelState.Clear();
                this.AddNotification("Level Added SuccessFully Successful", NotificationType.SUCCESS);
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("AddLevel");
            }
        }
        else
        {
            var result = ConfigManager.AddOrUpdateLevel(collection);
            if (result != false && returnUrl != null)
            {
                return Redirect(returnUrl);
            }
            else if (result != false && returnUrl == null)
            {
                return RedirectToAction("Create", "HR");
            }
        }

        ModelState.AddModelError("", "Please review the form and resend");
        this.AddNotification("Something went wrong, please try again", NotificationType.ERROR);
        return View(model);
    }

    public ActionResult AddLocation(string returnUrl)
    {
        ViewBag.returnUrl = returnUrl;
        ViewBag.pageTitle = "Add Location";
        ViewBag.Layout = "~/Views/Shared/Layouts/_HrLayout.cshtml";
        ViewBag.Groups = new SelectList(GroupRepo.Get().OrderBy(X => X.Id), "Id", "GroupName", "Id");
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AddLocation(LocationViewModel model = null, FormCollection collection = null, string returnUrl = null)
    {
        if (model != null && !collection.AllKeys.Contains("Country[0]"))
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
                this.AddNotification("Location Added Successfully", NotificationType.SUCCESS);
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Create", "HR");
            }
        }
        else
        {
            var result = ConfigManager.AddOrUpdateLocation(collection);
            if (result != false && returnUrl != null)
            {
                return Redirect(returnUrl);
            }
            else if (result != false && returnUrl == null)
            {
                return RedirectToAction("AddLocation");
            }
        }

        ModelState.AddModelError("", "please refill the form and make try submitting again");
        this.AddNotification("Something went wrong, please try again", NotificationType.ERROR);
        return View(model);
    }
    public ActionResult AddCareer(string returnUrl)
    {
        ViewBag.returnUrl = returnUrl;
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
                this.AddNotification("Career successfully Added", NotificationType.SUCCESS);
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Create", "HR");
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            throw ex;
        }
        ModelState.AddModelError("", "please refill the form and try submitting again");
        this.AddNotification("Something went wrong, please try again", NotificationType.ERROR);
        return View(model);
    }

    public ActionResult AddGroup(string returnUrl)
    {
        ViewBag.returnUrl = returnUrl;
        ViewBag.Layout = "~/Views/Shared/Layouts/_HrLayout.cshtml";
        ViewBag.PageTitle = "Add  Group";
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
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
                this.AddNotification("Group Successfully Added!", NotificationType.SUCCESS);
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Create", "HR");
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            throw ex;
        }
        ModelState.AddModelError("", "please refill the form and make try submitting again");
        this.AddNotification("Something went wrong, Please try again", NotificationType.ERROR);
        return View(model);
    }
}
}

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.webUi.Infrastructure;
using resourceEdge.webUi.Infrastructure.Handlers;
using resourceEdge.webUi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using resourceEdge.webUi.Infrastructure.Core;
using static resourceEdge.webUi.Infrastructure.EmployeeManager;

namespace resourceEdge.webUi.Controllers
{
    public class EmployeeController : Controller
    {

        private ApplicationUserManager userManager;
        private IPayroll payRollRepo;
        IEmployees EmployeeRepo;
        IFiles FileRepo;
        IPositions PositionRepo;
        IJobtitles jobRepo;
        IQuestions QuestionRepo;
        EmployeeManager EmpManager;
        EmployeeDetails EmpDetails;
        EmployeeEdit EmpEdit;
        public EmployeeController(IPayroll PRParam, IEmployees EParam, IFiles fParam, IPositions PParam, IJobtitles jParam, ILeaveManagement LParam,IPayroll payParam, IQuestions Qparam )
        {
            payRollRepo = PRParam;
            EmployeeRepo = EParam;
            FileRepo = fParam;
            PositionRepo = PParam;
            jobRepo = jParam;
            QuestionRepo = Qparam;
            EmpManager = new EmployeeManager(EParam,fParam, LParam, payParam);
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            EmpDetails = new EmployeeDetails();
            EmpEdit = new EmployeeEdit(EParam,fParam,LParam,payParam);
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
        // Edit: Edit

        public ActionResult Index()
        {
            ViewBag.PageTitle = "All Employees";
            if (User.IsInRole("Super Admin"))
            {
                return View(EmpManager.GetAllEmployees());
            }
            var userSessionDetail = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            return View(EmpManager.GetAllEmployeeByLocation(userSessionDetail.LocationId));
        }
        public ActionResult Edit(int Id)
        {
            var userId = EmpManager.GetUserIdFromEmployeeTable(Id);
            if (!string.IsNullOrEmpty(userId))
            {
            ViewBag.UserId = Id;
            ViewBag.Avartar = EmpEdit.getEmpAvatar(userId);
            return View();
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeListItem employees = EmpManager.GetEmployeeById(id.Value);

            if (employees == null)
            {
                return HttpNotFound();
            }
            ViewBag.empDetails = EmpDetails.GetEmpDetails(id.Value);
            ViewBag.HrDetails = EmpDetails.GetAllHrDetails(employees.businessunitId);
            ViewBag.unitHeadDetails = EmpDetails.GetAllUnitHeadDetails(employees.businessunitId);
            var TeamMembers = EmpDetails.GetTeamMembersWithAvatars(employees.businessunitId);
            ViewBag.TeamMembers = TeamMembers;
            return View("TabView",employees);
        }



        public JsonResult GetTeamMember(string userId, string searchString)
        {
            var result = EmpManager.GetUnitMembersBySearch(userId, searchString);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmpAvater(string id)
        {
            var result = EmpEdit.getEmpAvatar(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Salary(string userId, string returnUrl)
        {
            var user = UserManager.FindById(userId);
            PayrollViewModel empToSend = new PayrollViewModel();
            if (user != null)
            {
                var empSalary = payRollRepo.GetByUserId(userId);
                if (empSalary != null)
                {
                    empToSend.Deduction = empSalary.Deduction.Value;
                    empToSend.LeaveAllowance = empSalary.LeaveAllowance.Value;
                    empToSend.Loan = empSalary.Loan.Value;
                    empToSend.Reimbursable = empSalary.Reimbursable.Value;
                    empToSend.Salary = empSalary.Salary.Value;
                    empToSend.Total = empSalary.Total.Value;
                    empToSend.Remarks = empSalary.Remarks;
                }

                ViewBag.empSalary = empSalary;
                ViewBag.UserId = userId;
                ViewBag.returnUrl = returnUrl;
                return PartialView(empToSend);
            }
            //this.AddNotification("", NotificationType.ERROR);

            return PartialView(empToSend);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrUpdateSalary(PayrollViewModel model, string userId, string returnUrl)
        {
            try
            {
                var user = userManager.FindById(userId);
                var employee = EmployeeRepo.GetByUserId(userId);
                if (ModelState.IsValid && user != null && employee != null)
                {
                    EmpEdit.AddORUpdateSalary(userId, model, employee, user, User.Identity.GetUserId());
                    this.AddNotification("Operation successfull", NotificationType.SUCCESS);
                    return Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            this.AddNotification("Something went wrong", NotificationType.ERROR);
            return Redirect(returnUrl);
        }

        public ActionResult UpdateEmpAvater(string userId, string returnUrl)
        {
            var user = userManager.FindById(userId);
            if (user != null)
            {
                ViewBag.UserId = userId;
                ViewBag.returnUrl = returnUrl;
                return View();
            }
            TempData["Error"] = "This route does not exist";
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateEmpAvater(Domain.Entities.File model, string userId, string returnUrl, HttpPostedFileBase File)
        {
            var user = EmpManager.GetUserIdFromEmployeeTable(int.Parse(userId));
            var employee = EmployeeRepo.GetByUserId(user);
            var existingAvatar = FileRepo.GetByUserId(user);
            try
            {
                if (ModelState.IsValid && employee != null && File.ContentLength >0)
                {

                    string fileFullName = Server.MapPath("~/Files/Avatars/");
                    EmpEdit.AddOrUpdateAvater(model, userId, existingAvatar, fileFullName, File, FileRepo);
                    this.AddNotification("Operation successfull", NotificationType.SUCCESS);
                    return Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something went wrong";
                return Redirect(returnUrl);
                throw ex;
            }
            this.AddNotification("Something went wrong", NotificationType.ERROR);
            return Redirect(returnUrl);
        }

        public ActionResult TabView()
        {
            return View();
        }
    }
}
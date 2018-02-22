using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.webUi.Infrastructure;
using resourceEdge.webUi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
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
        EmployeeManager EmpManager;
        EmployeeDetails EmpDetails;
        EmployeeEdit EmpEdit;
        public EmployeeController(IPayroll PRParam, IEmployees EParam, IFiles fParam, IPositions PParam, IJobtitles jParam, ILeaveManagement LParam,IPayroll payParam )
        {
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            payRollRepo = PRParam;
            EmployeeRepo = EParam;
            EmpManager = new EmployeeManager(EParam,fParam, LParam, payParam);
            FileRepo = fParam;
            PositionRepo = PParam;
            jobRepo = jParam;
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
        public ActionResult Edit(string key)
        {
            ViewBag.UserId = key;
            ViewBag.Avartar = EmpEdit.getEmpAvatar(key);
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = EmployeeRepo.GetById(id.Value);

            if (employees == null)
            {
                return HttpNotFound();
            }
            ViewBag.empDetails = EmpDetails.GetEmpDetails(id.Value);
            ViewBag.HrDetails = EmpDetails.GetAllHrDetails(employees.businessunitId);
            ViewBag.unitHeadDetails = EmpDetails.GetAllUnitHeadDetails(employees.businessunitId);
            var TeamMembers = EmpDetails.GetTeamMembersWithAvatars(employees.businessunitId);
            ViewBag.TeamMembers = TeamMembers;
            return View(employees);
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
                return View(empToSend);
            }
            TempData["Error"] = "This route does not exist";
            return RedirectToAction("allEmployee", "HR");
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
                    TempData["Success"] = "Operation successfull";
                    return Redirect(returnUrl);
                    ///I had to return a string here because asp.net mvc does not allow childActions to return a redirect 
                    ///so i had to make use of the Response Object to redirect and just return a string.
                    ///this might not be a best approach, will figure it out later.
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            TempData["Error"] = "Something went wrong";
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
            return RedirectToAction("allEmployee", "HR");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateEmpAvater(Files model, string userId, string returnUrl, HttpPostedFileBase File)
        {
            var user = userManager.FindById(userId);
            var employee = EmployeeRepo.GetByUserId(userId);
            var existingAvatar = FileRepo.GetByUserId(userId);
            try
            {
                if (ModelState.IsValid && employee != null && File.ContentLength >0)
                {

                    string fileFullName = Server.MapPath("~/Files/Avatars/");
                    EmpEdit.AddOrUpdateAvater(model, userId, existingAvatar, fileFullName, File, FileRepo);
                    TempData["Success"] = "Operation successfull";
                    return Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something went wrong";
                return Redirect(returnUrl);
                throw ex;
            }
            TempData["Error"] = "Something went wrong";
            return Redirect(returnUrl);
        }


    }
}
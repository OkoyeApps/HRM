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
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Controllers
{
    public class EmployeeController : Controller
    {

        private ApplicationUserManager userManager;
        private IPayroll payRollRepo;
        IEmployees EmployeeRepo;        
        IFiles FileRepo;
        EmployeeManager EmpManager;
        public EmployeeController(IPayroll PRParam, IEmployees EParam, IFiles fParam)
        {
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            payRollRepo = PRParam;
            EmployeeRepo = EParam;
            EmpManager = new EmployeeManager();
            FileRepo = fParam;
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
        public ActionResult Edit(string userId)
        {
            ViewBag.UserId = userId;
            var aa =  EmpManager.getEmpAvatar(userId);
            ViewBag.Avartar = aa;
            return View();
        }
        
        //[ChildActionOnly]
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
                var employee = EmployeeRepo.GetEmployeeByUserid(userId);
                if (ModelState.IsValid && user != null && employee != null)
                { 
                    EmpManager.AddORUpdateSalary(userId, model,employee, user, User.Identity.GetUserId() );     
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
            var employee = EmployeeRepo.GetEmployeeByUserid(userId);
            var existingAvatar = FileRepo.GetByUserId(userId);
            try
            {
                if (ModelState.IsValid && employee != null)
                {

                    string fileFullName = Server.MapPath("~/Files/Avatars/");
                    EmpManager.AddOrUpdateAvater(model, userId, existingAvatar, fileFullName, File, FileRepo);
                    TempData["Success"] = "Operation successfull";
                    return Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            TempData["Error"] = "Something went wrong";
            return Redirect(returnUrl);
        }

        
    }
}
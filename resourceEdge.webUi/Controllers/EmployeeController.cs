using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.webUi.Infrastructure;
using resourceEdge.webUi.Models;
using System;
using System.Collections.Generic;
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
        EmployeeManager EmpManager;

        public EmployeeController(IPayroll PRParam, IEmployees EParam)
        {
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            payRollRepo = PRParam;
            EmployeeRepo = EParam;
            EmpManager = new EmployeeManager(ControllerContext);
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
            return View();
        }
        
        [ChildActionOnly]
        public ActionResult Salary(string userId, string returnUrl)
        {
            var user = UserManager.FindById(userId);
            if (user != null)
            {
              var empSalary = payRollRepo.GetByUserId(userId);
                var empToSend = new PayrollViewModel()
                {
                    Deduction = empSalary.Deduction,
                    LeaveAllowance = empSalary.LeaveAllowance,
                    Loan = empSalary.Loan,
                    Reimbursable = empSalary.Reimbursable,
                    Salary = empSalary.Salary,
                    Total = empSalary.Total,
                    Remarks = empSalary.Remarks
                };
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
        public string AddOrUpdateSalary(PayrollViewModel model, string userId, string returnUrl)
        {
            try
            {
                var user = userManager.FindById(userId);
                var employee = EmployeeRepo.GetEmployeeByUserid(userId);
                if (ModelState.IsValid && user != null && employee != null)
                { 
                    EmpManager.AddORUpdateSalary(userId, model,employee, user, User.Identity.GetUserId() );     
                    TempData["Success"] = "Operation successfull";
                    Response.Redirect(returnUrl);
                    //return View("Edit", new { userId = userId, returnUrl = returnUrl });
                    return "Finished";
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
            return "Finished";
        }

    }
}
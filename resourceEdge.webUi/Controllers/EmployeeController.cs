using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
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
        //ApplicationDbContext db;
        private IPayroll payRollRepo;
        IEmployees EmployeeRepo;

        public EmployeeController(ApplicationDbContext dbParam, IPayroll PRParam, IEmployees EParam)
        {
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            //db = dbParam;
            payRollRepo = PRParam;
            EmployeeRepo = EParam;
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
        public ActionResult Salary(PayrollViewModel model, string userId, string returnUrl)
        {
            try
            {
                var user = userManager.FindById(userId);
                var employee = EmployeeRepo.GetEmployeeByUserid(userId);
                if (ModelState.IsValid && user != null && employee != null)
                {
                    EmpPayroll payroll = new EmpPayroll();
                    payroll.BusinessUnit = employee.businessunitId.ToString();
                    payroll.Department = employee.departmentId.ToString();
                    payroll.EmpName = employee.FullName;
                    payroll.UserId = user.Id;
                    payroll.EmpStatus = employee.empStatusId;
                    payroll.Deduction = model.Deduction;
                    payroll.LeaveAllowance = model.LeaveAllowance;
                    payroll.Reimbursable = model.Reimbursable;
                    payroll.ResignationDate = employee.dateOfLeaving.Value;
                    payroll.ResumptionDate = employee.dateOfJoining.Value; //Fix this later because it has to be from the employee Table
                    payroll.Loan = model.Loan;
                    payroll.Salary = model.Salary;
                    payroll.Total = model.Total;
                    payroll.CreatedBy = User.Identity.GetUserId();
                    payroll.ModifiedBy = User.Identity.GetUserId();
                    payroll.CreatedDate = DateTime.Now;
                    payroll.ModifiedDate = DateTime.Now;
                    payroll.Remarks = model.Remarks; 
                    payRollRepo.AddORUpdate(userId, payroll);     
                    TempData["Success"] = "Operation successfull";
                    return RedirectToAction("Edit", new { userId = userId, returnUrl = returnUrl });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            TempData["Error"] = "Something went wrong";
            return View(model);
        }

    }
}
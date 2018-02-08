using Microsoft.AspNet.Identity;
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
        ApplicationDbContext db;
        private IPayroll payRollRepo;

        public EmployeeController(ApplicationUserManager Umparam, ApplicationDbContext dbParam, IPayroll PRParam)
        {
            UserManager = Umparam;
            db = dbParam;
            payRollRepo = PRParam;
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
        public ActionResult Edit()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Salary(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Salary(PayrollViewModel model, string returnUrl)
        {
            try
            {
                var employee = userManager.FindById(model.UserId);
                if (ModelState.IsValid && employee != null)
                {
                    Payroll payroll = new Payroll()
                    {
                        BusinessUnit = employee.businessunitId,
                        Department = employee.departmentId,
                        EmpName = employee.employees.FullName,
                        UserId = employee.Id,
                        EmpStatus = employee.employees.empStatusId,
                        Deduction = model.Deduction,
                        LeaveAllowance = model.LeaveAllowance,
                        Reimbursable = model.Reimbursable,
                        ResignationDate = model.ResignationDate,
                        ResumptionDate = model.ResumptionDate,
                        Loan = model.Loan,
                        Salary = model.Salary,
                        Total = model.Total,
                        CreatedBy = User.Identity.GetUserId(),
                        ModifiedBy = User.Identity.GetUserId(),
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        Remarks = model.Remarks
                    };
                    payRollRepo.Insert(payroll);
                    TempData["Success"] = "Operation successfull";
                    return Redirect(returnUrl);
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
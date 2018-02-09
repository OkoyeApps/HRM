using Microsoft.AspNet.Identity;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;
using resourceEdge.webUi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace resourceEdge.webUi.Infrastructure
{

    public interface IEmployee
    {

    }
    public class EmployeeManager
    {
        UnitOfWork unitofWork = new UnitOfWork();
        ControllerContext ctx = new ControllerContext();
        private ILeaveManagement LeaveRepo;
       private IFiles FileRepo;


        public EmployeeManager() {

        }
        public EmployeeManager(IFiles fParam)
        {
            FileRepo = fParam;
        }
        public class EmployeeListItem
        {
            public int empID { get; set; }
            public string userId { get; set; }
            public string empEmail { get; set; }
            public int empRoleId { get; set; }
            public string FullName { get; set; }
            public string reportingManager1 { get; set; }
            public string reportingManager2 { get; set; }
            public string empStatusId { get; set; }
            public int businessunitId { get; set; }
            public int departmentId { get; set; }

        }
        
        public List<Employees> GetAllEmployees()
        {
            return unitofWork.employees.Get().ToList();
        }
        public  List<Employees> GetEmpByBusinessUnit(int id)
        {
            var employeeByUnit = unitofWork.GetDbContext().employees.Where(x => x.businessunitId == id).ToList();
            if (employeeByUnit != null)
            {
                return employeeByUnit;
            }
            return null;
        }
        public List<Employees> GetReportManagerBusinessUnit(int id)
        {
            var context = unitofWork.GetDbContext();
            var ReportManager = context.ReportManagers.Where(x => x.BusinessUnitId == id).FirstOrDefault();
            if (ReportManager != null)
            {
                var employee = context.employees.Where(x => x.businessunitId == ReportManager.BusinessUnitId && x.empRoleId == 2).ToList();
                if (employee != null)
                {
                    return employee;
                }
            }
            return null;
        }
        public Employees GetEmployeeByUserId(string userid)
        {
            var employee = unitofWork.GetDbContext().employees.Where(x => x.userId == userid).SingleOrDefault();
            if (employee != null)
            {
                return employee;
            }
            return null;
        }
        public bool CheckIfEmployeeExistByUserId(string userId)
        {
            var employee = unitofWork.GetDbContext().employees.Any(x => x.userId == userId);
            if (employee == true)
            {
                return true;
            }
            return false;
        }
       public List<Employees> GetEmployeeByDept(int dept)
        {
           return unitofWork.GetDbContext().employees.Where(x => x.departmentId == dept).ToList();
        }
       
        public void UpdateEmployee(Employees employee)
        {
            unitofWork.employees.Update(employee);
            unitofWork.Save();
        }

        public EmployeeLeaves getEmpLeaveByUserId(string userId)
        {
           var employee =  unitofWork.GetDbContext().EmpLeaves.Where(x => x.UserId == userId).SingleOrDefault();
            if (employee != null)
            {
                return employee;
            }
            return null;
        }

        public ReportManagers getReportManagerByUserId(string userid)
        {
            var manager = unitofWork.GetDbContext().ReportManagers.Where(x => x.managerId == userid).SingleOrDefault();
            if (manager != null)
            {
                return manager;
            }
            return null;
        }

        public EmployeeLeaveTypes LeaveTypeById(int id)
        {
            var leave = unitofWork.GetDbContext().LeaveType.Where(x => x.id == id).SingleOrDefault();
            if (leave != null)
            {
                return leave;
            }
            return null;
        }

        public List<EmployeeListItem> GetReportManagrbyUserId(string userId)
        {
            List<EmployeeListItem> managers = new List<EmployeeListItem>();
            var employee = UserManager.getEmployeeIdFromUserTable(userId);
            if (employee != null)
            {
                EmployeeListItem listItem;
                int BuintId = (Int32.Parse(employee.businessunitId));
                var Reportmanager = unitofWork.GetDbContext().ReportManagers.Where(x => x.BusinessUnitId ==BuintId ).ToList();
                if (Reportmanager != null)
                {
                    foreach (var item in Reportmanager)
                    {
                        listItem = new EmployeeListItem();
                        var manager = GetEmployeeByUserId(item.managerId);
                        if (manager != null)
                        {
                            listItem.empID = item.employeeId;
                            listItem.businessunitId = item.BusinessUnitId;
                            listItem.departmentId = item.DepartmentId;
                            listItem.userId = item.managerId;
                            listItem.FullName = manager.FullName;
                            managers.Add(listItem);
                        }
                        
                    }                  
                    return managers;
                }
                
            }
            return null;
        }

        public void AddOrUpdateAvater(Files model, string userId, Files currentAvater, string filefullName, HttpPostedFileBase File, IFiles fileRepo)
        {
            if (currentAvater == null)
            {

                string fileName = Path.GetFileNameWithoutExtension(File.FileName);
                string extenstion = Path.GetExtension(File.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssff") + extenstion;
                model.FilePath = "~/Files/Avatars/" + fileName;
                fileName = Path.Combine(filefullName + fileName);
                File.SaveAs(fileName);
                var file = new Files()
                {
                    FileName = model.FileName,
                    FilePath = model.FilePath,
                    UserId = userId
                };
                fileRepo.Insert(model);
            }
            else
            {
                
                string fileName = Path.GetFileNameWithoutExtension(File.FileName);
                string extenstion = Path.GetExtension(File.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssff") + extenstion;
                model.FilePath = "~/Images/" + fileName;
                fileName = Path.Combine(ctx.Controller.ControllerContext.HttpContext.Server.MapPath("~/Images/") + fileName);
                File.SaveAs(fileName);
                currentAvater.FileName = model.FileName;
                currentAvater.FilePath = model.FilePath;               
                fileRepo.Insert(model);
            }
        }
        
        public string getEmpAvatar(string userId)
        {
            //Update this method so you can check the file type later
           return unitofWork.GetDbContext().Files.Where(x => x.UserId == userId).Select(x => x.FilePath).SingleOrDefault();
        }

        public bool DoesReportManagerExist(string userId, int bUnitId)
        {
            var manager = unitofWork.GetDbContext().ReportManagers.Where(x => x.managerId == userId).ToList();
            if (manager.Count > 1)
            {
                List<int> bUnit = new List<int>();
                int businessunit;
                foreach (var item in manager)
                {
                    businessunit = item.BusinessUnitId;
                    bUnit.Add(businessunit);
                    if (!bUnit.Contains(businessunit))
                    {
                        return false;
                    }
                    return true;
                }

                return true;
            }
            return false;
        }

        public void AddORUpdateSalary(string userId, PayrollViewModel entity,Employees employee, ApplicationUser currentUser, string systemUserId )
        {
           
            
            var CurrentPayRoll = unitofWork.GetDbContext().Payroll.Where(x => x.UserId == userId).FirstOrDefault();
            if (CurrentPayRoll != null)
            {
                CurrentPayRoll.BusinessUnit = entity.BusinessUnit;
                CurrentPayRoll.Deduction = entity.Deduction;
                CurrentPayRoll.LeaveAllowance = entity.LeaveAllowance;
                CurrentPayRoll.Loan = entity.Loan;
                CurrentPayRoll.Reimbursable = entity.Reimbursable;
                CurrentPayRoll.Salary = entity.Salary;
                CurrentPayRoll.Total = entity.Total;
                unitofWork.PayRoll.Update(CurrentPayRoll);
                unitofWork.Save();
            }
            else
            {             
                EmpPayroll payroll = new EmpPayroll();
                payroll.BusinessUnit = employee.businessunitId.ToString();
                payroll.Department = employee.departmentId.ToString();
                payroll.EmpName = employee.FullName;
                payroll.UserId = currentUser.Id;
                payroll.EmpStatus = employee.empStatusId;
                payroll.Deduction = entity.Deduction;
                payroll.LeaveAllowance = entity.LeaveAllowance;
                payroll.Reimbursable = entity.Reimbursable;
                payroll.ResignationDate = employee.dateOfLeaving.Value;
                payroll.ResumptionDate = employee.dateOfJoining.Value;
                payroll.Loan = entity.Loan;
                payroll.Salary = entity.Salary;
                payroll.Total = entity.Total;
                payroll.CreatedBy = systemUserId;
                payroll.ModifiedBy = systemUserId;
                payroll.CreatedDate = DateTime.Now;
                payroll.ModifiedDate = DateTime.Now;
                payroll.Remarks = entity.Remarks;
                unitofWork.PayRoll.Insert(payroll);
                unitofWork.Save();
            }
        }






        //private bool disposed = false;

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            unitofWork.Dispose();
        //        }
        //    }
        //    this.disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose();
        //    // GC.SuppressFinalize(this);
        //}
    }
}
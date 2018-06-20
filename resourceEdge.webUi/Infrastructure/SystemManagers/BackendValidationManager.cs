using Microsoft.AspNet.Identity.EntityFramework;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.webUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace resourceEdge.webUi.Infrastructure
{
    public class BackendValidationManager
    {
        private IEmployees EmpRepo;
        ApplicationDbContext dbContext = new Models.ApplicationDbContext();
        ApplicationUserManager UserManager;
        
        public BackendValidationManager()
        {
            UserManager = new ApplicationUserManager(new UserStore<AppUser>(dbContext));
        }
        public BackendValidationManager(IEmployees eParam)
        {
            EmpRepo = eParam;
        }
        public bool ValidateUser(string EmpId)
        {
            string empId = "Tenece" + EmpId;
            var employee = dbContext.Users.Where(x => x.EmployeeId.Contains(EmpId) || x.EmployeeId == EmpId).FirstOrDefault();
            if (employee != null)
            {
                return true;
            }
            return false;
        }
        public bool validateDates(DateTime dateOfJoining, DateTime dateOfLeaveing)
        {
            if (dateOfJoining > dateOfLeaveing)
            {
                return false;
            }
            else if (dateOfJoining < dateOfLeaveing)
            {
                return true;
            }
            return false;
        }

    }
}
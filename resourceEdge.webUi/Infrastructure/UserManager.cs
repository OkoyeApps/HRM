using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;
using resourceEdge.webUi.Infrastructure.Core;
using resourceEdge.webUi.Models;
using resourceEdge.webUi.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace resourceEdge.webUi.Infrastructure
{
    public static class UserManagement
    {
        private static ApplicationDbContext context = new ApplicationDbContext();
        static UnitOfWork unitOfWork = new UnitOfWork();
        private static ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public static async Task<Tuple<ApplicationUser, string>> CreateUser(
            string email, string empRole, string userstatus, string fname, string lname, string phoneNo,
            string empId, string jobId, string Comments, string createdBy, string modifiedBy, string modeofEntry, DateTime? selectedDate, string candidateReferredBy, bool? isactive
            , string DeptId, string BUnitId)
        {
            try
            {
                var user = new ApplicationUser()
                {

                    UserName = email,
                    Email = email,
                    emprole = empRole,
                    userstatus = userstatus,
                    firstname = fname,
                    lastname = lname,
                    businessunitId = BUnitId,
                    departmentId = DeptId,
                    contactnumber = phoneNo,
                    createdby = createdBy,
                    modifiedby = modifiedBy,
                    createddate = DateTime.Now,
                    modifieddate = DateTime.Now,
                    employeeId = empId,
                    modeofentry = modeofEntry,
                    entrycomments = Comments,
                    selecteddate = selectedDate,
                    candidatereferredby = candidateReferredBy,
                    JobtitleId = int.Parse(jobId),
                    Isactive = isactive,
                };
                //Enable this later
                //IdentityResult validEmail = await userManager.UserValidator.ValidateAsync(user);
                //if (!validEmail.Succeeded)
                //{
                //    return "Not a valid Email Address";
                //}
                //else
                //{
                Generators Generator = new Generators();
                var password = Generator.RandomPassword();
                var result = await userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    return null;
                }
                else
                {
                    var role = context.Roles.Find(empRole.ToString());
                    if (role == null)
                    {
                        return null;
                    }
                    else
                    {
                        var userRole = await userManager.AddToRoleAsync(user.Id.ToString(), role.Name);
                        if (!userRole.Succeeded)
                        {
                            return null;
                        }
                        else
                        {
                            //activate this later
                            //var message = $"Welcome to Tence Your Username is {user.UserName} and Password is {password}. \n Please ensure to keep this details safe.";
                            //NotificationManager manager = new NotificationManager();
                            //await manager.sendEmailNotification("Tenece", "noreply@gmail.com", message, user.Email);
                            return Tuple.Create(user, password);

                        }

                    }
                    // }
                    //}

                    //}

                }

                //Create AddManager, RemoveManager
                //Add departmental head
                //Add businessUnit head
                //

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool checkEmployeeId(string id, string email)
        {
            var user = context.Users.Where(x => x.employeeId == id || x.Email == email).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static ApplicationUser getEmployeeIdFromUserTable(string userid)
        {
            var result = context.Users.Find(userid);
            return result ?? null;
        }

    }
}
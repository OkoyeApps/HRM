using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;
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
    public static class UserManager
    {
        private static ApplicationDbContext context = new ApplicationDbContext();
        static UnitOfWork unitOfWork = new UnitOfWork();
        private static ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

        public static string GeneratePassword()
        {
            return Membership.GeneratePassword(10, 1);
        }

        public static async Task<ApplicationUser> CreateUser(
            string email, string empRole, string userstatus, string fname, string lname, string phoneNo,
            string empId, string jobId, string Comments, int? createdBy, string modifiedBy, string modeofEntry, DateTime? selectedDate, string candidateReferredBy, bool? isactive
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
                    jobtitle_id = int.Parse(jobId),
                    isactive = isactive,
                };
                //Enable this later
                //IdentityResult validEmail = await userManager.UserValidator.ValidateAsync(user);
                //if (!validEmail.Succeeded)
                //{
                //    return "Not a valid Email Address";
                //}
                //else
                //{
                var password = GeneratePassword();
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
                        //var userRole = userManager.AddToRole(user.Id.ToString(), role.Name);
                        if (!userRole.Succeeded)
                        {
                            return null;
                        }
                        else
                        {
                            var message = $"Welcome to Tence Your Username is {user.UserName} and Password is {password}. \n Please ensure to keep this details safe.";
                            // await  userManager.SendEmailAsync(user.Id, "Account Creation", message);
                            NotificationManager manager = new NotificationManager();
                            await manager.sendEmailNotification("Tenece", "noreply@gmail.com", message, user.Email);
                            return user;

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
        public static bool checkEmployeeId(string id)
        {
            var user = context.Users.Any(x => x.employeeId == id);
            if (user == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string getEmployeeFullNameByUserId(string userid)
        {
            if (userid != null)
            {
                return unitOfWork.GetDbContext().employees.Where(X => X.userId == userid).Select(x => x.FullName).SingleOrDefault();

            }
            return null;
        }
        public static ApplicationUser getEmployeeIdFromUserTable(string userid)
        {
            var result = context.Users.Find(userid);
            if (result != null)
            {
                return result;
            }
            return null;
        }

    }
}
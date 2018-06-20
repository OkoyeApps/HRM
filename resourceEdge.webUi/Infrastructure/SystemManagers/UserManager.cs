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
        public static ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<AppUser>(context));

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public static async Task<Tuple<AppUser, string>> CreateUser(
            string email, string empRole, string userstatus, string fname, string lname, string phoneNo,
            string empId, string jobId, string Comments, string createdBy, string modifiedBy, string modeofEntry, DateTime? selectedDate, string candidateReferredBy, bool? isactive
            , string DeptId, string BUnitId, int groupId, int locationId)
        {
            try
            {
                var user = new AppUser()
                {

                    UserName = email,
                    Email = email,
                    EmpRole = empRole,
                    UserStatus = userstatus,
                    FirstName = fname,
                    LastName = lname,
                    UserfullName = fname + "" + lname,
                    BusinessunitId = BUnitId,
                    DepartmentId = DeptId,
                    PhoneNumber = phoneNo,
                    CreatedBy = createdBy,
                    ModifiedBy = modifiedBy,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    EmployeeId = empId,
                    ModeofEntry = modeofEntry,
                    EntryComments = Comments,
                    SelectedDate = selectedDate,
                    Candidatereferredby = candidateReferredBy,
                    JobtitleId = int.Parse(jobId),
                    Isactive = isactive,
                    GroupId = groupId,
                    LocationId = locationId
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



        public static Employee CreateEmployee(EmployeeViewModel employees)
        {
            var UserId = HttpContext.Current.User.Identity.GetUserId();
            Employee realEmployee = new Employee();
            int group = 0;
            var UserFromSession = (SessionModel)HttpContext.Current.Session["_ResourceEdgeTeneceIdentity"];
            if (!HttpContext.Current.User.IsInRole("Super Admin"))
            {
                group = UserFromSession.GroupId.Value;
            }
            else if (HttpContext.Current.User.IsInRole("Super Admin"))
            {
                group = employees.GroupId;
            }
            if (group != 0)
            {
                var unitDetail = unitOfWork.BusinessUnit.GetByID(employees.businessunitId);
                realEmployee.BusinessunitId = employees.businessunitId;
                realEmployee.createdby = UserId;
                realEmployee.dateOfJoining = employees.dateOfJoining;
                realEmployee.dateOfLeaving = employees.dateOfLeaving;
                realEmployee.DepartmentId = employees.departmentId;
                realEmployee.empEmail = employees.empEmail;
                realEmployee.FullName = employees.FirstName + " " + employees.lastName;
                realEmployee.empStatusId = employees.empStatusId;
                realEmployee.isactive = true;
                realEmployee.JobTitleId = employees.jobtitleId;
                realEmployee.modeofEmployement = (int)employees.modeofEmployement;
                realEmployee.modifiedby = UserId;
                realEmployee.officeNumber = employees.officeNumber;
                realEmployee.PositionId = employees.positionId;
                realEmployee.prefixId = (int)employees.prefixId;
                realEmployee.yearsExp = employees.yearsExp;
                realEmployee.LevelId = employees.Level;
                realEmployee.LocationId = unitDetail.LocationId.Value;
                realEmployee.GroupId = group;
                realEmployee.isactive = true;
                var CreatedDate = realEmployee.createddate = DateTime.Now;
                var modifiedDate = realEmployee.modifieddate = DateTime.Now;
                return realEmployee;
            }
            return null;
        }

        public static bool checkEmployeeId(string id)
        {
            var user = context.Users.Where(x => x.EmployeeId == id).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            return false;
        }
        public static bool checkEmail(string email)
        {
            var user = context.Users.Where(x => x.Email == email).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            return false;
        }
        public static AppUser getEmployeeIdFromUserTable(string userid)
        {
            var result = context.Users.Find(userid);
            return result ?? null;
        }

        public static bool AssignLocationHead(string userId, int groupId)
        {
            var location = unitOfWork.Locations.GetByID(groupId);
            if (location != null)
            {
                if (string.IsNullOrEmpty(location.LocationHead1))
                {
                    location.LocationHead1 = userId;
                }
                else if (string.IsNullOrEmpty(location.LocationHead2) && !string.IsNullOrEmpty(location.LocationHead3) && !string.IsNullOrEmpty(location.LocationHead1))
                {
                    location.LocationHead2 = userId;
                }
                else if (string.IsNullOrEmpty(location.LocationHead3) && !string.IsNullOrEmpty(location.LocationHead2) && !string.IsNullOrEmpty(location.LocationHead1))
                {
                    location.LocationHead3 = userId;
                }
                if (string.IsNullOrEmpty(location.LocationHead1) || string.IsNullOrEmpty(location.LocationHead2) || string.IsNullOrEmpty(location.LocationHead3))
                {
                    unitOfWork.Locations.Update(location);
                    unitOfWork.Save();
                    return true;
                }
            }
            return false;
        }

        public static bool IsLocationHeadComplete(int locationId)
        {
            var location = unitOfWork.Locations.Get(filter: x => x.Id == locationId).Any(x => (x.LocationHead1 == null || x.LocationHead2 == null || x.LocationHead3 == null));
            if (location)
            {
                return true;
            }
            return false;
        }

        public static string GetIdentityCode(int groupId)
        {
            var code = unitOfWork.identityCodes.Get(filter: x => x.GroupId == groupId).FirstOrDefault();
            string identityCode = "";
            if (code != null)
            {
                identityCode = code.employee_code;
            }
            return identityCode;
        }
        public static bool? ValidateHeadHRsInGroup(IdentityRole role, int groupId)
        {
            if (role != null)
            {
                Dictionary<string, int> usersAndGroup = new Dictionary<string, int>();
                int counter = 0;
                foreach (var item in role.Users)
                {
                    var userDetail = unitOfWork.employees.Get(filter: x => x.userId == item.UserId).Select(x=>new {GroupId = x.GroupId }).FirstOrDefault();
                    if (userDetail != null)
                    {
                        if (!usersAndGroup.ContainsKey(item.UserId))
                        {
                            usersAndGroup.Add(item.UserId, userDetail.GroupId);
                            if (usersAndGroup.Any(x=>x.Value == userDetail.GroupId) && counter >0)
                            {
                                return false;
                            }
                        }
                    }
                    counter++;
                }

                    return true;
            }
                return null;
        }
    }
}
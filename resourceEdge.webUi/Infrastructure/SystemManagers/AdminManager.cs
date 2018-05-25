using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;
using resourceEdge.webUi.Infrastructure.Core;
using resourceEdge.webUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Infrastructure
{

    public class AdminManager
    {
        Rolemanager roleManager;
        //IEmployees empRepo;
        ApplicationUserManager userManager;
        ApplicationDbContext db;
        UnitOfWork unitOfWork;
        public AdminManager()
        {
            db = new ApplicationDbContext();
            unitOfWork = new UnitOfWork();
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new Rolemanager();
        }

        public async Task<bool> AssignHeadHR(string userId, int GroupId)
        {
            var newHeadHr = unitOfWork.employees.Get(filter: x => x.userId == userId && x.GroupId == GroupId).FirstOrDefault(); IdentityResult result1;
            if (newHeadHr != null)
            {
                var HRrole = roleManager.GetRoleByName("Head HR");
                if (HRrole != null)
                {
                    if (HRrole.Users.Count != 0)
                    {
                        var CurrentHrUserID = HRrole.Users.FirstOrDefault().UserId;
                        try
                        {
                            result1 = await userManager.RemoveFromRoleAsync(CurrentHrUserID, HRrole.Name);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    try
                    {
                        var result = await userManager.AddToRoleAsync(newHeadHr.userId, "Head HR");
                        if (result.Succeeded)
                        {
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }
            return false;
        }

        public bool AddLocationSystemAdmin(SystemAdminViewModel model)
        {
            try
            {
                if (model.Name != null && (model.GroupId > 0 && model.LocationId > 0))
                {
                    var User = new ApplicationUser
                    {
                        Email = model.Email,
                        FirstName = model.Name,
                        LocationId = model.LocationId,
                        GroupId = model.GroupId,
                        CreatedBy = HttpContext.Current.User.Identity.GetUserId(),
                        CreatedDate = DateTime.Now,
                        Isactive = true,
                        UserfullName = model.Name,
                        UserName = model.Email
                    };
                    Generators passwordGenerator = new Generators();
                    var password = passwordGenerator.RandomPassword();
                    var userResult = userManager.Create(User, password);
                    if (userResult.Succeeded)
                    {
                        var roleResult = userManager.AddToRole(User.Id, "System Admin");
                        if (roleResult.Succeeded)
                        {
                            var Admin = new SystemAdmin()
                            {
                                GroupId = model.GroupId,
                                LocationId = model.LocationId,
                                Name = User.UserfullName,
                                UserID = User.Id,
                                Email = model.Email
                            };
                            unitOfWork.SystemAdmin.Insert(Admin);
                            unitOfWork.Save();
                            var groupName = unitOfWork.Groups.GetByID(model.GroupId).GroupName;
                            EmployeeManager mm = new EmployeeManager();
                            mm.AddEmployeeToMailDispatch(User.Email, password, $"noreply", groupName, model.Name);
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<SystemAdminViewModel> GetAllSystemAdmin()
        {
            var admins = unitOfWork.SystemAdmin.Get(includeProperties: "Group,Location").Select(X => new SystemAdminViewModel
            {
                Email = X.Email,
                Group = X.Group.GroupName,
                Location = X.Location.State,
                Name = X.Name,
                UserID = X.UserID,
                 ID = X.ID
            });
            return admins;
        }
        public SystemAdmin GetAdminById(int id)
        {
            var admin = unitOfWork.SystemAdmin.Get(filter: x => x.ID == id, includeProperties: "Group,Location").FirstOrDefault();
            return admin;
        }

    }
}
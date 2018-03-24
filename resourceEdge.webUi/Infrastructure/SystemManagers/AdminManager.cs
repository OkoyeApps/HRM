using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using resourceEdge.Domain.Abstracts;
using resourceEdge.webUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace resourceEdge.webUi.Infrastructure
{

    public class AdminManager
    {
        Rolemanager roleManager = new Rolemanager();
        IEmployees empRepo;
        ApplicationUserManager userManager;
        ApplicationDbContext db = new ApplicationDbContext();
        public AdminManager(IEmployees eParam)
        {
            empRepo = eParam;
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
        }

        public async Task<bool> AssignHeadHR(string userId, int GroupId)
        {
            var newHeadHr = empRepo.GetEmployeeByGroupId(userId, GroupId);
            IdentityResult result1;
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
                        }catch(Exception ex)
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
    }
}
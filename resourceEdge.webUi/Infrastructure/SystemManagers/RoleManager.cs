using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using resourceEdge.Domain.Entities;
using resourceEdge.webUi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace resourceEdge.webUi.Infrastructure
{
    public class Rolemanager : IDisposable
    {
        private AuthManager.Core.AuthDbContext db = new AuthManager.Core.AuthDbContext();
        //private ApplicationDbContext db = new ApplicationDbContext();
        private RoleManager<ApplicationRole> roleManager;
        public Rolemanager()
        {
         roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole,string,ApplicationUserRole>(db));
        }

        public List<ApplicationRole> GetRoles()
        {
           var roles =  roleManager.Roles.ToList();
            return roles;
        }
        public ApplicationRole GetRoleById(string id)
        {
            var role = roleManager.FindById(id);
            return role ?? null;
        }
        public ApplicationRole GetRoleByName(string name)
        {
            var role = roleManager.FindByName(name); 
            return role ?? null;
        }

        public void CreateRoles()
        {
            if (!roleManager.RoleExists("Management"))
            {
                var role = new ApplicationRole();
                role.Name = "Management";
                role.Id = "1";
                roleManager.Create(role);

            }
            if (!roleManager.RoleExists("Manager"))
            {
                var role = new ApplicationRole();
                role.Name = "Manager";
                role.Id = "2";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("HR"))
            {
                var role = new ApplicationRole();
                role.Name = "HR";
                role.Id = "3";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new ApplicationRole();
                role.Name = "Employee";
                role.Id = "4";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Super Admin"))
            {
                var role = new ApplicationRole();
                role.Name = "Super Admin";
                role.Id = "11";
                roleManager.Create(role);

            }
            if (!roleManager.RoleExists("System Admin"))
            {
                var role = new ApplicationRole();
                role.Name = "System Admin";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Head HR"))
            {
                var role = new ApplicationRole();
                role.Name = "Head HR";
                role.Id = "5";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Head Of Unit"))
            {
                var role = new ApplicationRole();
                role.Name = "Head OF Unit";
                role.Id = "6";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Location Head"))
            {
                var role = new ApplicationRole() { Name = "Location Head", Id = "7" };
                roleManager.Create(role);
            }
        }

        

        public void CreateTemporaryRoleForAppraisal()
        {
            if (!roleManager.RoleExists("L1"))
            {
                var role = new ApplicationRole() { Name = "L1", Id = "8" };
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("L2"))
            {
                var role = new ApplicationRole() { Name = "L2", Id = "9" };
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("L3"))
            {
                var role = new ApplicationRole() { Name = "L3", Id = "10" };
                roleManager.Create(role);
            }
        }

        public void RemoveTemporaryRoleForAppraisal()
        {
            if (!roleManager.RoleExists("L1"))
            {
                var role = new ApplicationRole() { Name = "L1", Id = "8" };
                var role2 = new ApplicationRole() { Name = "L2", Id = "9" };
                var role3 = new ApplicationRole() { Name = "L3", Id = "10" };
                roleManager.Delete(role);
                roleManager.Delete(role2);
                roleManager.Delete(role3);
            }
            Dispose();
        }

        public void Dispose()
        {
            ((IDisposable)db).Dispose();
        }

    }
}
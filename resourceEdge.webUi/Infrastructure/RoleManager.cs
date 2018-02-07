using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using resourceEdge.webUi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace resourceEdge.webUi.Infrastructure
{
    public class Rolemanager
    {
       private ApplicationDbContext db = new ApplicationDbContext();
        private RoleManager<IdentityRole> roleManager;
        public Rolemanager()
        {
         roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
        }

        public List<IdentityRole> GetRoles()
        {
           var roles =  roleManager.Roles.ToList();
            return roles;
        }

        public void CreateRoles()
        {
            if (!roleManager.RoleExists("Management"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Management";
                role.Id = "1";
                roleManager.Create(role);

            }
            if (!roleManager.RoleExists("Manager"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Manager";
                role.Id = "2";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("HR"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "HR";
                role.Id = "3";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Employee";
                role.Id = "4";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("System Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "System Admin";
                roleManager.Create(role);
            }
        }
        //private bool disposed = false;

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            db.Dispose();
        //        }
        //    }
        //    this.disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose();
        //    //GC.SuppressFinalize(this);
        //}
    }
}
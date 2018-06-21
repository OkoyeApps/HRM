using AuthManager.Core;
using Microsoft.AspNet.Identity.EntityFramework;
using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceEdge.Logic.Auth_Providers
{
    public class AuthRoleManagerProvider<T> where T : DbContext
    {
        private T DbContext { get; set; }
        public AuthRoleManagerProvider(T Context)
        {
            DbContext = Context;
        }
        public AuthManagerRoleManager GetUserManager()
        {
            AuthManagerRoleManager RoleManager = new AuthManagerRoleManager(new RoleStore<ApplicationRole,string,ApplicationUserRole>(DbContext));
            return RoleManager;
        }
    }
}

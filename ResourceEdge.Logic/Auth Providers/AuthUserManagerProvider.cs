using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthManager.Core;
using Microsoft.AspNet.Identity.EntityFramework;
using resourceEdge.Domain.Entities;

namespace ResourceEdge.Logic.Auth_Providers
{
    public class AuthUserManagerProvider
    {
        private DbContext DbContext { get; set; }
        public AuthUserManagerProvider(DbContext Context)
        {
            DbContext = Context;
        }
        public  AuthUserManager GetUserManager()
        {   
            AuthUserManager UserManager = new AuthUserManager(new UserStore /*ApplicationUserStore(DbContext)*/<AppUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim > (DbContext));
            return UserManager;
        }
    }
}

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using resourceEdge.Domain.Entities;
using resourceEdge.webUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace resourceEdge.webUi.Security
{
    public class EdgeUserManager : UserManager<Users>
    {
        public EdgeUserManager(IUserStore<Users> store) : base(store)
        {

        }
        public static EdgeUserManager Create(IdentityFactoryOptions<EdgeUserManager> options,IOwinContext context)
        {
            EdgeUserDbContext db = context.Get<EdgeUserDbContext>();
            EdgeUserManager manager = new EdgeUserManager(new UserStore<Users>(db));

            manager.PasswordValidator = new CustomValidators.CustomPasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = true
            };

            manager.UserValidator = new CustomValidators.CustomUserValidator(manager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };


            return manager;
        }
    }
}
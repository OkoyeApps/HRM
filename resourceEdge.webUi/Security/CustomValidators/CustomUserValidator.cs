using Microsoft.AspNet.Identity;
using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using resourceEdge.webUi.Models;

namespace resourceEdge.webUi.Security.CustomValidators
{
    public class CustomUserValidator : UserValidator<AppUser>
    {
        public CustomUserValidator(ApplicationUserManager manager) : base(manager)
        {
        }

        public override async Task<IdentityResult> ValidateAsync(AppUser user)
        {
            IdentityResult result = await base.ValidateAsync(user);
            if (!user.Email.ToLower().EndsWith("@tenece.com"))
            {
                var errors = result.Errors.ToList();
                errors.Add("Only tenece.com email addresses are allowed");
                result = new IdentityResult(errors);
            }
            return result;
        }

        public IdentityResult Validate(AppUser user)
        {
            IdentityResult result = Validate(user);
            if (!user.Email.ToLower().EndsWith("@tenece.com"))
            {
                var errors = result.Errors.ToList();
                errors.Add("Only tenece emails are allowed");
                result = new IdentityResult(errors);
            }
            return result;
        }
        
    }
}
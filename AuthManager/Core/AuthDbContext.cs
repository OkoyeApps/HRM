using Microsoft.AspNet.Identity.EntityFramework;
using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthManager.Core
{
    public class AuthDbContext : IdentityDbContext<AppUser>
    {
        public AuthDbContext()
            : base("DefaultConnection2", throwIfV1Schema: false)
        {

        }
        public static AuthDbContext Create()
        {
            return new AuthDbContext();
        }
    }
}

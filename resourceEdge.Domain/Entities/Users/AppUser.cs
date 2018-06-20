using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string EmpRole { get; set; }
        public string UserStatus { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserfullName { get; set; }
        public int? GroupId { get; set; }
        public int? LocationId { get; set; }

        public string BusinessunitId { get; set; }
        public string DepartmentId { get; set; }

        public string EmployeeId { get; set; }
        public string ModeofEntry { get; set; }
        public string EntryComments { get; set; }
        public DateTime? SelectedDate { get; set; }
        public string Candidatereferredby { get; set; }
        public int JobtitleId { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> Isactive { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}

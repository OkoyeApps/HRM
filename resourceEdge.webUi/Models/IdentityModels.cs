using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using resourceEdge.Domain.Entities;

namespace resourceEdge.webUi.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
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

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
         
        }
        public static ApplicationDbContext Create()
        {            
            return new ApplicationDbContext();
        }
        
    }
}
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
        public string emprole { get; set; }
        public string userstatus { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string userfullname
        {
            get
            {
                return this.firstname + this.lastname;
            }
        }
        public string contactnumber { get; set; }
        public string businessunitId { get; set; }
        public string departmentId { get; set; }
        public string createdby { get; set; }
        public string modifiedby { get; set; }
        public Nullable<System.DateTime> createddate { get; set; }
        public Nullable<System.DateTime> modifieddate { get; set; }
        public string employeeId { get; set; }
        public string modeofentry { get; set; }
        public string other_modeofentry { get; set; }
        public string entrycomments { get; set; }
        public Nullable<System.DateTime> selecteddate { get; set; }
        public string candidatereferredby { get; set; }
        public Nullable<int> company_id { get; set; }
        public int JobtitleId { get; set; }
        public Nullable<bool> Isactive { get; set; }
        public  Employees employees { get; set; }
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
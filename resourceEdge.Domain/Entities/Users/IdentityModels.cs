using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Entities
{

    // You will not likely need to customize there, but it is necessary/easier to create our own 
    // project-specific implementations, so here they are:
    public class ApplicationUserLogin : IdentityUserLogin<string> {
        [Key]
        [Column(Order = 0)]
        public string LoginProvider { get; set; }

        [Key]
        [Column(Order = 1)]
        public string ProviderKey { get; set; }

        [Key]
        [Column(Order = 2)]
        public string UserId { get; set; }
        public virtual AppUser AspNetUser { get; set; }
    }
    public class ApplicationUserClaim : IdentityUserClaim<string> {
        public virtual AppUser AspNetUser { get; set; }
    }
    public class ApplicationUserRole : IdentityUserRole<string> {
        public virtual AppUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }

    // Must be expressed in terms of our custom Role and other types:
    //public class AppUser : IdentityUser<string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    //{
    //    public AppUser()
    //    {
    //        this.Id = Guid.NewGuid().ToString();

    //        // Add any custom User properties/code here
    //    }


    //    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
    //    {
    //        var userIdentity = await manager
    //            .CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
    //        return userIdentity;
    //    }
    //}


    // Must be expressed in terms of our custom UserRole:
    public class ApplicationRole : IdentityRole<string, ApplicationUserRole>
    {
        public ApplicationRole()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        public ApplicationRole(string name)
            : this()
        {
            this.Name = name;
            this.AspNetUsers = new HashSet<AppUser>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppUser> AspNetUsers { get; set; }


        // Add any custom Role properties/code here
    }


    // Must be expressed in terms of our custom types:
    //public class EdgeDbContext2DbContext : IdentityDbContext<AppUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    //{
    //    public EdgeDbContext2DbContext()
    //        : base("DefaultConnection")
    //    {
    //    }

    //    static EdgeDbContext2DbContext()
    //    {
    //        //Database.SetInitializer<EdgeDbContext>(new ApplicationDbInitializer());
    //    }

    //    public static EdgeDbContext2DbContext Create()
    //    {
    //        return new EdgeDbContext2DbContext();
    //    }

    //    // Add additional items here as needed
    //}

    // Most likely won't need to customize these either, but they were needed because we implemented
    // custom versions of all the other types:
    public class ApplicationUserStore : UserStore<AppUser, ApplicationRole, string,
        ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IUserStore<AppUser, string>,
        IDisposable
    {
        public ApplicationUserStore() : this(new EdgeDbContext())
        {
            base.DisposeContext = true;
        }

        public ApplicationUserStore(DbContext context)
            : base(context)
        {
        }
    }


    public class ApplicationRoleStore : RoleStore<ApplicationRole, string, ApplicationUserRole>,
    IQueryableRoleStore<ApplicationRole, string>,
    IRoleStore<ApplicationRole, string>, IDisposable
    {
        public ApplicationRoleStore()
            : base(new IdentityDbContext())
        {
            base.DisposeContext = true;
        }

        public ApplicationRoleStore(DbContext context)
            : base(context)
        {
        }
    }

}

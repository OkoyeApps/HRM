using Microsoft.AspNet.Identity.EntityFramework;
using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AuthManager.Core
{
    public class AuthDbContext : IdentityDbContext<AppUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public AuthDbContext()
            : base("DefaultConnection2")
        {

        }
        public static AuthDbContext Create()
        {
            return new AuthDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Configuration.LazyLoadingEnabled = true;
            modelBuilder.Conventions.Remove(new PluralizingTableNameConvention());
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();


            //modelBuilder.Entity<AppUser>().ToTable("AspNetUser");
            //modelBuilder.Entity<ApplicationRole>().ToTable("AspNetRole");
            //modelBuilder.Entity<ApplicationUserRole>().HasKey(x =>new { x.UserId, x.RoleId }).ToTable("AspNetUserRole");
            //modelBuilder.Entity<ApplicationUserClaim>().ToTable("AspNetUserClaim");
            //modelBuilder.Entity<ApplicationUserLogin>().HasKey(x =>new { x.LoginProvider, x.ProviderKey, x.UserId }).ToTable("AspNetUserLogin");


            modelBuilder.Entity<AppUser>()
        .HasMany(e => e.AspNetUserClaims)
        .WithRequired(e => e.AspNetUser)
        .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AppUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<ApplicationUserLogin>().HasKey(x => new { x.LoginProvider, x.ProviderKey, x.UserId });
            modelBuilder.Entity<ApplicationUserRole>().HasKey(x => new { x.RoleId, x.UserId });
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AppUser>().ToTable("AspNetUser");

            modelBuilder.Entity<ApplicationRole>().ToTable("AspNetRole");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("AspNetUserRole");
            modelBuilder.Entity<ApplicationUserClaim>().ToTable("AspNetUserClaim");
            modelBuilder.Entity<ApplicationUserLogin>().ToTable("AspNetUserLogin");
        }
    }
}

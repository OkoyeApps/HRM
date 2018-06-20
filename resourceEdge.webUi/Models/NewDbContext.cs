using Microsoft.AspNet.Identity.EntityFramework;
using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace resourceEdge.webUi.Models
{
    public class NewDbContext : IdentityDbContext<AppUser>
    {
        public NewDbContext(): base("DefaultConnection3", throwIfV1Schema: false)
        {

        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Configuration.LazyLoadingEnabled = true;
            modelBuilder.Conventions.Remove(new PluralizingTableNameConvention());
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //base.OnModelCreating(modelBuilder);
        }
    }
}
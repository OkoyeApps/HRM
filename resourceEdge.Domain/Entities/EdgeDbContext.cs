using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace resourceEdge.Domain.Entities
{
    public partial class EdgeDbContext : DbContext
    {
        public EdgeDbContext()
            : base("name=ResourceDbContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Configuration.LazyLoadingEnabled = false;

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Employees>()
                .HasRequired(x => x.Departments)
                .WithMany()
                .HasForeignKey(x => x.departmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Departments>()
                .HasRequired(x => x.BusinessUnits)
                .WithMany()
                .HasForeignKey(x => x.BunitId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
            // throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<Employees> employees { get; set; }
        public virtual DbSet<IdentityCodes> identityCodes { get; set; }
        public virtual DbSet<BusinessUnits> businessunits { get; set; }
        public virtual DbSet<Departments> departments { get; set; }
        public virtual DbSet<Jobtitles> jobtitles { get; set; }
        public virtual DbSet<Positions> positions { get; set; }
        public virtual DbSet<Prefixes> prefixes { get; set; }
        public virtual DbSet<EmploymentStatus> employmentstatus { get; set; }
        public virtual DbSet<products> products { get; set; }
        public virtual DbSet<ReportManagers> ReportManagers { get; set; }
        public virtual DbSet<EmpHolidays> EmpHolidays { get; set; }
        public virtual DbSet<EmployeeLeaves> EmpLeaves { get; set; }
        public virtual DbSet<LeaveManagement> LeaveManagement { get; set; }
        public virtual DbSet<LeaveManagementSummary> leaveManagementSummary { get; set; }
        public virtual DbSet<LeaveRequest> LeaveRequest { get; set; }
        public virtual DbSet<MonthList> MonthList { get; set; }
        public virtual DbSet<Months> Months { get; set; }
        public virtual DbSet<WeekDays> WeekDays { get; set; }
        public virtual DbSet<Weeks> Weeks { get; set; }
        public virtual DbSet<EmployeeLeaveTypes> LeaveType { get; set; }
        public DbSet<Requisition> Requisition { get; set; }
        public DbSet<EmpPayroll> Payroll { get; set; }
        public DbSet<Files> Files { get; set; }
        public DbSet<Logins> Logins { get; set; }
        public DbSet<Levels> Levels { get; set; }
        public DbSet<Careers> Careers { get; set; }
        public DbSet<CareerPath> CareerPath { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<ActivityLogs> ActivityLog { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<AppraisalMode> AppraisalMode { get; set; }
        public DbSet<AppraisalPeriods> AppraisalPeriods { get; set; }
        public DbSet<AppraisalRating> ApprasialRatings { get; set; }
        public DbSet<AppraisalStatus> AppraisalStatus { get; set; }
        public DbSet<Ratings> Ratings { get; set; }
        public DbSet<AppraisalConfiguration> AppraisalConfiguration { get;set;}
        public DbSet<AppraisalInitialization> AppraisalInitialization { get; set; }
        public DbSet<Parameters> Parameter { get; set; }
    }
    
}


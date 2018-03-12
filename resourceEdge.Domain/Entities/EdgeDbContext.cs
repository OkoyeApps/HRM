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
            //modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
                Configuration.LazyLoadingEnabled = true;
            modelBuilder.Conventions.Remove(new PluralizingTableNameConvention());
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //modelBuilder.Entity<Employees>()
            //    .HasRequired(x => x.Departments)
            //    .WithMany()
            //    .HasForeignKey(x => x.departmentId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Departments>()
            //    .HasRequired(x => x.BusinessUnits)
            //    .WithMany()
            //    .HasForeignKey(x => x.BunitId)
            //    .WillCascadeOnDelete(false);

            //base.OnModelCreating(modelBuilder);
            // throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<Employees> Employee { get; set; }
        public virtual DbSet<IdentityCodes> IdentityCode { get; set; }
        public virtual DbSet<BusinessUnits> Businessunit { get; set; }
        public virtual DbSet<Departments> departments { get; set; }
        public virtual DbSet<Jobtitles> Jobtitle { get; set; }
        public virtual DbSet<Positions> Position { get; set; }
        public virtual DbSet<Prefixes> Prefix { get; set; }
        public virtual DbSet<EmploymentStatus> EmploymentStatus { get; set; }
        public virtual DbSet<products> products { get; set; }
        public virtual DbSet<ReportManagers> ReportManager { get; set; }
        public virtual DbSet<EmpHolidays> EmpHoliday { get; set; }
        public virtual DbSet<EmployeeLeaves> EmployeeLeave { get; set; }
        public virtual DbSet<LeaveManagement> LeaveManagement { get; set; }
        public virtual DbSet<LeaveManagementSummary> LeaveManagementSummary { get; set; }
        public virtual DbSet<LeaveRequest> LeaveRequest { get; set; }
        public virtual DbSet<MonthList> MonthList { get; set; }
        public virtual DbSet<Months> Month { get; set; }
        public virtual DbSet<WeekDays> WeekDay { get; set; }
        public virtual DbSet<Weeks> Week { get; set; }
        public virtual DbSet<EmployeeLeaveTypes> LeaveType { get; set; }
        public DbSet<Requisition> Requisition { get; set; }
        public DbSet<EmpPayroll> Payroll { get; set; }
        public DbSet<Files> File { get; set; }
        public DbSet<Logins> Login { get; set; }
        public DbSet<Levels> Level { get; set; }
        public DbSet<Careers> Career { get; set; }
        public DbSet<CareerPath> CareerPath { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<ActivityLogs> ActivityLog { get; set; }
        public DbSet<Groups> Group { get; set; }
        public DbSet<AppraisalMode> AppraisalMode { get; set; }
        public DbSet<AppraisalPeriods> AppraisalPeriod { get; set; }
        public DbSet<AppraisalRating> ApprasialRating { get; set; }
        public DbSet<AppraisalStatus> AppraisalStatus { get; set; }
        public DbSet<Ratings> Rating { get; set; }
        public DbSet<AppraisalConfiguration> AppraisalConfiguration { get;set;}
        public DbSet<AppraisalInitialization> AppraisalInitialization { get; set; }
        public DbSet<Parameters> Parameter { get; set; }
        public DbSet<SubscribedAppraisal> SubscribedAppraisal { get; set; }
        public DbSet<MailDispatcher> MailDispatcher { get; set; }
        public DbSet<Questions> Question { get; set; }

        public System.Data.Entity.DbSet<resourceEdge.Domain.Entities.QuestionViewModel> QuestionViewModels { get; set; }
    }
    
}


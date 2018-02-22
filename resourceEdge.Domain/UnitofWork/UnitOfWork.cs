using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Security.Principal;

namespace resourceEdge.Domain.UnitofWork
{
    public class UnitOfWork : IDisposable, IDbContext
    {

        private EdgeDbContext myContext;
        private GenericRepository<BusinessUnits> businessRepo;
        private GenericRepository<Departments> departmentRepo;
        private GenericRepository<Employees> employeesRepo;
        private GenericRepository<EmploymentStatus> employementStatusRepo;
        private GenericRepository<IdentityCodes> identitycodesRepo;
        private GenericRepository<Jobtitles> jobTitleRepo;
        private GenericRepository<Positions> positionRepo;
        private GenericRepository<Prefixes> prefixRepo;
        private GenericRepository<products> productRepo;
        private GenericRepository<LeaveManagement> LeaveManagementRepo;
        private GenericRepository<ReportManagers> ReportManagerRepo;
        private GenericRepository<EmployeeLeaveTypes> LeaveTypeRepo;
        private GenericRepository<EmployeeLeaves> EmpLeaveRepo;
        private GenericRepository<LeaveRequest> leaveRequestRepo;
        private GenericRepository<Requisition> requistionRepo;
        private GenericRepository<EmpPayroll> PayrollRepo;
        public GenericRepository<Files> FilesRepo;
        public GenericRepository<Logins> LoginRepo;
        public GenericRepository<Location> LocationRepo;
        public GenericRepository<Levels> levelRepo;
        public GenericRepository<Careers> careerRepo;
        public GenericRepository<CareerPath> careerPathRepo;
        public GenericRepository<ActivityLogs> activityLogRepo;
        public GenericRepository<Groups> GroupRepo;
        public GenericRepository<EmployeeRating> EmpRatingRepo;
        public GenericRepository<AppraisalRating> RatingRepo;
        public GenericRepository<Questions> QuestionRepo;
        public GenericRepository<Skills> SkillRepo;
        public GenericRepository<Parameters> parameterRepo;
        public GenericRepository<AppraisalMode> AppraisalModeRepo;
        public GenericRepository<AppraisalPeriods> AppraisalPeriodRepo;
        public GenericRepository<AppraisalRating> AppraisalRatinRepo;
        public GenericRepository<AppraisalStatus> AppraisalStatusRepo;
        public EdgeDbContext Context
        {
            get
            {
                if (myContext == null)
                {
                    return GetDbContext();
                }
                else
                {
                    return myContext;
                }
            }
        }
        public EdgeDbContext GetDbContext()
        {
            myContext = new EdgeDbContext();
            return myContext;
        }

        public GenericRepository<BusinessUnits> BusinessUnit
        {
            get { return this.businessRepo ?? new GenericRepository<BusinessUnits>(Context); }
        }
        public GenericRepository<Departments> Department
        {
            get { return this.departmentRepo ?? new GenericRepository<Departments>(Context); }
        }
        public GenericRepository<Employees> employees
        {
            get { return this.employeesRepo ?? new GenericRepository<Employees>(Context); }
        }

        public GenericRepository<EmploymentStatus> employmentStatus
        {
            get { return this.employementStatusRepo ?? new GenericRepository<EmploymentStatus>(Context); }
        }
        public GenericRepository<IdentityCodes> identityCodes
        {
            get { return this.identitycodesRepo ?? new GenericRepository<IdentityCodes>(Context); }
        }      
        public GenericRepository<Jobtitles> jobTitles
        {
            get { return this.jobTitleRepo ?? new GenericRepository<Jobtitles>(Context); }
        }
        public GenericRepository<Positions> positions
        {
            get { return this.positionRepo ?? new GenericRepository<Positions>(Context); }
        }
        public GenericRepository<Prefixes> prefix
        {
            get { return this.prefixRepo ?? new GenericRepository<Prefixes>(Context); }
        }
        public GenericRepository<LeaveManagement> LeaveManagement
        {
            get { return this.LeaveManagementRepo ?? new GenericRepository<LeaveManagement>(Context); }
        }
        public GenericRepository<products> productRepository
        {
            get { return this.productRepo ?? new GenericRepository<products>(Context); }
        }

        public GenericRepository<ReportManagers> ReportManager
        {
            get { return this.ReportManagerRepo ?? new GenericRepository<ReportManagers>(Context); }
        }

        public GenericRepository<EmployeeLeaveTypes> LeaveType
        {
            get { return this.LeaveTypeRepo ?? new GenericRepository<EmployeeLeaveTypes>(Context); }
        }
        public GenericRepository<EmployeeLeaves> EmployeeLeave
        {
            get { return this.EmpLeaveRepo ?? new GenericRepository<EmployeeLeaves>(Context); }
        }
        public GenericRepository<LeaveRequest> LRequest
        {
            get { return this.leaveRequestRepo ?? new GenericRepository<LeaveRequest>(Context); }
        }

        public GenericRepository<Requisition> Requisition
        {
            get { return this.requistionRepo ?? new GenericRepository<Requisition>(Context); }
        }

        public GenericRepository<EmpPayroll> PayRoll
        {
            get { return PayrollRepo ?? new GenericRepository<EmpPayroll>(Context); }
        }

        public GenericRepository<Files> Files
        {
            get { return FilesRepo ?? new GenericRepository<Entities.Files>(Context); }
        }
        public GenericRepository<Logins> Logins
        {
            get { return this.LoginRepo ?? new GenericRepository<Entities.Logins>(Context); }
        }
        public GenericRepository<Location> Locations
        {
            get { return this.LocationRepo ?? new GenericRepository<Location>(Context); }
        }
        public GenericRepository<Levels> Levels
        {
            get { return this.levelRepo ?? new GenericRepository<Entities.Levels>(Context); }
        }
        public GenericRepository<Careers> Careers
        {
            get { return this.careerRepo ?? new GenericRepository<Entities.Careers>(Context); }
        }
        public GenericRepository<CareerPath> CareerPath
        {
            get { return this.careerPathRepo ?? new GenericRepository<Entities.CareerPath>(Context); }
        }
        public GenericRepository<ActivityLogs> ActivityLogs
        {
            get { return activityLogRepo ?? new GenericRepository<Entities.ActivityLogs>(Context); }
        }
        public GenericRepository<Groups> Groups
        {
            get { return GroupRepo ?? new GenericRepository<Entities.Groups>(Context); }
        }
        public GenericRepository<Questions> Questions
        {
            get { return this.QuestionRepo ?? new GenericRepository<Entities.Questions>(Context); }
        }
        public GenericRepository<Skills> Skills
        {
            get { return this.SkillRepo ?? new GenericRepository<Entities.Skills>(Context); }
        }
        public GenericRepository<EmployeeRating> EmployeeRatings
        {
            get { return this.EmpRatingRepo ?? new GenericRepository<EmployeeRating>(Context); }
        }
        public GenericRepository<AppraisalRating> Ratings
        {
            get { return this.RatingRepo ?? new GenericRepository<AppraisalRating>(Context); }
        }
        public GenericRepository<Parameters> Parameters
        {
            get { return this.parameterRepo ?? new GenericRepository<Entities.Parameters>(Context); }
        }
        public GenericRepository<AppraisalMode> AppraisalMode
        {
            get { return this.AppraisalModeRepo ?? new GenericRepository<Entities.AppraisalMode>(Context); }
        }
        public GenericRepository<AppraisalPeriods> AppraisalPeriod
        {
            get { return this.AppraisalPeriodRepo ?? new GenericRepository<AppraisalPeriods>(Context); }
        }
        public GenericRepository<AppraisalRating> AppraislRating
        {
            get { return this.AppraisalRatinRepo ?? new GenericRepository<AppraisalRating>(Context); }
        }
        public GenericRepository<AppraisalStatus> AppraisalStatus
        {
            get { return this.AppraisalStatusRepo ?? new GenericRepository<Entities.AppraisalStatus>(Context); }
        }
        public void Save()
        {
            myContext.SaveChanges();
           // Dispose();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    myContext.Dispose();
                }
                if (Context != null)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

     
    }
}

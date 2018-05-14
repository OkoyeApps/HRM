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
        private GenericRepository<BusinessUnit> BusinessRepo;
        private GenericRepository<Departments> departmentRepo;
        private GenericRepository<Employee> employeesRepo;
        private GenericRepository<EmploymentStatus> employementStatusRepo;
        private GenericRepository<IdentityCode> identitycodesRepo;
        private GenericRepository<Jobtitle> jobTitleRepo;
        private GenericRepository<Position> positionRepo;
        private GenericRepository<Prefix> prefixRepo;
        private GenericRepository<products> productRepo;
        private GenericRepository<LeaveManagement> LeaveManagementRepo;
        private GenericRepository<ReportManager> ReportManagerRepo;
        private GenericRepository<EmployeeLeaveType> LeaveTypeRepo;
        private GenericRepository<EmployeeLeave> EmpLeaveRepo;
        private GenericRepository<LeaveRequest> leaveRequestRepo;
        private GenericRepository<Requisition> requistionRepo;
        private GenericRepository<EmpPayroll> PayrollRepo;
        private GenericRepository<File> FilesRepo;
        private GenericRepository<Login> LoginRepo;
        private GenericRepository<Location> LocationRepo;
        private GenericRepository<Level> levelRepo;
        private GenericRepository<Career> careerRepo;
        private GenericRepository<CareerPath> careerPathRepo;
        private GenericRepository<ActivityLog> activityLogRepo;
        private GenericRepository<Group> GroupRepo;
        private GenericRepository<EmployeeRating> EmpRatingRepo;
        private GenericRepository<Ratings> RatingRepo;
        private GenericRepository<Question> QuestionRepo;
        private GenericRepository<Skills> SkillRepo;
        private GenericRepository<Parameters> parameterRepo;
        private GenericRepository<AppraisalMode> AppraisalModeRepo;
        private GenericRepository<AppraisalPeriod> AppraisalPeriodRepo;
        private GenericRepository<AppraisalRating> AppraisalRatinRepo;
        private GenericRepository<AppraisalStatus> AppraisalStatusRepo;
        private GenericRepository<AppraisalInitialization> AppraisalInitilizationRepo;
        private GenericRepository<AppraisalConfiguration> AppraisalConfigurationRepo;
        private GenericRepository<SubscribedAppraisal> SubscibedAppraisalRepo;
        private GenericRepository<MailDispatcher> MailDispacthRepo;
        private GenericRepository<EmployeeDetailDispatcher> EMpDetailDispatchRepo;
        private GenericRepository<Menu> MenuRepository;
        private GenericRepository<AppraisalQuestion> AppraisalQuestionRepo;
        private GenericRepository<AppraisalResult> AppraisalResultRepo;
        private GenericRepository<Candidate> CandidateRepo;
        private GenericRepository<CandidateWorkDetail> CandidateWorkRepo;
        private GenericRepository<CandidateInterview> CandidateInterviewRepo;
        private GenericRepository<Interview> InterviewRepo;
        private GenericRepository<CandidateStatus> candidateStatusRepo;
        private GenericRepository<InterviewStatus> interviewStatusRepo;
        private GenericRepository<InterviewType> interviewTypeRepo;
        private GenericRepository<Asset> assetRepo;
        private GenericRepository<AssetCategory> assetCategory;
        private GenericRepository<RequestAsset> requestassetRepo;
        private GenericRepository<GeneralQuestion> generalQuestionRepo;
        private EdgeDbContext Context
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

        public GenericRepository<BusinessUnit> BusinessUnit
        {
            get { return this.BusinessRepo ?? new GenericRepository<BusinessUnit>(Context); }
        }
        public GenericRepository<Departments> Department
        {
            get { return this.departmentRepo ?? new GenericRepository<Departments>(Context); }
        }
        public GenericRepository<Employee> employees
        {
            get { return this.employeesRepo ?? new GenericRepository<Employee>(Context); }
        }

        public GenericRepository<EmploymentStatus> employmentStatus
        {
            get { return this.employementStatusRepo ?? new GenericRepository<EmploymentStatus>(Context); }
        }
        public GenericRepository<IdentityCode> identityCodes
        {
            get { return this.identitycodesRepo ?? new GenericRepository<IdentityCode>(Context); }
        }      
        public GenericRepository<Jobtitle> jobTitles
        {
            get { return this.jobTitleRepo ?? new GenericRepository<Jobtitle>(Context); }
        }
        public GenericRepository<Position> positions
        {
            get { return this.positionRepo ?? new GenericRepository<Position>(Context); }
        }
        public GenericRepository<Prefix> prefix
        {
            get { return this.prefixRepo ?? new GenericRepository<Prefix>(Context); }
        }
        public GenericRepository<LeaveManagement> LeaveManagement
        {
            get { return this.LeaveManagementRepo ?? new GenericRepository<LeaveManagement>(Context); }
        }
        public GenericRepository<products> productRepository
        {
            get { return this.productRepo ?? new GenericRepository<products>(Context); }
        }

        public GenericRepository<ReportManager> ReportManager
        {
            get { return this.ReportManagerRepo ?? new GenericRepository<ReportManager>(Context); }
        }

        public GenericRepository<EmployeeLeaveType> LeaveType
        {
            get { return this.LeaveTypeRepo ?? new GenericRepository<EmployeeLeaveType>(Context); }
        }
        public GenericRepository<EmployeeLeave> EmployeeLeave
        {
            get { return this.EmpLeaveRepo ?? new GenericRepository<EmployeeLeave>(Context); }
        }
        public GenericRepository<LeaveRequest> LRequest
        {
            get { return leaveRequestRepo ?? new GenericRepository<LeaveRequest>(Context); }
        }

        public GenericRepository<Requisition> Requisition
        {
            get { return this.requistionRepo ?? new GenericRepository<Requisition>(Context); }
        }

        public GenericRepository<EmpPayroll> PayRoll
        {
            get { return PayrollRepo ?? new GenericRepository<EmpPayroll>(Context); }
        }

        public GenericRepository<File> Files
        {
            get { return FilesRepo ?? new GenericRepository<Entities.File>(Context); }
        }
        public GenericRepository<Login> Logins
        {
            get { return this.LoginRepo ?? new GenericRepository<Entities.Login>(Context); }
        }
        public GenericRepository<Location> Locations
        {
            get { return this.LocationRepo ?? new GenericRepository<Location>(Context); }
        }
        public GenericRepository<Level> Levels
        {
            get { return this.levelRepo ?? new GenericRepository<Entities.Level>(Context); }
        }
        public GenericRepository<Career> Careers
        {
            get { return this.careerRepo ?? new GenericRepository<Entities.Career>(Context); }
        }
        public GenericRepository<CareerPath> CareerPath
        {
            get { return this.careerPathRepo ?? new GenericRepository<Entities.CareerPath>(Context); }
        }
        public GenericRepository<ActivityLog> ActivityLogs
        {
            get { return activityLogRepo ?? new GenericRepository<Entities.ActivityLog>(Context); }
        }
        public GenericRepository<Group> Groups
        {
            get { return GroupRepo ?? new GenericRepository<Entities.Group>(Context); }
        }
        public GenericRepository<Question> Questions
        {
            get { return this.QuestionRepo ?? new GenericRepository<Entities.Question>(Context); }
        }
        public GenericRepository<Skills> Skills
        {
            get { return this.SkillRepo ?? new GenericRepository<Entities.Skills>(Context); }
        }
        public GenericRepository<EmployeeRating> EmployeeRatings
        {
            get { return this.EmpRatingRepo ?? new GenericRepository<EmployeeRating>(Context); }
        }
        public GenericRepository<Ratings> Ratings
        {
            get { return this.RatingRepo ?? new GenericRepository<Ratings>(Context); }
        }
        public GenericRepository<Parameters> Parameters
        {
            get { return this.parameterRepo ?? new GenericRepository<Entities.Parameters>(Context); }
        }
        public GenericRepository<AppraisalMode> AppraisalMode
        {
            get { return this.AppraisalModeRepo ?? new GenericRepository<Entities.AppraisalMode>(Context); }
        }
        public GenericRepository<AppraisalPeriod> AppraisalPeriod
        {
            get { return this.AppraisalPeriodRepo ?? new GenericRepository<AppraisalPeriod>(Context); }
        }
        public GenericRepository<AppraisalRating> AppraislRating
        {
            get { return this.AppraisalRatinRepo ?? new GenericRepository<AppraisalRating>(Context); }
        }
        public GenericRepository<AppraisalStatus> AppraisalStatus
        {
            get { return this.AppraisalStatusRepo ?? new GenericRepository<Entities.AppraisalStatus>(Context); }
        }
        public GenericRepository<AppraisalInitialization> AppraisalInitialization
        {
            get { return this.AppraisalInitilizationRepo ?? new GenericRepository<Entities.AppraisalInitialization>(Context); }
        }
        public GenericRepository<AppraisalConfiguration> AppraisalConfiguration
        {
            get { return this.AppraisalConfigurationRepo ?? new GenericRepository<AppraisalConfiguration>(Context); }
        }
        public GenericRepository<SubscribedAppraisal> SubscribedAppraisal
        {
            get { return this.SubscibedAppraisalRepo ?? new GenericRepository<Entities.SubscribedAppraisal>(Context); }
        }
        public GenericRepository<MailDispatcher> MailDispatch
        {
            get { return this.MailDispacthRepo ?? new GenericRepository<MailDispatcher>(Context); }
        }
        public GenericRepository<EmployeeDetailDispatcher> EmpDetailDispatch
        {
            get { return this.EMpDetailDispatchRepo ?? new GenericRepository<EmployeeDetailDispatcher>(Context); }
        }
        public GenericRepository<Menu> Menu
        {
            get { return this.MenuRepository ?? new GenericRepository<Menu>(Context); }
        }
        public GenericRepository<AppraisalQuestion> AppraisalQuestion
        {
            get { return this.AppraisalQuestionRepo ?? new GenericRepository<Entities.AppraisalQuestion>(Context); }
        }
        public GenericRepository<AppraisalResult> AppraisalResult
        {
            get { return this.AppraisalResultRepo ?? new GenericRepository<Entities.AppraisalResult>(Context); }
        }
        public GenericRepository<Candidate> Candidate
        {
            get { return this.CandidateRepo?? new GenericRepository<Entities.Candidate>(Context); }
        }
        public GenericRepository<CandidateWorkDetail> CandidateWorkDetail
        {
            get { return this.CandidateWorkRepo ?? new GenericRepository<Entities.CandidateWorkDetail>(Context); }
        }
        public GenericRepository<Interview> Interview
        {
            get { return this.InterviewRepo ?? new GenericRepository<Entities.Interview>(Context); }
        }
        public GenericRepository<CandidateInterview> CandidateInterview
        {
            get { return this.CandidateInterviewRepo ?? new GenericRepository<Entities.CandidateInterview>(Context); }
        }
        public GenericRepository<CandidateStatus> CandidateStatus
        {
            get { return this.candidateStatusRepo ?? new GenericRepository<Entities.CandidateStatus>(Context); }
        }
        public GenericRepository<InterviewStatus> InterviewStatus
        {
            get { return this.interviewStatusRepo ?? new GenericRepository<Entities.InterviewStatus>(Context); }
        }
        public GenericRepository<InterviewType> InterviewType
        {
            get { return this.interviewTypeRepo ?? new GenericRepository<Entities.InterviewType>(Context); }
        }
        public GenericRepository<Asset> Asset
        {
            get { return this.assetRepo ?? new GenericRepository<Entities.Asset>(Context); }
        }
        public GenericRepository<AssetCategory> AssetCategory
        {
            get { return this.assetCategory ?? new GenericRepository<Entities.AssetCategory>(Context); }
        }
        public GenericRepository<RequestAsset> RequestAsset
        {
            get { return this.requestassetRepo ?? new GenericRepository<Entities.RequestAsset>(Context); }
        }

        public GenericRepository<GeneralQuestion> GeneralQuestion
        {
           get { return this.generalQuestionRepo ?? new GenericRepository<Entities.GeneralQuestion>(Context); }
        }
        public void Save()
        {
            try
            {
                myContext.SaveChanges();
            }catch(Exception ex)
            {
                throw ex;
            }
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

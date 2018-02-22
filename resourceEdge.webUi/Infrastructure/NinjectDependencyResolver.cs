using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using resourceEdge.Domain.Concrete;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using resourceEdge.webUi.Models;

namespace resourceEdge.webUi.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            // put bindings here
            kernel.Bind<IDbContext>().To<Domain.UnitofWork.UnitOfWork>();
            kernel.Bind<Iproduct>().To<ProductRepository>();
            kernel.Bind<IBusinessUnits>().To<BusinessRepository>();
            kernel.Bind<IDepartments>().To<DepartmentRepository>();
            kernel.Bind<IidentityCodes>().To<IdentityRepository>();
            kernel.Bind<IJobtitles>().To<JobTitleRepository>();
            kernel.Bind<IPositions>().To<PositionRepository>();
            kernel.Bind<IPrefixes>().To<PrefixRepository>();
            kernel.Bind<IEmploymentStatus>().To<EmployementStatusRepository>();
            kernel.Bind<IEmployees>().To<EmployeeRepository>();
            kernel.Bind<IReportManager>().To<ReprtManagerRepository>();
            kernel.Bind<ILeaveManagement>().To<LeaveManagementRepo>();
            kernel.Bind<IRequisition>().To<RequisitionRepo>();
            kernel.Bind<IPayroll>().To<PayrollRepository>();
            kernel.Bind<IFiles>().To<FileRepository>();
            kernel.Bind<ILogin>().To<LoginRepository>();
            kernel.Bind<ILevels>().To<LevelRepository>();
            kernel.Bind<ICareers>().To<CareerRepository>();
            kernel.Bind<ICareerPath>().To<CareerPathRepository>();
            kernel.Bind<ILocation>().To<LocationRepository>();
            kernel.Bind<IGroups>().To<GroupRepository>();
            kernel.Bind<IParameter>().To<ParameterRepository>();
            kernel.Bind<ISkills>().To<SkillRepository>();
            kernel.Bind<IQuestions>().To<QuestionRepository>();
            kernel.Bind<IRating>().To<RatingRepository>();
            kernel.Bind<IAppraisalStatus>().To<AppraisalStatusRepository>();
            kernel.Bind<IAppraisalMode>().To<AppraisalModeRepository>();
            kernel.Bind<IAppriaslPeriods>().To<AppraisalPeriodRepository>();
            kernel.Bind<IAppraisalRating>().To<AppraisalRatingRepository>();

;        }
    }
}
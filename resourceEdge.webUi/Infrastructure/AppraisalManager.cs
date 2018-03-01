using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace resourceEdge.webUi.Infrastructure
{
    public class AppraisalManager
    {
        IEmployees employeeRepo;
        IAppraisalConfiguration AppraisalConfigRepo;
        UnitOfWork unitOfWork = new UnitOfWork();
        public AppraisalManager(IEmployees EmpParam, IAppraisalConfiguration AppConfigParam)
        {
            employeeRepo = EmpParam;
            AppraisalConfigRepo = AppConfigParam;
        }
        public AppraisalManager(IEmployees EmpParam)
        {
            employeeRepo = EmpParam;
        }

        public string InitializationCodeGeneration(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public string RandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(InitializationCodeGeneration(4, true));
            builder.Append(InitializationCodeGeneration(2, false));
            return builder.ToString();
        }

        public AppraisalPeriods GetPeriodByName(string name)
        {
            var period = unitOfWork.AppraisalPeriod.Get(filter: x => x.Name == name).FirstOrDefault();
            return period ?? null;
        }
        public List<AppraisalInitialization> GetAllInitialization()
        {
            var result = unitOfWork.AppraisalInitialization.Get(includeProperties: "Group").ToList();
            return result ?? null;
        }
        public bool EnableAppraisal(int id)
        {
            var result = unitOfWork.AppraisalInitialization.GetByID(id);
            try
            {
                if (result != null)
                {
                    result.Enable = true;
                    unitOfWork.AppraisalInitialization.Update(result);
                    return true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return false;  
        }
        public bool SubscribeToAppraisal(string code, string UserId)
        {
            try
            {
                var allAppraisal = unitOfWork.SubscribedAppraisal.Get().ToList();
                var currentAppraisal = unitOfWork.AppraisalInitialization.Get(filter: x => x.InitilizationCode == code).FirstOrDefault();
                var AppliedCount = allAppraisal.Find(x => x.UserId == UserId && x.Code == code);
                if (currentAppraisal != null && AppliedCount == null)
                {
                    var subscription = new SubscribedAppraisal()
                    {
                        AppraisalInitializationId = currentAppraisal.Id,
                        Code = currentAppraisal.InitilizationCode,
                        UserId = UserId
                    };
                    unitOfWork.SubscribedAppraisal.Insert(subscription);
                    unitOfWork.Save();
                    return true;
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            return false;
        }
        public List<BusinessUnits> GetBusinessUnitsByLocation(string userId)
        {
            var employee = employeeRepo.GetByUserId(userId);
            if (employee != null)
            {
                var location = unitOfWork.BusinessUnit.Get(filter: x => x.LocationId == employee.LocationId).ToList();
                if (location != null)
                {
                    return location;
                }
            }
            return null;
        }

        public bool AddOrUpdateAppraisalConfiguration(AppraisalConfigratuionViewModel model, string UserId)
        {
            try
            {
                var ExistingAppraisal = unitOfWork.AppraisalConfiguration.Get(filter: x => x.BusinessUnit == model.BusinessUnit && x.Code == model.Code && UserId == x.CreatedBy).FirstOrDefault();
                if (ExistingAppraisal == null)
                {
                    int EnableTo = 0;
                    var HR = employeeRepo.GetByUserId(UserId);
                    if (HR != null)
                    {
                        int.TryParse(model.EnableTo.ToString(), out EnableTo);
                        AppraisalConfiguration config = new AppraisalConfiguration()
                        {
                            AppraisalStatus = model.AppraisalStatus,
                            BusinessUnit = model.BusinessUnit,
                            Code = model.Code,
                            EnableTo = EnableTo,
                            Department = model.Department.Value,
                            Parameters = model.Parameters.ToString(),
                            Eligibility = model.Eligibility.ToString(),
                            CreatedBy = UserId,
                            CreatedDate = DateTime.Now,
                            Location = HR.LocationId
                        };
                        AppraisalConfigRepo.Insert(config);
                    }
                    else
                    {
                        ExistingAppraisal.LineManager1 = model.LineManager1;
                        ExistingAppraisal.LineManager2 = model.LineManager2;
                        ExistingAppraisal.LineManager3 = model.LineManager3;
                        AppraisalConfigRepo.update(ExistingAppraisal);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }
    }
}
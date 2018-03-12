using Microsoft.AspNet.Identity;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;
using resourceEdge.Domain.ViewModels;
using resourceEdge.webUi.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Infrastructure
{
    public class AppraisalManager
    {
        IEmployees employeeRepo;
        IAppraisalConfiguration AppraisalConfigRepo;
        IQuestions QuestionRepo;
        UnitOfWork unitOfWork = new UnitOfWork();
        Generators Generator = new Generators();
        public AppraisalManager(IEmployees EmpParam, IAppraisalConfiguration AppConfigParam, IQuestions QParam)
        {
            employeeRepo = EmpParam;
            AppraisalConfigRepo = AppConfigParam;
            QuestionRepo = QParam;
        }
        public AppraisalManager(IEmployees EmpParam)
        {
            employeeRepo = EmpParam;
        }

        public SystemViewModel InitAppraisal()
        {
            SystemViewModel model = new SystemViewModel();
            model.Groups = new System.Web.Mvc.SelectList(unitOfWork.Groups.Get().OrderBy(x=>x.GroupName), "Id", "GroupName", "Id");
            model.Rating = new System.Web.Mvc.SelectList(unitOfWork.AppraislRating.Get().ToList(), "Id", "Name", "Id");
            model.Status = new System.Web.Mvc.SelectList(unitOfWork.AppraisalStatus.Get(), "Id", "Name", "Id");
            model.AppraisalMode = new System.Web.Mvc.SelectList(unitOfWork.AppraisalMode.Get(), "Id", "Name", "Id");
            return model;
        }
        public SystemViewModel ConfigureAppraisal(int locationId)
        {
            SystemViewModel model = new SystemViewModel();
            model.BusinessUnits = new System.Web.Mvc.SelectList(GetBusinessUnitsByLocation(locationId).Select(x=> new { BusId = x.Id, unitName = x.unitname }), "BusId", "unitName", "BusId");
            model.Status = new System.Web.Mvc.SelectList(unitOfWork.AppraisalStatus.Get(), "Id", "Name", "Id");
            model.EmploymentStatus = new System.Web.Mvc.SelectList(unitOfWork.employmentStatus.Get().Select(x=> new { empstId = x.empstId, employmentStatus = x.employemntStatus }), "empstId", "employmentStatus", "empstId");
            model.Parameter = new System.Web.Mvc.SelectList(unitOfWork.Parameters.Get().Select(X => new { Text = X.ParameterName, Value = X.Id }), "Value", "Text", "Value");
            return model;
        }

        public string GetInitializationcode(int size, bool lowerCase)
        {
            string code = null;
            string ExistingCode = null;
            do
            {
                code = Generator.CodeGeneration(size, lowerCase);
               var ExistingAppraisal = unitOfWork.AppraisalInitialization.Get(filter: x => x.InitilizationCode == code).Select(x=>x.InitilizationCode).FirstOrDefault();
                if (ExistingCode != null)
                {
                    ExistingCode = ExistingAppraisal;
                }
            } while (code == ExistingCode );
            return code;

        }


        public AppraisalInitialization GetInitializationCode(string code)
        {
            var result = unitOfWork.AppraisalInitialization.Get(filter: x => x.InitilizationCode == code).FirstOrDefault();
            return result;
        }

        //public void AddInitializationToMail(int groupid, DateTime StartDate )
        //{
        //    var GroupName = unitOfWork.Groups.GetByID(groupid).GroupName;
        //    MailDispatcher mail = new MailDispatcher()
        //    {
        //        Delivered = false,
        //        GroupName = GroupName,
        //        Type = Domain.Infrastructures.MailType.Appraisal,
        //        TimeToSend = StartDate
        //    };
        //    unitOfWork.MailDispatch.Insert(mail);
        //    unitOfWork.Save();
        //}

        public bool SubscribeForAppraisal(string code, int locationId, string userId)
        {
            var initialization = GetInitializationCode(code);
            if (initialization != null)
            {
                var subscription = new SubscribedAppraisal()
                {
                    AppraisalInitializationId = initialization.Id,
                    Code = code,
                    LocationId = locationId,
                    UserId = userId
                };
                unitOfWork.SubscribedAppraisal.Insert(subscription);
                unitOfWork.Save();
                return true;
            }
            return false;
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
                    result.StartDate = DateTime.Now;
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
        public List<BusinessUnits> GetBusinessUnitsByLocation(int Id)
        {
                var location = unitOfWork.BusinessUnit.Get(filter: x => x.LocationId == Id ).ToList();
            if (location != null)
            {
                return location;
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

        public bool AddOrUpdateAppraisalQuestion(FormCollection collection = null, int? id = null, QuestionViewModel model = null)
        {
            if (id == null)
            {
                string UserId = HttpContext.Current.User.Identity.GetUserId();
                IDictionary<string, object> myDictionary = new Dictionary<string, object>();
                collection.CopyTo(myDictionary);
                var allKeys = myDictionary.Keys;
                var allquestions = allKeys.Where(x => x.ToLower().StartsWith("quest"));
                var alldescriptions = allKeys.Where(x => x.ToLower().StartsWith("desc"));
                var Employee = allKeys.Where(x => x.StartsWith("Employee"));
                var EmployeeDetail = employeeRepo.GetByUserId(myDictionary[Employee.ElementAt(0)].ToString());

                if (alldescriptions.Count() != 0 && allquestions.Count() != 0)
                {
                    for (int i = 0; i < allquestions.Count(); i++)
                    {
                        var question = myDictionary[allquestions.ElementAtOrDefault(i)].ToString();
                        var description = myDictionary[alldescriptions.ElementAtOrDefault(i)].ToString();
                        Questions Question = new Questions()
                        {
                            Question = question,
                            UserIdForQuestion = myDictionary[Employee.ElementAtOrDefault(0)].ToString(),
                            Description = description,
                            Createdby = UserId,
                            ModifiedDate = DateTime.Now,
                            CreatedDate = DateTime.Now,
                            Isactive = false,
                            BusinessUnitId = EmployeeDetail.businessunitId,
                            DepartmentId = EmployeeDetail.departmentId,
                            GroupId = EmployeeDetail.GroupId,
                            LocationId = EmployeeDetail.LocationId.Value,
                            UserFullName = EmployeeDetail.FullName
                        };
                        try
                        {
                            QuestionRepo.Insert(Question);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                            //return false;
                        }
                    }
                 return true;
                }
            }
            else
            {
                var existingQuestion = unitOfWork.Questions.GetByID(id);
                if (existingQuestion != null)
                {
                    existingQuestion.Question = model.Question;
                    existingQuestion.Description = model.Description;
                    try
                    {
                    QuestionRepo.update(existingQuestion);
                    return true;
                    }catch(Exception ex)
                    {
                        throw ex;
                        //return false
                    }
                }
            }
            return false;
        }
        public AppraiseeDropDown GenerateAppraisalQuestions(string userId, int GroupId, int LocationId)
        {
            var subscribedAppraisal = unitOfWork.SubscribedAppraisal
                                        .Get(includeProperties: "AppraisalInitialization", filter: x => x.GroupId == GroupId
                                        && x.LocationId == LocationId && x.AppraisalInitialization.EndDate > DateTime.Today 
                                        && x.AppraisalInitialization.IsActive == true).FirstOrDefault();
            List <AppraisalInitialization> CurrentAppraisalInprogress = new List<AppraisalInitialization>();

            var userQuestions = unitOfWork.Questions.Get(filter: x => x.UserIdForQuestion == userId).ToList();

            AppraiseeDropDown dropDown = new AppraiseeDropDown()
            {
                Questions = userQuestions,
                RatingType = subscribedAppraisal.AppraisalInitialization.RatingType
            };

            return dropDown ?? null;
        }
    }
}
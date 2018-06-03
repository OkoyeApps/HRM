using Microsoft.AspNet.Identity;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;
using resourceEdge.Domain.ViewModels;
using resourceEdge.webUi.Infrastructure.Core;
using resourceEdge.webUi.Models;
using resourceEdge.webUi.Models.SystemModel;
using System;
using System.Collections;
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


        public SystemViewModel InitAppraisal(int? id = null)
        {
            SystemViewModel model = new SystemViewModel();
            if (id != null)
            {
                model.Groups = new System.Web.Mvc.SelectList(unitOfWork.Groups.Get(filter: x => x.Id == id.Value).Select(y => new { Id = y.Id, GroupName = y.GroupName }), "Id", "GroupName");
            }
            else
            {
                model.Groups = new System.Web.Mvc.SelectList(unitOfWork.Groups.Get().OrderBy(x => x.GroupName), "Id", "GroupName", "Id");
            }
            model.Rating = new System.Web.Mvc.SelectList(unitOfWork.AppraislRating.Get().ToList(), "Id", "Name", "Id");
            model.Status = new System.Web.Mvc.SelectList(unitOfWork.AppraisalStatus.Get(), "Id", "Name", "Id");
            model.AppraisalMode = new System.Web.Mvc.SelectList(unitOfWork.AppraisalMode.Get(), "Id", "Name", "Id");
            return model;
        }
        public SystemViewModel ConfigureAppraisal(int locationId)
        {
            SystemViewModel model = new SystemViewModel();
            model.BusinessUnits = new System.Web.Mvc.SelectList(GetBusinessUnitsByLocation(locationId).Select(x => new { Id = x.Id, unitName = x.unitname }), "Id", "unitName", "Id");
            model.Status = new System.Web.Mvc.SelectList(unitOfWork.AppraisalStatus.Get(), "Id", "Name", "Id");
            model.EmploymentStatus = new System.Web.Mvc.SelectList(unitOfWork.employmentStatus.Get().Select(x => new { empstId = x.empstId, employmentStatus = x.employemntStatus }), "empstId", "employmentStatus", "empstId");
            model.Parameter = new System.Web.Mvc.SelectList(unitOfWork.Parameters.Get().Select(X => new { Text = X.ParameterName, Value = X.Id }), "Value", "Text", "Value");
            return model;
        }

        public IList<YearlistItem> GenerateYearDropDown()
        {
            var currentYear = DateTime.Now.Year;
            IList<YearlistItem> yearArray = new List<YearlistItem>();
            for (int i = 0; i < 5; i++)
            {
                yearArray.Add(new YearlistItem { Name = $"{currentYear}", value = currentYear });
                currentYear = currentYear + 1;
            }
            return yearArray;
        }
        public bool GetOpenAppraisal(int groupId)
        {
            var OpenAppraisal = unitOfWork.AppraisalInitialization.Get(x => x.GroupId == groupId && x.AppraisalStatusId != 2 && x.EndDate > DateTime.Now).FirstOrDefault();
            if (OpenAppraisal != null)
            {
                return false;
            }
            return true;
        }
        public string GetInitializationcode(int size, bool lowerCase)
        {
            string code = null;
            string ExistingCode = null;
            do
            {
                code = Generator.CodeGeneration(size, lowerCase);
                var ExistingAppraisal = unitOfWork.AppraisalInitialization.Get(filter: x => x.InitilizationCode == code).Select(x => x.InitilizationCode).FirstOrDefault();
                if (ExistingCode != null)
                {
                    ExistingCode = ExistingAppraisal;
                }
            } while (code == ExistingCode);
            return code;

        }
        public bool AddAppraisalInitialization(AppraisalInitilizationViewModel model, string Code, int period)
        {
            AppraisalInitialization initilize = new AppraisalInitialization()
            {
                GroupId = model.Group,
                AppraisalModeId = model.AppraisalMode,
                AppraisalStatusId = model.AppraisalStatus,
                StartDate = model.StartDate,
                EndDate = model.DueDate,
                FromYear = model.FromYear,
                InitilizationCode = Code,
                AppraisalPeriodId = period,
                AppraisalRatingId = int.Parse(model.RatingType),
                ToYear = model.ToYear,
                CreatedBy = HttpContext.Current.User.Identity.GetUserId(),
                ModifiedBy = HttpContext.Current.User.Identity.GetUserId(),
                CreatedDate = DateTime.Now
            };
            unitOfWork.AppraisalInitialization.Insert(initilize);
            unitOfWork.Save();
            return true;
        }

        public AppraisalInitialization GetInitializationCode(string code)
        {
            var result = unitOfWork.AppraisalInitialization.Get(filter: x => x.InitilizationCode == code && x.IsActive != false).FirstOrDefault();
            return result;
        }

        public bool SubscribeForAppraisal(string code, int locationId, string userId)
        {
            var initialization = GetInitializationCode(code);
            if (initialization != null)
            {
                var ExistingSubscription = unitOfWork.SubscribedAppraisal.Get(x => x.Code == code);
                if (ExistingSubscription.Count() == 0)
                {
                    var subscription = new SubscribedAppraisal()
                    {
                        AppraisalInitializationId = initialization.Id,
                        Code = code,
                        LocationId = locationId,
                        UserId = userId,
                        IsActive = true,
                        GroupId = initialization.GroupId
                    };
                    unitOfWork.SubscribedAppraisal.Insert(subscription);
                    unitOfWork.Save();
                    return true;
                }
            }
            return false;
        }

        public AppraisalPeriod GetPeriodByName(string name)
        {
            var period = unitOfWork.AppraisalPeriod.Get(filter: x => x.Name == name).FirstOrDefault();
            return period ?? null;
        }
        public List<AppriasalinitializationListItem> GetAllInitialization()
        {
            var result = unitOfWork.AppraisalInitialization.Get(includeProperties: "Group,AppraisalPeriod,AppraisalMode,AppraisalRating,AppraisalStatus")
                .Select(y => new
                AppriasalinitializationListItem
                {
                    AppraisalMode = y.AppraisalMode.Name,
                    AppraisalStatus = y.AppraisalStatus.Name,
                    EndDate = y.EndDate,
                    FromYear = y.FromYear,
                    Group = y.Group.GroupName,
                    Id = y.Id,
                    InitilizationCode = y.InitilizationCode,
                    Period = y.AppraisalPeriod.Name,
                    RatingType = y.AppraisalRating.Name,
                    StartDate = y.StartDate,
                    ToYear = y.ToYear,
                    Enabled = y.Enable
                }).ToList();
            return result ?? null;
        }
        public bool EnableAppraisal(int id)
        {
            var result = unitOfWork.AppraisalInitialization.GetByID(id);
            try
            {
                if (result != null)
                {
                    //result.StartDate = DateTime.Now;
                    result.Enable = true;
                    result.IsActive = true;
                    unitOfWork.AppraisalInitialization.Update(result);
                    unitOfWork.Save();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }
        public List<BusinessUnit> GetBusinessUnitsByLocation(int Id)
        {
            var location = unitOfWork.BusinessUnit.Get(filter: x => x.LocationId == Id).ToList();
            if (location != null)
            {
                return location;
            }
            return null;
        }

        public bool AddOrUpdateAppraisalConfiguration(AppraisalConfigratuionViewModel model, string UserId, int groupId, int locationId)
        {
            try
            {
                Rolemanager RoleManager = new Rolemanager();
                var UserManager = UserManagement.userManager;
                var ExistingAppraisal = unitOfWork.AppraisalConfiguration.Get(filter: x => x.BusinessUnitId == model.BusinessUnit && x.Code == model.Code && x.DepartmentId == model.Department.Value && x.IsActive == true).FirstOrDefault();
                var SubScribedApraisal = unitOfWork.SubscribedAppraisal.Get(filter: x => x.GroupId == groupId && x.LocationId == locationId && x.IsActive == true).FirstOrDefault();
                if (ExistingAppraisal == null)
                {
                    var HR = employeeRepo.GetByUserId(UserId);
                    if (HR != null)
                    {
                        AppraisalConfiguration config = new AppraisalConfiguration()
                        {
                            AppraisalStatus = model.AppraisalStatus,
                            BusinessUnitId = model.BusinessUnit,
                            Code = model.Code,
                            DepartmentId = model.Department.Value,
                            Parameters = model.Parameters.ToString(),
                            Eligibility = model.Eligibility.ToString(),
                            CreatedBy = UserId,
                            EnableTo = 0,//remove this later from the model
                            CreatedDate = DateTime.Now,
                            LocationId = HR.LocationId,
                            LineManager1 = model.LineManager1,
                            LineManager2 = model.LineManager2,
                            LineManager3 = model.LineManager3,
                            IsActive = true,
                            AppraisalInitializationId = SubScribedApraisal.AppraisalInitializationId,
                        };
                        AppraisalConfigRepo.Insert(config);
                        RoleManager.CreateTemporaryRoleForAppraisal();
                        UserManager.AddToRole(model.LineManager1, "L1");
                        UserManager.AddToRole(model.LineManager2, "L2");
                        UserManager.AddToRole(model.LineManager3, "L3");
                        return true;
                    }
                    else
                    {
                        ExistingAppraisal.LineManager1 = model.LineManager1;
                        ExistingAppraisal.LineManager2 = model.LineManager2;
                        ExistingAppraisal.LineManager3 = model.LineManager3;
                        AppraisalConfigRepo.update(ExistingAppraisal);
                        //Remove Former user From Role
                        UserManager.RemoveFromRole(ExistingAppraisal.LineManager1, "L1");
                        UserManager.RemoveFromRole(ExistingAppraisal.LineManager2, "L2");
                        UserManager.RemoveFromRole(ExistingAppraisal.LineManager3, "L3");
                        //Add New users To role
                        UserManager.AddToRole(model.LineManager1, "L1");
                        UserManager.AddToRole(model.LineManager2, "L2");
                        UserManager.AddToRole(model.LineManager3, "L3");
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

        public bool ValidateAppraisalCode(string code)
        {
            var result = unitOfWork.AppraisalInitialization.Get(filter: x => x.InitilizationCode == code && x.IsActive == true).Any(x => x.InitilizationCode == code);
            if (result)
            {
                return true;
            }
            return false;
        }

        public bool validateDepartmentForAppraisal(int? groupId, int locationId,string code, int? unitId, int? departmentId)
        {
            var result = unitOfWork.AppraisalConfiguration.Get(filter: x => x.LocationId == locationId && x.BusinessUnitId == unitId && x.DepartmentId == departmentId && x.Code == code).FirstOrDefault();
            if (result != null)
            {
                return false;
            }
            return true;
        }

        public bool AddOrUpdateAppraisalQuestion(FormCollection collection = null, int? id = null, QuestionViewModel model = null)
        {
            try
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
                            Question Question = new Question()
                            {
                                EmpQuestion = question,
                                UserIdForQuestion = myDictionary[Employee.ElementAtOrDefault(0)].ToString(),
                                Description = description,
                                Createdby = UserId,
                                ModifiedDate = DateTime.Now,
                                CreatedDate = DateTime.Now,
                                Isactive = false,
                                BusinessUnitId = EmployeeDetail.businessunitId,
                                DepartmentId = EmployeeDetail.DepartmentId,
                                GroupId = EmployeeDetail.GroupId,
                                LocationId = EmployeeDetail.LocationId.Value,
                                UserFullName = EmployeeDetail.FullName,
                            };
                            QuestionRepo.Insert(Question);
                        }
                    }
                    return true;
                }
                else
                {
                    var existingQuestion = unitOfWork.Questions.GetByID(id);
                    if (existingQuestion != null)
                    {
                        existingQuestion.EmpQuestion = model.Question;
                        existingQuestion.Description = model.Description;
                        QuestionRepo.update(existingQuestion);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
                //return false
            }
        }
        public AppraiseeDropDown GenerateAppraisalQuestions(string userId, int GroupId, int LocationId)
        {
            //this checks to make sure that your location has subscribed for thr appraisal process
            var subscribedAppraisal = unitOfWork.SubscribedAppraisal
                                        .Get(includeProperties: "AppraisalInitialization", filter: x => x.GroupId == GroupId
                                        && x.LocationId == LocationId && x.AppraisalInitialization.EndDate.Day != DateTime.Today.Day
                                        && x.AppraisalInitialization.IsActive == true).Any(x => x.IsActive == true);
            var userSessionObject = (SessionModel)HttpContext.Current.Session["_ResourceEdgeTeneceIdentity"];
            if (subscribedAppraisal)
            {
                List<AppraisalInitialization> CurrentAppraisalInprogress = new List<AppraisalInitialization>();

                var userQuestions = unitOfWork.Questions.Get(filter: x => x.UserIdForQuestion == userId && x.Approved == true).ToList();
                var DepartmentQuestions = unitOfWork.GeneralQuestion.Get(filter: x => x.BusinessUnitId == userSessionObject.UnitId && x.DepartmentId == userSessionObject.DepartmentId).ToList();
                var GeneralQuestions = unitOfWork.GeneralQuestion.Get(filter: x => x.GroupId == userSessionObject.GroupId && x.DepartmentId == null).ToList();
                IList<Question> QuestionToRemove = new List<Question>();
                IList<GeneralQuestion> GeneralQuestionToRemove = new List<GeneralQuestion>();
                IList<GeneralQuestion> DepartmentQuestionToRemove = new List<GeneralQuestion>();
                IList<GeneralQuestion> GeneralQuestion = new List<GeneralQuestion>();
                IList<GeneralQuestion> DepartmentQuestion = new List<GeneralQuestion>();
                IDictionary<int, int> answers = new Dictionary<int, int>();
                IDictionary<int, int> GeneralQuestionAns = new Dictionary<int, int>();
                IDictionary<int, int> DepartmentQuestionAns = new Dictionary<int, int>();

                //this is for the user specific Kpis
                if (userQuestions.Count > 0)
                {
                    foreach (var item in userQuestions)
                    {
                        var AppraisalQuestion = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == item.UserIdForQuestion && x.QuestionId == item.Id).FirstOrDefault();

                        if (AppraisalQuestion != null)
                        {
                            if (AppraisalQuestion.IsSubmitted == true && (AppraisalQuestion.L3Status == true || AppraisalQuestion.L3Status == null))
                            {
                                QuestionToRemove.Add(item);
                            }
                            else if (AppraisalQuestion.IsSubmitted == true && AppraisalQuestion.L3Status == false)
                            {
                                answers.Add(item.Id, AppraisalQuestion.Answer);
                            }
                        }
                    }
                    if (QuestionToRemove.Count > 0)
                    {
                        QuestionToRemove.ToList().ForEach(x => userQuestions.Remove(x));
                    }


                    //this is for the user Group Kpis
                    if (GeneralQuestions.Count > 0)
                    {
                        foreach (var item in GeneralQuestions)
                        {
                            var AppraisalQuestion = unitOfWork.AppraisalQuestion.Get(filter: x => x.GroupId == item.GroupId && x.GeneralQuestionId == item.Id && x.UserId == userId).FirstOrDefault();

                            if (AppraisalQuestion != null)
                            {
                                if (AppraisalQuestion.IsSubmitted == true && (AppraisalQuestion.L3Status == true || AppraisalQuestion.L3Status == null))
                                {

                                    GeneralQuestionToRemove.Add(item);
                                }
                                else if (AppraisalQuestion.IsSubmitted == true && AppraisalQuestion.L3Status == false)
                                {
                                    GeneralQuestionAns.Add(item.Id, AppraisalQuestion.Answer);
                                }
                            }
                        }
                        if (GeneralQuestionToRemove.Count > 0)
                        {
                            GeneralQuestionToRemove.ToList().ForEach(x => GeneralQuestions.Remove(x));
                        }


                        //this is for the user Department specific Kpis
                        if (DepartmentQuestions.Count > 0)
                        {
                            foreach (var item in DepartmentQuestions)
                            {
                                var AppraisalQuestion = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == userId && x.DepartmentQuestionId == item.Id).FirstOrDefault();

                                if (AppraisalQuestion != null)
                                {
                                    if (AppraisalQuestion.IsSubmitted == true && (AppraisalQuestion.L3Status == true || AppraisalQuestion.L3Status == null))
                                    {
                                        DepartmentQuestionToRemove.Add(item);
                                    }
                                    else if (AppraisalQuestion.IsSubmitted == true && AppraisalQuestion.L3Status == false)
                                    {
                                        DepartmentQuestionAns.Add(item.Id, AppraisalQuestion.Answer);
                                    }
                                }
                            }
                            if (DepartmentQuestionToRemove.Count > 0)
                            {
                                DepartmentQuestionToRemove.ToList().ForEach(x => DepartmentQuestions.Remove(x));
                            }

                        }

                    }
                    AppraiseeDropDown dropDown = new AppraiseeDropDown()
                    {
                        Questions = userQuestions,
                        PreviousAnswer = (Dictionary<int, int>)answers,
                        DepartmentPreviousAnswer = (Dictionary<int, int>)DepartmentQuestionAns,
                        GeneralPreviousAnswer = (Dictionary<int, int>)GeneralQuestionAns,
                        DepartmentQuestion = DepartmentQuestions,
                        GeneralQuestion = GeneralQuestions

                        //RatingType = subscribedAppraisal.AppraisalInitialization.RatingType
                    };
                    return dropDown ?? null;
                }
            }
            return null;
        }
        public bool AddOrUpdateAppraisalQuestion(FormCollection model, string userId)
        {
            try
            {
                List<AppraisalQuestion> QuestionList = new List<AppraisalQuestion>();
                AppraisalQuestion AppQuestion;
                IDictionary<string, object> myDIctionary = new Dictionary<string, object>();
                var userSessionObject = (SessionModel)HttpContext.Current.Session["_ResourceEdgeTeneceIdentity"];
                if (userSessionObject != null)
                {
                    var appraisalConfigurationDetails = unitOfWork.AppraisalConfiguration
                        .Get(filter: x => x.LocationId == userSessionObject.LocationId
                        && x.BusinessUnitId == userSessionObject.UnitId
                        && x.IsActive == true).LastOrDefault();
                    if (appraisalConfigurationDetails != null)
                    {
                        //This check actually sets up some parameters that would be used in the assignments
                        model.CopyTo(myDIctionary);
                        var allKeys = model.AllKeys.ToList();

                        int answer = 0;
                        int question = 0;
                        string[] EmployeeQuestions = null;
                        string[] GroupQuestions = null;
                        string[] DepartmentQuestions = null;
                        if (myDIctionary.ContainsKey("Question"))
                        {
                             EmployeeQuestions = myDIctionary["Question"].ToString().Split(',');
                        }
                        if (myDIctionary.ContainsKey("GenQuestion"))
                        {
                            GroupQuestions = myDIctionary["GenQuestion"].ToString().Split(',');
                        }
                        if (myDIctionary.ContainsKey("deptQuestion"))
                        {
                             DepartmentQuestions = myDIctionary["deptQuestion"].ToString().Split(',');
                        }

                        //Answers which include employee, General and departmental
                        var departmentnAns = allKeys.Where(x => x.StartsWith("department")).ToList();
                        var GroupAns = allKeys.Where(x => x.StartsWith("general")).ToList();
                        var questionAns = allKeys.Where(x => x.StartsWith("questionAns")).ToList();

                        //this loop adds the Employee Questions and answers
                        if (EmployeeQuestions != null )
                        {
                            for (int i = 0; i < EmployeeQuestions.Count(); i++)
                            {
                                int.TryParse(EmployeeQuestions[i], out question);
                                if (myDIctionary.ContainsKey(questionAns[i]))
                                {
                                    int.TryParse(myDIctionary[questionAns[i]].ToString(), out answer);
                                }
                                var existingQuestion = unitOfWork.AppraisalQuestion.Get(filter: x => x.QuestionId == question && x.UserId == userId).FirstOrDefault();
                                if (question > 0 && existingQuestion == null)
                                {
                                    AppQuestion = new AppraisalQuestion()
                                    {
                                        QuestionId = question,
                                        Answer = answer,
                                        GroupId = userSessionObject.GroupId,
                                        L1Status = null,
                                        L2Status = null,
                                        L3Status = null,
                                        LineManager1 = appraisalConfigurationDetails.LineManager1,
                                        LinrManager2 = appraisalConfigurationDetails.LineManager2,
                                        LineManager3 = appraisalConfigurationDetails.LineManager3,
                                        LocationId = userSessionObject.LocationId,
                                        UserId = userId,
                                        IsSubmitted = true
                                    };
                                    QuestionList.Add(AppQuestion);
                                }
                                else
                                {
                                    existingQuestion.Answer = answer;
                                    existingQuestion.L3Status = null;
                                    existingQuestion.EditCount = existingQuestion.EditCount != null? existingQuestion.EditCount + 1 : 1;
                                    unitOfWork.AppraisalQuestion.Update(existingQuestion);
                                }
                            }
                        }
                        //this loop adds the Group Questions and answers
                        if (GroupQuestions != null)
                        {
                            for (int i = 0; i < GroupQuestions.Count(); i++)
                            {
                                int.TryParse(GroupQuestions[i], out question);
                                if (myDIctionary.ContainsKey(GroupAns[i]))
                                {
                                    int.TryParse(myDIctionary[GroupAns[i]].ToString(), out answer);
                                }
                                var existingQuestion = unitOfWork.AppraisalQuestion.Get(filter: x => x.GeneralQuestionId == question && x.UserId == userId).FirstOrDefault();
                                if (question > 0 && existingQuestion == null)
                                {
                                    AppQuestion = new AppraisalQuestion()
                                    {
                                        GeneralQuestionId = question,
                                        Answer = answer,
                                        GroupId = userSessionObject.GroupId,
                                        L1Status = null,
                                        L2Status = null,
                                        L3Status = null,
                                        LineManager1 = appraisalConfigurationDetails.LineManager1,
                                        LinrManager2 = appraisalConfigurationDetails.LineManager2,
                                        LineManager3 = appraisalConfigurationDetails.LineManager3,
                                        LocationId = userSessionObject.LocationId,
                                        UserId = userId,
                                        IsSubmitted = true
                                    };
                                    QuestionList.Add(AppQuestion);
                                }
                                else
                                {
                                    existingQuestion.Answer = answer;
                                    existingQuestion.L3Status = null;
                                    existingQuestion.EditCount = existingQuestion.EditCount != null ? existingQuestion.EditCount + 1 : 1;
                                    unitOfWork.AppraisalQuestion.Update(existingQuestion);
                                }
                            }
                        }

                        //this loop adds the Departmental Questions and answers
                        if (DepartmentQuestions != null)
                        {
                            for (int i = 0; i < DepartmentQuestions.Count(); i++)
                            {                  
                                int.TryParse(DepartmentQuestions[i], out question);
                                if (myDIctionary.ContainsKey(departmentnAns[i]))
                                {
                                    int.TryParse(myDIctionary[departmentnAns[i]].ToString(), out answer);
                                }
                                var existingQuestion = unitOfWork.AppraisalQuestion.Get(filter: x => x.DepartmentQuestionId == question && x.UserId == userId).FirstOrDefault();
                                if (question > 0 && existingQuestion == null)
                                {
                                    AppQuestion = new AppraisalQuestion()
                                    {
                                        DepartmentQuestionId = question,
                                        BusinessUnitId = userSessionObject.UnitId,
                                        Answer = answer,
                                        GroupId = userSessionObject.GroupId,
                                        L1Status = null,
                                        L2Status = null,
                                        L3Status = null,
                                        LineManager1 = appraisalConfigurationDetails.LineManager1,
                                        LinrManager2 = appraisalConfigurationDetails.LineManager2,
                                        LineManager3 = appraisalConfigurationDetails.LineManager3,
                                        LocationId = userSessionObject.LocationId,
                                        UserId = userId,
                                        IsSubmitted = true
                                    };
                                    QuestionList.Add(AppQuestion);
                                }
                                else
                                {
                                    existingQuestion.Answer = answer;
                                    existingQuestion.L3Status = null;
                                    existingQuestion.EditCount = existingQuestion.EditCount + 1;
                                    unitOfWork.AppraisalQuestion.Update(existingQuestion);
                                }
                            }
                        }
                        if (QuestionList.Count > 0)
                        {
                            QuestionList.ForEach(x => unitOfWork.AppraisalQuestion.Insert(x));
                        }
                        unitOfWork.Save();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CompareIntegers(params int[] values)
        {
            var result = values.Max();
            return result;
        }

        public bool IsLineManager()
        {
            var UserId = HttpContext.Current.User.Identity.GetUserId();
            var IsLineManager = unitOfWork.AppraisalConfiguration.Get(filter: x => x.LineManager1 == UserId || x.LineManager2 == UserId || x.LineManager3 == UserId).Any();
            return IsLineManager != false ? true : false;

        }
        public List<AppraisalQuestion> GetAppraisalForLineManager()
        {
            //Check if the HttpContext can be initialized in the default constructor
            var UserId = HttpContext.Current.User.Identity.GetUserId();
            if (IsLineManager() != false)
            {
                var lineManagerQuestions = unitOfWork.AppraisalQuestion.Get(filter: x => x.LineManager1 == UserId || x.LinrManager2 == UserId || x.LineManager3 == UserId).ToList();
                if (lineManagerQuestions != null || lineManagerQuestions.Count > 0)
                {
                    return lineManagerQuestions;
                }
            }
            return null;
        }
        public IEnumerable<EmployeeListItem> GetAllEmployeeForLineManagerToAppraise()
        {
            var UserId = HttpContext.Current.User.Identity.GetUserId();
            IList<EmployeeListItem> EmployeeList = new List<EmployeeListItem>();
            if (IsLineManager() != false)
            {
                var lineManagerUnitToAppraise = unitOfWork.AppraisalConfiguration.Get(filter: x => x.LineManager1 == UserId || x.LineManager2 == UserId || x.LineManager3 == UserId).ToList().Select(x => new { unit = x.BusinessUnitId, dept = x.DepartmentId });
                if (lineManagerUnitToAppraise.Count() > 0)
                {
                    foreach (var item in lineManagerUnitToAppraise)
                    {
                        var AllEmployees = unitOfWork.employees.Get(filter: x => x.businessunitId == item.unit && x.DepartmentId == item.dept)
                                                 .Select(x => new EmployeeListItem()
                                                 {
                                                     FullName = x.FullName,
                                                     userId = x.userId,
                                                     empEmail = x.empEmail,
                                                     DepartmentName = unitOfWork.Department.GetByID(x.DepartmentId).deptname,
                                                     BusinessUnitName = unitOfWork.BusinessUnit.GetByID(x.businessunitId).unitname
                                                     //Be careful when using this, i could do this i.e using the the dbcontext in this manner
                                                     //this is safe because the application uses one dbcontext per request...
                                                 }).ToList();
                        AllEmployees.ForEach(X => EmployeeList.Add(X));
                    }
                }
                return EmployeeList;
            }
            return null;
        }

        public IEnumerable<AppraisalQuestionViewModel> ViewSubmittedEmployeeAppraisal(string userId)
        {
            var EmployeeQuestions = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == userId && x.IsSubmitted == true, includeProperties: "Question,GeneralQuestion,DepartmentQuestion");
            var EditCount = EmployeeQuestions.Any(x => x.EditCount == 2);
            IList<AppraisalQuestionViewModel> EmployeeAppraisalQuestion = new List<AppraisalQuestionViewModel>();
            if (HttpContext.Current.User.IsInRole("L1") && !HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains("myappraisal"))
            {
                EmployeeAppraisalQuestion = GenerateAllQuestionsForView(userId);//     EmployeeQuestions.Where(X => X.L1Status == null).Select(x => new AppraisalQuestionViewModel() { Question = x.Question.EmpQuestion, Answers = x.Answer, id = x.QuestionId.Value }).ToList();
            }
            if (HttpContext.Current.User.IsInRole("L2") && !HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains("myappraisal"))
            {
                EmployeeAppraisalQuestion = GenerateAllQuestionsForView(userId);// EmployeeQuestions.Where(X => X.L2Status == null).Select(x => new AppraisalQuestionViewModel() { Question = x.Question.EmpQuestion, Answers = x.Answer, id = x.QuestionId.Value }).ToList();
            }
            if (EmployeeQuestions != null && EmployeeQuestions.Count() > 0)
            {

                if (EditCount && HttpContext.Current.User.IsInRole("L3") && !HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains("myappraisal"))
                {
                    var appraisalQuestion = EmployeeQuestions.Where(X => X.L3Status == null || X.L3Status == false);
                    if (appraisalQuestion != null)
                    {
                        foreach (var item in appraisalQuestion)
                        {
                        var AppraisalQuestion = new AppraisalQuestionViewModel();
                            if (item.QuestionId != null)
                            {
                                AppraisalQuestion.Question = item.Question.EmpQuestion;
                                AppraisalQuestion.id = item.Id;
                            }
                            else if (item.GeneralQuestionId != null)
                            {
                                AppraisalQuestion.Question = item.GeneralQuestion.Question;
                                AppraisalQuestion.id = item.Id;
                            }
                            else if (item.DepartmentQuestionId != null)
                            {
                                AppraisalQuestion.Question = item.DepartmentQuestion.Question;
                                AppraisalQuestion.id = item.Id;
                            }
                            AppraisalQuestion.Answers = item.Answer;
                            EmployeeAppraisalQuestion.Add(AppraisalQuestion);
                        }
                        if (EmployeeAppraisalQuestion.Count > 0)
                        {
                            EmployeeAppraisalQuestion.ElementAt(0).EditCount = true;
                        }
                    //EmployeeAppraisalQuestion = appraisalQuestion.Select(x => new AppraisalQuestionViewModel() { Question = x.Question.EmpQuestion, Answers = x.Answer, id = x.QuestionId }).ToList();
                    //var result = EmployeeAppraisalQuestion.Where(x => x.Question != null).FirstOrDefault();
                    //result.EditCount = true;
                    //EmployeeAppraisalQuestion.Remove(result);
                    //EmployeeAppraisalQuestion.Add(result);

                    }
                }
                else if(HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains("myappraisal") || HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains("employeeappraisal"))
                {
                   var EmployeesQuestion = EmployeeQuestions.Where(X => X.L3Status == null);
                    foreach (var item in EmployeeQuestions)
                    {
                        var Question = new AppraisalQuestionViewModel();
                        if (item.Question != null)
                        {
                            Question.Question = item.Question.EmpQuestion;
                            Question.Answers = item.Answer;
                            Question.id = item.QuestionId;
                            Question.Status = item.L3Status;
                            Question.Type = Domain.Infrastructures.AppraisalQuestionType.Personal;
                        }
                        else if(item.GeneralQuestion != null)
                        {
                            Question.Question = item.GeneralQuestion.Question;
                            Question.Answers = item.Answer;
                            Question.id = item.GeneralQuestionId;
                            Question.Status = item.L3Status;
                            Question.Type = Domain.Infrastructures.AppraisalQuestionType.general;
                        }
                        else if (item.DepartmentQuestion != null)
                        {
                            Question.Question = item.DepartmentQuestion.Question;
                            Question.Answers = item.Answer;
                            Question.id = item.DepartmentQuestionId;
                            Question.Status = item.L3Status;
                            Question.Type = Domain.Infrastructures.AppraisalQuestionType.Department;
                        }
                        EmployeeAppraisalQuestion.Add(Question);
                    }
                }
            }

            return (IEnumerable<AppraisalQuestionViewModel>)EmployeeAppraisalQuestion ?? null;
        }

        public IList<AppraisalQuestionViewModel> GenerateAllQuestionsForView(string userId)
        {
            IList<AppraisalQuestionViewModel> EmployeeAppraisalQuestion = new List<AppraisalQuestionViewModel>();
            var EmployeeQuestions = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == userId && x.IsSubmitted == true, includeProperties: "Question,GeneralQuestion,DepartmentQuestion");
            IList<AppraisalQuestion> AppraisalQuestion = new List<AppraisalQuestion>();
            if (HttpContext.Current.User.IsInRole("L1"))
            {
                AppraisalQuestion = EmployeeQuestions.Where(X => X.L1Status == null).ToList();
            }
            if (HttpContext.Current.User.IsInRole("L2"))
            {
                AppraisalQuestion = EmployeeQuestions.Where(X => X.L2Status == null).ToList();
            }
            if (AppraisalQuestion != null)
            {
                foreach (var item in AppraisalQuestion)
                {
                    var Questions = new AppraisalQuestionViewModel();
                    if (item.QuestionId != null)
                    {
                        Questions.Question = item.Question.EmpQuestion;
                        Questions.id = item.Id;
                    }
                    else if (item.GeneralQuestionId != null)
                    {
                        Questions.Question = item.GeneralQuestion.Question;
                        Questions.id = item.Id;
                    }
                    else if (item.DepartmentQuestionId != null)
                    {
                        Questions.Question = item.DepartmentQuestion.Question;
                        Questions.id = item.Id;
                    }
                    Questions.Answers = item.Answer;
                    EmployeeAppraisalQuestion.Add(Questions);
                }
            }
            return EmployeeAppraisalQuestion;
        }

        /// <summary>
        /// This method checks the user Question and approves the question. 
        /// It also checks the current linemanager and approves the Final result, based on the lineManager involved.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="QuestionId"></param>
        /// <returns></returns>
        public bool ApproveAppraisalQuestion(string userId, int? QuestionId, int? type, FormCollection collection)
        {
            if (QuestionId != null)
            {
                AppraisalQuestion result = null;
                switch (type)
                {
                    case 1:
                        result = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == userId && x.QuestionId == QuestionId).FirstOrDefault();
                        break;
                    case 2:
                        result = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == userId && x.GeneralQuestionId == QuestionId).FirstOrDefault();
                        break;
                    case 3:
                        result = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == userId && x.DepartmentQuestionId == QuestionId).FirstOrDefault();
                        break;
                }
                if (result != null)
                {
                    if (HttpContext.Current.User.IsInRole("L1") && result.EditCount == null || result.EditCount <= 2) //this checks for edits less than or equal to 3 and approves finally from the lineManager1
                    {
                        result.L1Status = true;
                        result.IsAccepted = true;
                    }
                    else if (HttpContext.Current.User.IsInRole("L2"))
                    {
                        result.L2Status = true;
                    }
                    else if (HttpContext.Current.User.IsInRole("L3"))
                    {
                        result.L3Status = true;
                    }
                    unitOfWork.AppraisalQuestion.Update(result);
                    unitOfWork.Save();
                    return true;
                }
            }
            if (QuestionId == null)
            {
                IEnumerable<AppraisalQuestion> result = new List<AppraisalQuestion>();
                var editCount = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == userId).Any(x => x.EditCount == 2);
               
                if (editCount && HttpContext.Current.User.IsInRole("L3"))
                {
                    result = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == userId && x.L3Status != true);
                }
                else if (HttpContext.Current.User.IsInRole("L1") || HttpContext.Current.User.IsInRole("L2"))
                {
                    result = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == userId && x.L3Status == true);
                }
                else {
                    result = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == userId && x.L3Status != false && x.L3Status != true);
                }
                Dictionary<string, int> AllModel = new Dictionary<string, int>();
                IList<AppraisalQuestion> QuestionsToUpdate = new List< AppraisalQuestion> ();
                if (editCount && HttpContext.Current.User.IsInRole("L3"))
                {
                    var allKeys = collection.AllKeys;
                    var AllQuestionKeys = allKeys.Where(x => x.StartsWith("item.id")).FirstOrDefault();
                    var allAnswers = allKeys.Where(x => x.StartsWith("AnswerFromL3")).FirstOrDefault();
                    var allQuestionId = collection[AllQuestionKeys].Split(',');
                    var AllAnswers = collection[allAnswers].Split(',');
                    for (int i = 0; i < allQuestionId.Length; i++)
                    {
                        AllModel.Add(allQuestionId[i], int.Parse(AllAnswers[i])); //Change this code later to tryParse

                    }
                }
                if (result != null && result.Count() > 0)
                {

                    foreach (var item in result)
                    {

                        if (HttpContext.Current.User.IsInRole("L1")) //this checks for edits less than or equal to 3 and approves finally from the lineManager1
                        {
                            item.L1Status = true;
                            item.IsAccepted = true;
                        }
                        else if (HttpContext.Current.User.IsInRole("L2"))
                        {
                            item.L2Status = true;
                        }
                        else if (HttpContext.Current.User.IsInRole("L3"))
                        {
                            item.L3Status = true;
                            if (editCount)
                            {
                                if (AllModel.ContainsKey(item.Id.ToString()))
                                {

                                    var ans = AllModel[item.Id.ToString()];
                                    item.Answer = ans;
                                    
                                    QuestionsToUpdate.Add(item);
                                }
                            }
                        }
                    }
                    foreach (var item in result)
                    {
                        if (QuestionsToUpdate.Any(x=>x.Id == item.Id))
                        {
                            item.Answer = QuestionsToUpdate.Where(x => x.Id == item.Id).FirstOrDefault().Answer;
                        }
                    }
                    //QuestionsToUpdate.ToList().ForEach(x => result.ToList().Remove(x));
                    //QuestionsToUpdate.ToList().ForEach(x => result.ToList().Add(x));
                    result.ToList().ForEach(X => unitOfWork.AppraisalQuestion.Update(X));
                    unitOfWork.Save();
                    return true;
                }
            }
            return false;
        }
        public bool? DenyAppraisalQuestion(string userId, int? QuestionId, int? type)
        {
            if (QuestionId != null)
            {
                AppraisalQuestion result = null;
                switch (type)
                {
                    case 1:
                        result = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == userId && x.QuestionId == QuestionId).FirstOrDefault();
                        break;
                    case 2:
                        result = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == userId && x.GeneralQuestionId == QuestionId).FirstOrDefault();
                        break;
                    case 3:
                        result = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == userId && x.DepartmentQuestionId == QuestionId).FirstOrDefault();
                        break;
                }
                var isEditCountComplete = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == userId).Any(x => x.EditCount == 2);
                if (result != null && !isEditCountComplete)
                {
                    if (HttpContext.Current.User.IsInRole("L1")) //this checks for edits less than or equal to 3 and approves finally from the lineManager1
                    {
                        result.L1Status = false;
                        result.IsAccepted = false;
                    }
                    else if (HttpContext.Current.User.IsInRole("L2"))
                    {
                        result.L2Status = false;
                    }
                    else if (HttpContext.Current.User.IsInRole("L3"))
                    {
                        result.L3Status = false;
                    }
                    unitOfWork.AppraisalQuestion.Update(result);
                    unitOfWork.Save();
                    return true;
                }
                return null;
            }
            if (QuestionId == null)
            {
                var result = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == userId && x.L3Status != false);
                if (result != null)
                {
                    foreach (var item in result)
                    {
                        if (HttpContext.Current.User.IsInRole("L1") && item.EditCount == null || item.EditCount <= 2) //this checks for edits less than or equal to 3 and approves finally from the lineManager1
                        {
                            item.L1Status = false;
                            item.IsAccepted = false;
                        }
                        else if (HttpContext.Current.User.IsInRole("L2"))
                        {
                            item.L2Status = false;
                        }
                        else if (HttpContext.Current.User.IsInRole("L3"))
                        {
                            item.L3Status = false;
                        }
                    }
                    result.ToList().ForEach(X => unitOfWork.AppraisalQuestion.Update(X));
                    unitOfWork.Save();
                    return true;
                }
            }
            return false;
        }

        public Tuple<IList<EmployeeListItem>, IList<EmployeeListItem>, IList<EmployeeListItem>> GetLineManagerViews()
        {
            var UserPrincipal = HttpContext.Current.User;
            var IsInRole = HttpContext.Current.User.IsInRole("L1");
            IList<EmployeeListItem> AllEmployeesToAppraise = new List<EmployeeListItem>();
            IList<EmployeeListItem> EmployeeToAttend = new List<EmployeeListItem>();
            IList<EmployeeListItem> EmployeesForL2 = new List<EmployeeListItem>();
            IList<EmployeeListItem> EmployeesForL3 = new List<EmployeeListItem>();
            var userId = UserPrincipal.Identity.GetUserId();
            var DepartmentToAppraise = unitOfWork.AppraisalConfiguration.Get(filter: x => x.LineManager1 == userId || x.LineManager2 == userId || x.LineManager3 == userId)
                .Select(x => new EmployeeListItem
                {
                    departmentId = x.DepartmentId.Value,
                    businessunitId = x.BusinessUnitId.Value
                });
            if (DepartmentToAppraise != null || DepartmentToAppraise.Count() > 0)
            {
                //This foreach gets all the user in a current department for appraisal only
                foreach (var item in DepartmentToAppraise)
                {
                    var employee = unitOfWork.employees.Get(filter: x => x.businessunitId == item.businessunitId && x.DepartmentId == item.departmentId).Select(x => new EmployeeListItem() { userId = x.userId });
                    if (employee != null)
                    {
                        employee.ToList().ForEach(x => AllEmployeesToAppraise.Add(x));
                    }
                }
                //This checks if those current users appraisals is completed
                foreach (var item in AllEmployeesToAppraise)
                {
                    var EmployeeAppraisal = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == item.userId);
                    bool EmployeeForL2 = false;
                    bool EmployeeToAppraise = false;
                    bool EmployeeForL3 = false;
                    if (EmployeeAppraisal != null && EmployeeAppraisal.Count() > 0)
                    {
                        if (HttpContext.Current.User.IsInRole("L1"))
                        {
                            EmployeeToAppraise = EmployeeAppraisal.All(X => X.L1Status == null && (X.L3Status != null && X.L3Status == true) && (X.L2Status != null && X.L2Status.Value == true));
                        }
                        if (HttpContext.Current.User.IsInRole("L2") || UserPrincipal.IsInRole("L1"))
                        {
                        EmployeeForL2 = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == item.userId).All(X => X.L3Status == true && (X.L2Status == null || X.L2Status == false));
                        }
                        if (HttpContext.Current.User.IsInRole("L3") || HttpContext.Current.User.IsInRole("L2") || UserPrincipal.IsInRole("L1"))
                        {
                         EmployeeForL3 = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == item.userId).Any(X => X.L3Status == null || X.L3Status == false);
                        }
                    }

                    //This returns the Employees for LineManager 1 to Appraise
                    if (EmployeeToAppraise && UserPrincipal.IsInRole("L1"))
                    {
                        var currentEmployee = unitOfWork.employees.Get(filter: x => x.userId == item.userId, includeProperties: "Department")
                            .Select(x => new EmployeeListItem
                            {
                                userId = x.userId,
                                FullName = x.FullName,
                                DepartmentName = unitOfWork.Department.GetByID(x.DepartmentId).deptname,
                                BusinessUnitName = unitOfWork.BusinessUnit.GetByID(x.businessunitId).unitname
                            }).FirstOrDefault();
                        EmployeeToAttend.Add(currentEmployee);
                    }
                    //This returns the Employees for LineManager 2 to Appraise
                    if (EmployeeForL2 && (UserPrincipal.IsInRole("L1") || UserPrincipal.IsInRole("L2")))
                    {
                        var currentEmployee = unitOfWork.employees.Get(filter: x => x.userId == item.userId, includeProperties: "Department")
                        .Select(x => new EmployeeListItem
                        {
                            userId = x.userId,
                            FullName = x.FullName,
                            DepartmentName = unitOfWork.Department.GetByID(x.DepartmentId).deptname,
                            BusinessUnitName = unitOfWork.BusinessUnit.GetByID(x.businessunitId).unitname
                        }).FirstOrDefault();
                        EmployeesForL2.Add(currentEmployee);
                    }
                    //This returns the Employees for LineManager 3 to Appraise
                    if (EmployeeForL3 && (UserPrincipal.IsInRole("L1") || UserPrincipal.IsInRole("L3") || UserPrincipal.IsInRole("L2")))
                    {
                        var currentEmployee = unitOfWork.employees.Get(filter: x => x.userId == item.userId, includeProperties: "Department")
                        .Select(x => new EmployeeListItem
                        {
                            userId = x.userId,
                            FullName = x.FullName,
                            DepartmentName = unitOfWork.Department.GetByID(x.DepartmentId).deptname,
                            BusinessUnitName = unitOfWork.BusinessUnit.GetByID(x.businessunitId).unitname
                        }).FirstOrDefault();
                        EmployeesForL3.Add(currentEmployee);
                    }
                }
                return Tuple.Create(EmployeeToAttend, EmployeesForL2, EmployeesForL3);
            }
            return null;
        }

        public bool AddOrUpdateGeneralQuestion(FormCollection collection, int? id = null, GeneralQuestionViewModel model = null)
        {
            try
            {
                if (id == null)
                {
                    string UserId = HttpContext.Current.User.Identity.GetUserId();
                    IDictionary<string, object> myDictionary = new Dictionary<string, object>();
                    collection.CopyTo(myDictionary);
                    var allKeys = myDictionary.Keys;
                    var allquestions = allKeys.Where(x => x.ToLower().StartsWith("quest"));
                    var alldescriptions = allKeys.Where(x => x.ToLower().StartsWith("desc"));
                    int GroupId = 0;
                    int.TryParse(collection["group"], out GroupId);
                    //var EmployeeDetail = employeeRepo.GetByUserId(myDictionary[Employee.ElementAt(0)].ToString());
                    int departmentId = 0;
                    if (HttpContext.Current.User.IsInRole("Manager"))
                    {
                        var deptId = collection["department"].ToString();
                        int.TryParse(deptId, out departmentId);
                    }
                    if (alldescriptions.Count() != 0 && allquestions.Count() != 0)
                    {
                        for (int i = 0; i < allquestions.Count(); i++)
                        {
                            var question = myDictionary[allquestions.ElementAtOrDefault(i)].ToString();
                            var description = myDictionary[alldescriptions.ElementAtOrDefault(i)].ToString();
                            GeneralQuestion Question = new GeneralQuestion()
                            {
                                Question = question,
                                Description = description,
                                CreatedBy = UserId,
                                CreatedOn = DateTime.Now,
                                IsActive = true,
                                GroupId = GroupId,
                            };
                            if (HttpContext.Current.User.IsInRole("Manager"))
                            {
                                var userSessionObject = (SessionModel)HttpContext.Current.Session["_ResourceEdgeTeneceIdentity"];
                                if (userSessionObject != null && departmentId != 0)
                                {
                                    Question.LocationId = userSessionObject.LocationId;
                                    Question.GroupId = userSessionObject.GroupId;
                                    Question.BusinessUnitId = userSessionObject.UnitId;
                                    Question.DepartmentId = departmentId;
                                    Question.GroupId = userSessionObject.GroupId;
                                }
                            }
                            unitOfWork.GeneralQuestion.Insert(Question);
                            unitOfWork.Save();
                        }
                    }
                    return true;
                }
                else
                {
                    var existingQuestion = unitOfWork.Questions.GetByID(id);
                    if (existingQuestion != null)
                    {
                        existingQuestion.EmpQuestion = model.Question;
                        existingQuestion.Description = model.Description;
                        QuestionRepo.update(existingQuestion);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
                //return false
            }
        }

     
    }
}
using Microsoft.AspNet.Identity;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;
using resourceEdge.Domain.ViewModels;
using resourceEdge.webUi.Infrastructure.Core;
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
                model.Groups = new System.Web.Mvc.SelectList(unitOfWork.Groups.Get(filter: x=>x.Id ==  id.Value).Select(y=> new { Id = y.Id, GroupName = y.GroupName }), "Id", "GroupName" );
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
                yearArray.Add(new YearlistItem { Name =$"{currentYear +1}", value = currentYear+ 1 });
                currentYear = currentYear+1;
            }
            return yearArray;
        }
        public bool GetOpenAppraisal(int groupId)
        {
            var OpenAppraisal = unitOfWork.AppraisalInitialization.Get(x => x.GroupId == groupId && x.AppraisalStatus != 2 && x.EndDate > DateTime.Now).FirstOrDefault();
            if (OpenAppraisal != null)
            {
                return true;
            }
            return false;
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
                    //result.StartDate = DateTime.Now;
                    result.Enable = true;
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
                var ExistingAppraisal = unitOfWork.AppraisalConfiguration.Get(filter: x => x.BusinessUnitId == model.BusinessUnit && x.Code == model.Code && UserId == x.CreatedBy && x.IsActive == true).FirstOrDefault();
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
                                        && x.AppraisalInitialization.IsActive == true).Any(x=>x.IsActive ==true);

            if (subscribedAppraisal != false)
            {
                List<AppraisalInitialization> CurrentAppraisalInprogress = new List<AppraisalInitialization>();

                var userQuestions = unitOfWork.Questions.Get(filter: x => x.UserIdForQuestion == userId && x.Approved == true).ToList();
                List<Question> QuestionToRemove = new List<Question>();
                Dictionary<int,int> answers = new Dictionary<int, int>();
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
                            else if(AppraisalQuestion.IsSubmitted == true && AppraisalQuestion.L3Status == false)
                            {
                                answers.Add(item.Id, AppraisalQuestion.Answer);
                            }
                        }                     
                    }
                    if (QuestionToRemove.Count > 0)
                    {
                        QuestionToRemove.ForEach(x => userQuestions.Remove(x));
                    }
                AppraiseeDropDown dropDown = new AppraiseeDropDown()
                {
                    Questions = userQuestions,
                     PreviousAnswer = answers
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
                List<string> Questions = new List<string>();

                var userEmployeeDetails = unitOfWork.employees.Get(filter: x => x.userId == userId).FirstOrDefault();
                if (userEmployeeDetails != null)
                {
                    var appraisalConfigurationDetails = unitOfWork.AppraisalConfiguration
                        .Get(filter: x => x.LocationId == userEmployeeDetails.LocationId
                        && x.BusinessUnitId == userEmployeeDetails.businessunitId
                        && x.IsActive == true).FirstOrDefault();
                    if (appraisalConfigurationDetails != null)
                    {
                        //This check actually sets up some parameters that would be used in the assignments
                        var allKeys = model.AllKeys.ToList();
                        var allQuestionId = allKeys.Where(x => x.StartsWith("Question")).SingleOrDefault();
                        var AllQstIds = model[allQuestionId];
                        allKeys.RemoveRange(0, 2);
                        var AllQuestionId = AllQstIds.Split(',');

                        for (int i = 0; i < allKeys.Count; i++)
                        {
                            Questions.Add(model[(string)allKeys[i]]);
                        }
                        int answer = 0;
                        int question = 0;
                        for (int i = 0; i < allKeys.Count; i++)
                        {
                            int.TryParse(model[(string)allKeys[i]], out answer);
                            int.TryParse(AllQuestionId[i], out question);
                            var existingQuestion = unitOfWork.AppraisalQuestion.Get(filter: x => x.QuestionId == question && x.UserId == userId).FirstOrDefault();
                            if (existingQuestion == null)
                            {
                                //Submit new Question
                                AppQuestion = new AppraisalQuestion()
                                {
                                    Answer = answer,
                                    GroupId = userEmployeeDetails.GroupId,
                                    L1Status = null,
                                    L2Status = null,
                                    L3Status = null,
                                    LineManager1 = appraisalConfigurationDetails.LineManager1,
                                    LinrManager2 = appraisalConfigurationDetails.LineManager2,
                                    LineManager3 = appraisalConfigurationDetails.LineManager3,
                                    LocationId = userEmployeeDetails.LocationId.Value,
                                    QuestionId = question,
                                    UserId = userId,
                                    IsSubmitted = true
                                };
                                unitOfWork.AppraisalQuestion.Insert(AppQuestion);
                                unitOfWork.Save();
                            }
                            else
                            {
                            //Update Question Answer
                            existingQuestion.Answer = answer;
                            existingQuestion.L3Status = null;
                            existingQuestion.EditCount = 1;
                            unitOfWork.AppraisalQuestion.Update(existingQuestion);
                            
                            unitOfWork.Save();
                            }
                        }
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
            IList<EmployeeListItem> EmployeeList =  new List<EmployeeListItem>(); 
            if (IsLineManager() != false)
            {
                var lineManagerUnitToAppraise = unitOfWork.AppraisalConfiguration.Get(filter: x => x.LineManager1 == UserId || x.LineManager2 == UserId || x.LineManager3 == UserId).ToList().Select(x=> new { unit = x.BusinessUnitId, dept = x.DepartmentId });
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
                                                     DepartmentName =  unitOfWork.Department.GetByID(x.DepartmentId).deptname,
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
            var EmployeeQuestions = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == userId && x.IsSubmitted == true, includeProperties: "Question");
            var EditCount = EmployeeQuestions.Any(x => x.EditCount == 2);
            IList<AppraisalQuestionViewModel> EmployeeAppraisalQuestion = new List<AppraisalQuestionViewModel>();
            if (HttpContext.Current.User.IsInRole("L1"))
            {
                EmployeeAppraisalQuestion = EmployeeQuestions.Where(X => X.L1Status == null).Select(x => new AppraisalQuestionViewModel() { Question = x.Question.EmpQuestion, Answers = x.Answer, id = x.QuestionId }).ToList(); 
            }
            if (HttpContext.Current.User.IsInRole("L2"))
            {
                EmployeeAppraisalQuestion = EmployeeQuestions.Where(X => X.L2Status == null).Select(x => new AppraisalQuestionViewModel() { Question = x.Question.EmpQuestion, Answers = x.Answer, id = x.QuestionId }).ToList();
            }
            if (HttpContext.Current.User.IsInRole("L3"))
            {
                if (EditCount)
                {
                    EmployeeAppraisalQuestion =  EmployeeQuestions.Where(X => X.L3Status == null || X.L3Status == false).Select(x => new AppraisalQuestionViewModel() { Question = x.Question.EmpQuestion, Answers = x.Answer, id = x.QuestionId }).ToList();
                    var result = EmployeeAppraisalQuestion.Where(x => x.Question != null).FirstOrDefault();
                    result.EditCount = true;
                    EmployeeAppraisalQuestion.Remove(result);
                    EmployeeAppraisalQuestion.Add(result);

                    EmployeeAppraisalQuestion.ElementAt(0).EditCount = true;
                }
                else
                {
                    EmployeeAppraisalQuestion = EmployeeQuestions.Where(X => X.L3Status == null).Select(x => new AppraisalQuestionViewModel() { Question = x.Question.EmpQuestion, Answers = x.Answer, id = x.QuestionId }).ToList();

                }
            }
          
            return (IEnumerable<AppraisalQuestionViewModel>) EmployeeAppraisalQuestion ?? null;
        }

        /// <summary>
        /// This method checks the user Question and approves the question. 
        /// It also checks the current linemanager and approves the Final result, based on the lineManager involved.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="QuestionId"></param>
        /// <returns></returns>
        public bool ApproveAppraisalQuestion(string userId, int? QuestionId, FormCollection collection)
        {
            if (QuestionId != null)
            {
                var result = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == userId && x.QuestionId == QuestionId).FirstOrDefault();
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
                var result = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == userId);//.Where(x => x.L3Status != true);
                var editCount = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == userId).Any(x => x.EditCount == 2);
                Dictionary<string, int> AllModel = new Dictionary<string, int>();
                if (editCount)
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
                    if (result != null)
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
                                if (AllModel.ContainsKey(item.QuestionId.ToString()))
                                {

                                    var ans = AllModel[item.QuestionId.ToString()];
                                    item.Answer = ans;
                                }
                                }
                            }
                        }
                        result.ToList().ForEach(X => unitOfWork.AppraisalQuestion.Update(X));
                        unitOfWork.Save();
                        return true;
                    }
                }
                return false;
            }
        public bool? DenyAppraisalQuestion(string userId, int? QuestionId)
        {
            if (QuestionId != null)
            {
                var result = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == userId && x.QuestionId == QuestionId).FirstOrDefault();
                var isEditCountComplete = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == userId).Any(x => x.EditCount == 2);
                if (result != null && !isEditCountComplete)
                {
                    if (HttpContext.Current.User.IsInRole("L1") && result.EditCount == null || result.EditCount <= 2) //this checks for edits less than or equal to 3 and approves finally from the lineManager1
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
                var result = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == userId);
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
                if (DepartmentToAppraise != null || DepartmentToAppraise.Count() >0)
                {
                    //This foreach gets all the user in a current department for appraisal only
                    foreach (var item in DepartmentToAppraise)
                    {
                        var employee = unitOfWork.employees.Get(filter: x => x.businessunitId == item.businessunitId && x.DepartmentId == item.departmentId).Select(x =>new EmployeeListItem() { userId = x.userId });
                        if (employee != null)
                        {
                            AllEmployeesToAppraise = employee.ToList();
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
                       EmployeeToAppraise = EmployeeAppraisal.All(X=> X.L3Status == null && ( X.L3Status != null && X.L3Status == true) &&( X.L2Status != null && X.L2Status.Value == true));
                       EmployeeForL2 = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == item.userId).All(X => X.L3Status == true && (X.L2Status == null || X.L2Status == false));
                       EmployeeForL3 = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == item.userId).Any(X => X.L3Status == null || X.L3Status == false);
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
                    return Tuple.Create(EmployeeToAttend,EmployeesForL2, EmployeesForL3);
                }
            return null;
        }
    }
}
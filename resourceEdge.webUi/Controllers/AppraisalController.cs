using Microsoft.AspNet.Identity;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.webUi.Infrastructure;
using resourceEdge.webUi.Infrastructure.Handlers;
using resourceEdge.webUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using resourceEdge.webUi.Infrastructure.Core;
using resourceEdge.Domain.ViewModels;
using System.Collections;
using resourceEdge.webUi.Models.SystemModel;
using Fluentx;
using resourceEdge.webUi.Infrastructure.SystemManagers;
using Fluentx.Mvc;

namespace resourceEdge.webUi.Controllers
{
   [CustomAuthorizationFilter]
    public class AppraisalController : Controller
    {
        IEmployees  EmpRepo;
        IParameter ParameterRepo;
        IQuestions QuestionRepo;
        IGroups GroupRepo;
        ISkills skillRepo;
        IRating RatingRepo;
        IAppraisalMode ApraisalModeRepo;
        IAppraisalStatus AppraisalStatusRepo;
        IAppraisalRating AppraisalRatingRepo;
        IAppraisalMode AppraisalMode;
        IAppraisalConfiguration AppraisalConfigRepo;
        IAppraisalInitialization InitializtionRepo;
        IEmploymentStatus StatusRepo;
        EmployeeManager EmployeeManager;
        AppraisalManager AppraisalManager;
        DropDownManager dropDownmanager; 
        public AppraisalController(IParameter param, IQuestions questParam, ISkills skillParam, IRating RParam, IGroups GParam,
            IAppraisalMode ModeParam, IAppraisalStatus statusParams, IAppraisalRating appratingParam, IAppraisalMode AppMode,
            IAppraisalInitialization InitializtionParam, IEmploymentStatus statusParam, IEmployees EmpParam, IAppraisalConfiguration AppConfigParam
            )
        {
            ParameterRepo = param;
            QuestionRepo = questParam;
            skillRepo = skillParam;
            RatingRepo = RParam;
            GroupRepo = GParam;
            ApraisalModeRepo = ModeParam;
            AppraisalStatusRepo = statusParams;
            AppraisalRatingRepo = appratingParam;
            AppraisalMode = AppMode;
            InitializtionRepo = InitializtionParam;
            StatusRepo = statusParam;
            AppraisalConfigRepo = AppConfigParam;
            EmpRepo = EmpParam;
            EmployeeManager = new EmployeeManager(EmpParam);
            AppraisalManager = new AppraisalManager(EmpParam, AppConfigParam, questParam);
            dropDownmanager = new DropDownManager();
        }
        public ActionResult AddParameter()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddParameter(ParameterViewModel model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var parameter = new Parameters()
                    {
                        ParameterName = model.ParameterName,
                        Descriptions = model.Descriptions,
                        createdby = User.Identity.GetUserId(),
                        modifiedby = User.Identity.GetUserId(),
                        createddate = DateTime.Now,
                        modifieddate = DateTime.Now,
                        isactive = true
                    };
                    ParameterRepo.Insert(parameter);
                    ModelState.Clear();
                    return Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                throw ex;
               
            }
            this.AddNotification($"Something went wrong please try again", NotificationType.ERROR);
            return View(model);
        }

        [CustomAuthorizationFilter(Roles ="Manager"), Route("Employee/addQuestion")]
        public ActionResult AddQuestion()
        {
            var userObject = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            var users =new SelectList(EmployeeManager.GetEmployeeUnitMembers(userObject.UnitId, userObject.LocationId.Value).Select(X=> new { Text = X.FullName, Value = X.userId}), "Value", "Text","Value");
            ViewBag.Employees = users;
            ViewBag.parameter = dropDownmanager.GetParameter();
            ViewBag.PageTitle = "Add Performance Indicator";
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, CustomAuthorizationFilter(Roles = "Manager"), Route("Employee/addQuestion")]
        public ActionResult AddQuestion(FormCollection collection, string returnUrl)
        {
            var result = AppraisalManager.AddOrUpdateAppraisalQuestion(collection);
            if (result != false)
            {
                ModelState.Clear();
               this.AddNotification($"Question(s) added", NotificationType.SUCCESS);
                return RedirectToAction("AddQuestion");
            }
            this.AddNotification($"Sorry Something went wrong, Please enter the Questions again", NotificationType.ERROR);
            return RedirectToAction("AddQuestion");
        }

        public ActionResult EditQuestion(int? id)
        {
            if (id == null)
            {
                return new  HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Question = QuestionRepo.GetById(id.Value);
            if (Question == null)
            {
                return HttpNotFound();
            }
            return View(Question);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditQuestion(QuestionViewModel model)
        {
            var result = AppraisalManager.AddOrUpdateAppraisalQuestion(null,null, model);
            if (result !=false)
            {
                return View();
            }
            this.AddNotification($"Sorry Something went wrong, please try again", NotificationType.ERROR);
            return View(model);
        }

        public ActionResult AddSkills()
        {
            ViewBag.PageTitle = "Add Skills";
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddSkills(SkillViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Skills skill = new Skills()
                    {
                        SkillName = model.SkillName,
                        Description = model.Description,
                        createdby = User.Identity.GetUserId(),
                        ModifiedBy = User.Identity.GetUserId(),
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        Isactive = true,
                        Isused = true,
                    };
                    skillRepo.Insert(skill);
                    ModelState.Clear();
                    this.AddNotification($"Skill Added!", NotificationType.SUCCESS);
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
            this.AddNotification($"Could Not be saved, please ensure that that you fill all fields", NotificationType.ERROR);
            return View();
        }

        public ActionResult AddRating()
        {
            ViewBag.PageTitle = "Add Ratings";
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddRating(FormCollection collection)
        {
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
            collection.CopyTo(myDictionary);
            var aa = myDictionary.Keys.ToList();
            var allquestions = aa.Where(x => x.ToLower().StartsWith("rating")).ToList();
            try
            {
                if (allquestions.Count != 0)
                {
                    for (int i = 0; i < allquestions.Count; i++)
                    {
                        var question = myDictionary[allquestions[i]].ToString();
                        Ratings Rating = new Ratings()
                        {
                            RatingValue = i,
                            RatingText = myDictionary[allquestions[i]].ToString(),
                            CreatedBy = User.Identity.GetUserId(),
                            ModifiedBy = User.Identity.GetUserId(),
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            Isactive = true
                        };
                        RatingRepo.Insert(Rating);
                    }
                    this.AddNotification($"", NotificationType.SUCCESS);
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
         this.AddNotification($"Something went wrong. Please Contact your system Administrator", NotificationType.ERROR);
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.PageTitle = "All Initialized Appraisals";
            return View(AppraisalManager.GetAllInitialization());
        }

        [CustomAuthorizationFilter(Roles = "Head HR")]
        public ActionResult InitilizeAppraisal()
        {
            var UserFromSession = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            ViewBag.AllDropDown = AppraisalManager.InitAppraisal();
            var allYears = AppraisalManager.GenerateYearDropDown();
            SelectList Allyears = new SelectList(allYears, "value", "Name");
            ViewBag.Years =Allyears;
            ViewBag.PageTitle = "Appraisal Initialization";
             return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult InitilizeAppraisal(AppraisalInitilizationViewModel model)
        {           
            var validDate = validateDates(model.StartDate, model.DueDate);
            var UnopenAppraisal = AppraisalManager.GetOpenAppraisal(model.Group);
            var validYear = ValidateAppraisalYear(model.FromYear, model.ToYear);
            if (ModelState.IsValid)
            {
                if (validYear)
                {
                    if (UnopenAppraisal)
                    {
                        if (validDate)
                        {
                            var Code = AppraisalManager.GetInitializationcode(15, false);
                            var period = AppraisalManager.GetPeriodByName(model.Period);
                            var result = AppraisalManager.AddAppraisalInitialization(model, Code, period.Id);
                            if (result)
                            {
                                ModelState.Clear();
                                //AppraisalManager.AddInitializationToMail(model.Group, model.StartDate);
                                this.AddNotification($"Appraisal Initilzated", NotificationType.SUCCESS);
                                return RedirectToAction("Index");
                            }
                            this.AddNotification("Oops, Something went wrong, please make sure all required fields are filled and try again", NotificationType.ERROR);
                            return RedirectToAction("InitilizeAppraisal");
                           // InitializtionRepo.Insert(initilize);

                        }
                        this.AddNotification("Oops, please Due Date has to be greater than start date and start date can't be less than or equal to  Today", NotificationType.ERROR);
                        return RedirectToAction("InitilizeAppraisal");
                    }
                    this.AddNotification("Oops, Sorry but there is an uncompleted initialized appriasal running in the system. please wait till this group has finished the current appriasal", NotificationType.ERROR);
                    return RedirectToAction("InitilizeAppraisal");
                }
                this.AddNotification("Oops, please you can only initialize to a current year or one year ahead", NotificationType.ERROR);
                return RedirectToAction("InitilizeAppraisal");
            }
            this.AddNotification("Oops, appraisal could not be added, please make sure all fields are filled or contact your system administrator", NotificationType.ERROR);
            return RedirectToAction("InitilizeAppraisal");
        }

        public bool validateDates(DateTime? startDate, DateTime? stopDate)
        {
            if (startDate != null)
            {
                if (stopDate != null)
                {
                    if (startDate > stopDate || (startDate < DateTime.Now))
                    {
                        return false;
                    }
                    else if (startDate < stopDate)
                    {
                        return true;
                    }
                }
                return false;
            }
            return false;
        }
        public bool ValidateAppraisalYear(int year1, int year2)
        {
            if (year1 > year2) return false;
            else if (year1 > DateTime.Now.Year) return false;
            else if (year2 > (DateTime.Now.Year + 1)) return false;
            return true;
        }
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult EnableAppraisal(int Id)
        {
            bool result = AppraisalManager.EnableAppraisal(Id);
            if (result != false)
            { 
                this.AddNotification($"Enabled the Appraisal, Please remember to Monitor while it is on-going", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
            this.AddNotification($"Something went wrong, please Try enabling again and please make sure you are not editing the request", NotificationType.ERROR);
            return RedirectToAction("Index");
        }

        [CustomAuthorizationFilter(Roles ="HR")]
        public ActionResult SubscribeToAppraisal()
        {
            var userSessionObject = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            ViewBag.PageTitle = "Subscribe";
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult SubscribeToAppraisal(string code)
        {
          var userSessionObject = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            if (userSessionObject != null)
            {
                var result = AppraisalManager.SubscribeForAppraisal(code, userSessionObject.LocationId.Value, User.Identity.GetUserId());
                if (result.HasValue)
                {
                    if (result.Value)
                    {
                    this.AddNotification("Subscribed for appraisal", NotificationType.SUCCESS);
                    return  RedirectToAction("ConfigureAppraisal");
                    }
                    this.AddNotification("Could not subscribe for appraisal, please make sure your subscription code is right and if problem continues please see your Head Hr", NotificationType.ERROR);
                    return RedirectToAction("SubscribeToAppraisal");
                }
                this.AddNotification("You are already subscribed to this process", NotificationType.ERROR);
                return RedirectToAction("ConfigureAppraisal");

            }
            this.AddNotification("Could not subscribe for this Appraisal. please see the Head HR", NotificationType.ERROR);
            return RedirectToAction("SubscribeToAppraisal");
        }


        [CustomAuthorizationFilter(Roles = "HR"), SubscriptionFilter]
        public ActionResult ConfigureAppraisal()
        {
            var userSessionObject = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            ViewBag.groupId = userSessionObject.GroupId.Value;
            ViewBag.dropDowns = AppraisalManager.ConfigureAppraisal(userSessionObject.LocationId.Value);
            ViewBag.PageTitle = "Configure Appraisal";
            return View();
        }

        [SubscriptionFilter]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ConfigureAppraisal(AppraisalConfigratuionViewModel model)
        {
            var userSessionObject = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            var validCode = AppraisalManager.ValidateAppraisalCode(model.Code);
            var validDepartment = AppraisalManager.validateDepartmentForAppraisal(null, userSessionObject.LocationId.Value, model.Code, model.BusinessUnit, model.Department);
            if (!validDepartment)
            {
                this.AddNotification("Please the Department has already been configured, try configuring another. if problem persist, contact your system administrator", NotificationType.ERROR);
                return RedirectToAction("ConfigureAppraisal");
            }
            if (!validCode)
            {
                this.AddNotification("The Subscription Code is invalid, please try again with a valid code. if problem persist, contact your system administrator", NotificationType.ERROR);
                return RedirectToAction("ConfigureAppraisal");
            }
            if (ModelState.IsValid)
            {

                var result = AppraisalManager.AddOrUpdateAppraisalConfiguration(model,User.Identity.GetUserId(), userSessionObject.GroupId.Value, userSessionObject.LocationId.Value);
                if (result)
                {
                    this.AddNotification("Appraisal Configured Successfully", NotificationType.SUCCESS);
                    return RedirectToAction("ConfigureAppraisal");
                }        
            }
            this.AddNotification("Oops Appraisal not configured, please make sure you fill the right details for all given fields and try again", NotificationType.ERROR);
            return RedirectToAction("ConfigureAppraisal");
        }

        [CustomAuthorizationFilter]
        public ActionResult EmployeeAppraisal()
        {
            var userSessionObject = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            ViewBag.PageTitle = $"Appraisal for {userSessionObject.FullName.ToUpper()}";
            var IsEligible = AppraisalManager.EligibleDepartmentForAppraisal(userSessionObject.UnitId, userSessionObject.DepartmentId);
            if (!IsEligible)
            {
                this.AddNotification("Sorry your HR hasn't configured appraisal for your department, Kindly see Him/Her for complain", NotificationType.ERROR);
                return RedirectToAction("Leave", "SelfService");
            }
            var Question = AppraisalManager.GenerateAppraisalQuestions(User.Identity.GetUserId(), userSessionObject.GroupId.Value, userSessionObject.LocationId.Value);
            if (Question != null)
            {
             return View(Question);
            }
            this.AddNotification("Sorry you can't perform this appraisal process Now", NotificationType.ERROR);
            return RedirectToAction("Leave", "SelfService");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EmployeeAppraisal(FormCollection model, AppraiseeDropDown dropDown)
        {
            var result = AppraisalManager.AddOrUpdateAppraisalQuestion(model, User.Identity.GetUserId());
            if (result != false)
            {
                this.AddNotification($"Thanks, Operation Successful!", NotificationType.SUCCESS);
                return RedirectToAction("Leave", "SelfService");
            }
            return RedirectToAction("SubmittedAppraisal");
        }


        [HttpPost, CustomAuthorizationFilter(Roles = "L1,L2,L3")]
        public ActionResult SubmittedAppraisal(string key)
        {
            key = formatUserId(key);
            ViewBag.userId = key;
            ViewBag.PageTitle = EmployeeManager.GetEmployeeByUserId(key).FullName + " Appraisal";
            var result = AppraisalManager.ViewSubmittedEmployeeAppraisal(key);
            return View("SubmittedAppraisal",result);
        }

        [CustomAuthorizationFilter(Roles = "L1,L2,L3")]
        public ActionResult AppraisalForApproval()
        {
            ViewBag.PageTitle = "Appraisal Management";
            var result = AppraisalManager.GetLineManagerViews();
            ViewBag.Result = result;
            if (result.Item1 != null && result.Item2 != null && result.Item3 != null && User.IsInRole("L1"))
                return View("LineManager1");
            else if (result.Item1.Count == 0 && (result.Item2.Count > 0 || result.Item2.Count == 0) && (result.Item3.Count > 0 || result.Item3.Count == 0) && User.IsInRole("L2"))          
                return View("LineManager2");
            else if (result.Item1.Count == 0 && result.Item2.Count == 0 && (result.Item3.Count> 0 || result.Item3.Count == 0) && User.IsInRole("L3"))
                return View("LineManager3");
            return View("");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ApproveSubmittedAppraisalQuestion(string key, int? QuestionId, int? type, FormCollection modelCollection)
        {
            var result = AppraisalManager.ApproveAppraisalQuestion(key, QuestionId, type, modelCollection);
            var objectToPost =new { key = key };
            Dictionary<string, object> PostData = new Dictionary<string, object>();
            PostData.Add("key", objectToPost);
            return new RedirectAndPostActionResult("/appraisal/SubmittedAppraisal", PostData);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DenySubmittedAppraisalQuestion(string key, int? QuestionId, int? type)
        {
            var result = AppraisalManager.DenyAppraisalQuestion(key, QuestionId, type);
            if (result == null)
            {
                this.AddNotification("Sorry but  this appraisal can't be edited anymore. the appraisee has exceeded his Edit limit.", NotificationType.INFO);
            }
            Dictionary<string, object> userIdToSend = new Dictionary<string, object>();
            var useridObject = new 
            {
                 key = key
            };
            userIdToSend.Add("key", useridObject);
            return new  Fluentx.Mvc.RedirectAndPostActionResult("/appraisal/SubmittedAppraisal", userIdToSend);
        }

        [Route("AddGeneralQuestion"), CustomAuthorizationFilter(Roles = "Manager,Head HR")]
        public ActionResult AddGeneralQuestion()
        {
            
            ViewBag.group = dropDownmanager.GetGroup();
            if (User.IsInRole("Manager"))
            {
                var UserFromSession = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
                ViewBag.department = dropDownmanager.GetDepartmentByUnit(UserFromSession.UnitId);
            }
            ViewBag.PageTitle = "Add Question(s)";
            return View("AddGeneralQuestion");
        }
        [HttpPost, ValidateAntiForgeryToken, CustomAuthorizationFilter(Roles = "Manager,Head HR")]
        public ActionResult AddGeneralGroupQuestion(FormCollection Collection)
        {
            var result = AppraisalManager.AddOrUpdateGeneralQuestion(Collection);
            if (result)
            {
                this.AddNotification("Question(s) Added!", NotificationType.SUCCESS);
                return RedirectToAction("AddGeneralQuestion");
            }
            this.AddNotification("Oops! Something went wrong, please try again", NotificationType.ERROR);
            return RedirectToAction("AddGeneralQuestion");
        }

        public ActionResult MyAppraisal()
        {
            ViewBag.PageTitle = "Submitted Performanace indicator";
            var result = AppraisalManager.ViewSubmittedEmployeeAppraisal(User.Identity.GetUserId());
            ViewBag.Personal = true;
            return View("SubmittedAppraisal", result);
        }

        [CustomAuthorizationFilter(Roles = "L1,L2,L3")]
        public ActionResult EmployeesToAppraise()
        {
            var result = AppraisalManager.GetAllEmployeeForLineManagerToAppraise();
            ViewBag.PageTitle = "All Employee To Appraise";
            ViewBag.AllEmployees = result;
            return View(result);
        }


        public string formatUserId(string Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Id);
            builder.Replace("{", string.Empty);
            builder.Replace("}", string.Empty);
            builder.Replace(" ", string.Empty);
            builder.Replace("key=", string.Empty);
            string result = builder.ToString();
            return result;
        }
    }
}
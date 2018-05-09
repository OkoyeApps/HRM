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

        [CustomAuthorizationFilter(Roles ="Manager")]
        public ActionResult AddQuestion()
        {
            var userObject = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            var users =new SelectList(EmployeeManager.GetEmployeeUnitMembers(userObject.UnitId, userObject.LocationId).Select(X=> new { Text = X.FullName, Value = X.userId}), "Value", "Text","Value");
            ViewBag.Employees = users;
            ViewBag.parameter = dropDownmanager.GetParameter();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
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
            var Code =AppraisalManager.GetInitializationcode(15, false);
            var period = AppraisalManager.GetPeriodByName(model.Period);
            var validDate = validateDates(model.StartDate, model.DueDate);
            var openAppraisal = AppraisalManager.GetOpenAppraisal(model.Group);
            if (ModelState.IsValid)
            {
                if (openAppraisal)
                {
                    if (validDate)
                    {
                        AppraisalInitialization initilize = new AppraisalInitialization()
                        {
                            GroupId = model.Group,
                            AppraisalMode = model.AppraisalMode,
                            AppraisalStatus = model.AppraisalStatus,
                            StartDate = model.StartDate,
                            EndDate = model.DueDate,
                            FromYear = model.FromYear,
                            InitilizationCode = Code,
                            Period = period.Id,
                            RatingType = model.RatingType,
                            ToYear = model.ToYear,
                            CreatedBy = User.Identity.GetUserId(),
                            ModifiedBy = User.Identity.GetUserId(),
                            CreatedDate = DateTime.Now
                        };
                        InitializtionRepo.Insert(initilize);
                        ModelState.Clear();
                        //AppraisalManager.AddInitializationToMail(model.Group, model.StartDate);
                        this.AddNotification($"Appraisal Initilzated", NotificationType.SUCCESS);
                        return RedirectToAction("Index");
                    }
                this.AddNotification("Oops, please Due Date has to be greater than start date", NotificationType.ERROR);
                return RedirectToAction("InitilizeAppraisal");
                }
                this.AddNotification("Oops, Sorry but there is an uncompleted initialized appriasal running in the system. please wait till this group has finished the current appriasal", NotificationType.ERROR);
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
        public ActionResult SubscribeToAppraisal(string Code)
        {
            var userSessionObject = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult SubscribeToAppraisal(string code, string userId)
        {
          var userSessionObject = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            if (userSessionObject != null)
            {
                var result = AppraisalManager.SubscribeForAppraisal(code, userSessionObject.LocationId, User.Identity.GetUserId());
                if (result != false)
                {
                    this.AddNotification($"Subscribed for appraisal", NotificationType.SUCCESS);
                  return  RedirectToAction("ConfigureAppraisal");
                }
            }
            this.AddNotification("Could not subscribed for this Appraisal. please see the Head HR", NotificationType.ERROR);
            return View();
        }


        [CustomAuthorizationFilter(Roles = "HR"), SubscriptionFilter]
        public ActionResult ConfigureAppraisal()
        {
            var userSessionObject = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            ViewBag.groupId = userSessionObject.GroupId;
            ViewBag.dropDowns = AppraisalManager.ConfigureAppraisal(userSessionObject.LocationId);
            ViewBag.PageTitle = "Configure Appraisal";
            return View();
        }

        [SubscriptionFilter]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ConfigureAppraisal(AppraisalConfigratuionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userSessionObject = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
                var result = AppraisalManager.AddOrUpdateAppraisalConfiguration(model,User.Identity.GetUserId(), userSessionObject.GroupId, userSessionObject.LocationId);
                this.AddNotification($"", NotificationType.SUCCESS);
                return RedirectToAction("ConfigureAppraisal");
            }
            this.AddNotification($"Something went wrong, Appraisal not configured", NotificationType.ERROR);
            return RedirectToAction("ConfigureAppraisal");
        }

        [CustomAuthorizationFilter]
        public ActionResult EmployeeAppraisal()
        {
            var userSessionObject = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            ViewBag.PageTitle = $"Appraisal for {userSessionObject.FullName.ToUpper()}";
            var Question = AppraisalManager.GenerateAppraisalQuestions(User.Identity.GetUserId(), userSessionObject.GroupId, userSessionObject.LocationId);
            if (Question != null)
            {
             return View(Question);
            }
            this.AddNotification($"Sorry you can't perform this appraisal process Now", NotificationType.ERROR);
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
            return RedirectToAction("EmployeeAppraisal");
        }

        [CustomAuthorizationFilter(Roles = "L1,L2,L3")]
        public ActionResult EmployeesToAppraise()
        {
            var result = AppraisalManager.GetAllEmployeeForLineManagerToAppraise();
            ViewBag.PageTitle = "All Employee To Appraise";
            ViewBag.AllEmployees = result;
            return View(result);
        }

        [HttpPost, CustomAuthorizationFilter(Roles = "L1,L2,L3")]
        public ActionResult SubmittedAppraisal(string userId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(userId);
            builder.Replace("{", string.Empty);
            builder.Replace("}", string.Empty);
            builder.Replace(" ", string.Empty);
            builder.Replace("userId=", string.Empty);
            userId = builder.ToString();
            ViewBag.userId = userId;
            ViewBag.PageTitle = EmployeeManager.GetEmployeeByUserId(userId).FullName + " Appraisal";
            var result = AppraisalManager.ViewSubmittedEmployeeAppraisal(userId);
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
        public ActionResult ApproveSubmittedAppraisalQuestion(string userId, int? QuestionId, FormCollection modelCollection)
        {
            var result = AppraisalManager.ApproveAppraisalQuestion(userId, QuestionId, modelCollection);
            return RedirectToAction("EmployeesToAppraise");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DenySubmittedAppraisalQuestion(string userId, int? QuestionId)
        {
            var result = AppraisalManager.DenyAppraisalQuestion(userId, QuestionId);
            if (result == null)
            {
                this.AddNotification("Sorry but you this appraisal can't be edited anymore. the appraisee has exceeded his Edit limit.", NotificationType.INFO);
            }
            Dictionary<string, object> userIdToSend = new Dictionary<string, object>();
            var useridObject = new 
            {
                 userId = userId
            };
            userIdToSend.Add("userId", useridObject);
            return new  Fluentx.Mvc.RedirectAndPostActionResult("/appraisal/SubmittedAppraisal", userIdToSend);
        }
    }
}
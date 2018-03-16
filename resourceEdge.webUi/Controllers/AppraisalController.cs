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

namespace resourceEdge.webUi.Controllers
{
    [Authorize]
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
            ModelState.AddModelError("", "Please try again");
            this.AddNotification("Something went wrong please try again", NotificationType.ERROR);
            return View(model);
        }

        [Authorize(Roles ="Manager")]
        public ActionResult AddQuestion()
        {
            var userObject = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            var users =new SelectList(EmployeeManager.GetEmployeeUnitMembers(userObject.UnitId, userObject.LocationId).Select(X=> new { Text = X.FullName, Value = X.userId}), "Value", "Text","Value");
            ViewBag.Employees = users;
            ViewBag.parameter = new SelectList(ParameterRepo.Get().OrderBy(x => x.Id), "Id", "ParameterName", "Id");
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddQuestion(FormCollection collection, string returnUrl)
        {
            var result = AppraisalManager.AddOrUpdateAppraisalQuestion(collection);
            if (result != false)
            {
                ModelState.Clear();
               this.AddNotification("Question(s) added Sucessfully", NotificationType.SUCCESS);
                return RedirectToAction("AddQuestion");
            }
            this.AddNotification("Sorry Something went wrong, Please enter the Questions again", NotificationType.ERROR);
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
            this.AddNotification("Sorry Something went wrong, please try again", NotificationType.ERROR);
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
                    this.AddNotification("Skill Added Successfully", NotificationType.SUCCESS);
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ModelState.AddModelError("", "Could Not be saved, please ensure that that you fill all fields");
            this.AddNotification("Could Not be saved, please ensure that that you fill all fields", NotificationType.ERROR);
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
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ViewBag.Error = "Something went wrong. Please Contact your system Administrator";
            return View();
        }

        public ActionResult AllInitializedAppraisal()
        {
            ViewBag.PageTitle = "All Initialized Aappraisals";
            return View(AppraisalManager.GetAllInitialization());
        }

        public ActionResult InitilizeAppraisal()
        {
            ViewBag.AllDropDown = AppraisalManager.InitAppraisal();
            ViewBag.PageTitle = "Appraisal Initialization";
             return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult InitilizeAppraisal(AppraisalInitilizationViewModel model)
        {
            var Code =AppraisalManager.GetInitializationcode(15, false);
            var period = AppraisalManager.GetPeriodByName(model.Period);
            if (ModelState.IsValid)
            {
                AppraisalInitialization initilize = new AppraisalInitialization()
                {
                    GroupId = model.Group,
                    AppraisalMode = model.AppraisalMode,
                    AppraisalStatus = model.AppraisalStatus,
                    StartDate = model.StartDate,
                    EndDate = model.DueDate,
                    FromYear = model.FromYear.Year,
                    InitilizationCode = Code,
                    Period = period.Id,
                    RatingType = model.RatingType,
                    ToYear = model.ToYear.Year,
                    CreatedBy = User.Identity.GetUserId(),
                    ModifiedBy = User.Identity.GetUserId(),
                    CreatedDate = DateTime.Now
                };
                InitializtionRepo.Insert(initilize);
                ModelState.Clear();
                //AppraisalManager.AddInitializationToMail(model.Group, model.StartDate);
                ViewBag.Success = "Appraisal Initilzation Successful";
                return RedirectToAction("AllInitializedAppraisal");
            }
            return RedirectToAction("InitilizeAppraisal");
        }
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult EnableAppraisal(int Id)
        {
            bool result = AppraisalManager.EnableAppraisal(Id);
            if (result != false)
            {
                
                ViewBag.Success = "Sucessfuly Enabled the Appraisal, Please wait while it is on-going";
                return View("AllInitializedAppraisal");
            }
            ViewBag.Error = "Something went wrong, please Try enabling again";
            return View("AllInitializedAppraisal");
        }

        [Authorize(Roles ="HR")]
        public ActionResult SubscribeToAppraisal(string Code)
        {
            var userSessionObject = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            //if (Code != null)
            //{
            //    var result = AppraisalManager.SubscribeForAppraisal(Code, userSessionObject.LocationId,User.Identity.GetUserId());
            //    if (result != false)
            //    {
            //        this.AddNotification("Subscription Successful", NotificationType.SUCCESS);
            //        return View(""); //This shoould redirect to viewQuestions of the Hr controller
            //    }
            //}
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
                    this.AddNotification("Successfully subscribed for appraisal", NotificationType.SUCCESS);
                  return  RedirectToAction("ConfigureAppraisal");
                }
            }
            ModelState.AddModelError("", "Could not subscribed for this Appraisal. please see the Head HR");
            return View();
        }


        [Authorize(Roles = "HR")]
        [SubscriptionFilter]
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
                this.AddNotification("Appraisal Configuration successful", NotificationType.SUCCESS);
                return RedirectToAction("ConfigureAppraisal");
            }
            this.AddNotification("Something went wrong, Appraisal not configured", NotificationType.ERROR);
            return RedirectToAction("ConfigureAppraisal");
        }

        [Authorize]
        public ActionResult EmployeeAppraisal()
        {
            var userSessionObject = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            ViewBag.PageTitle = $"Appraisal for {userSessionObject.FullName.ToUpper()}";
            var Question = AppraisalManager.GenerateAppraisalQuestions(User.Identity.GetUserId(), userSessionObject.GroupId, userSessionObject.LocationId);
            if (Question != null)
            {
             return View(Question);
            }
            this.AddNotification("Sorry you can't perform this appraisal process now", NotificationType.ERROR);
            return RedirectToAction("Leave", "SelfService");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EmployeeAppraisal(FormCollection model)
        {
            var result = AppraisalManager.AddOrUpdateAppraisalQuestion(model, User.Identity.GetUserId());
            if (result != false)
            {
                this.AddNotification("Thanks, Operation Successful!", NotificationType.SUCCESS);
                return RedirectToAction("Leave", "SelfService");
            }
            return RedirectToAction("EmployeeAppraisal");
        }
    }
}
using Microsoft.AspNet.Identity;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.webUi.Infrastructure;
using resourceEdge.webUi.Infrastructure.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Controllers
{
    [EdgeIdentityFilter]
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
            AppraisalManager = new AppraisalManager(EmpParam, AppConfigParam);
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
            return View(model);
        }

        public ActionResult AddQuestion()
        {
            ViewBag.parameter = new SelectList(ParameterRepo.Get().OrderBy(x => x.Id), "Id", "ParameterName", "Id");
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddQuestion(FormCollection collection, string returnUrl)
        {
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
            collection.CopyTo(myDictionary);
            var allKeys = myDictionary.Keys.ToList();
            var allquestions = allKeys.Where(x => x.ToLower().StartsWith("quest")).ToList();
            var alldescriptions = allKeys.Where(x => x.ToLower().StartsWith("desc")).ToList();
            if (alldescriptions.Count != 0 && allquestions.Count != 0)
            {
                for (int i = 0; i < allquestions.Count; i++)
                {
                    var question = myDictionary[allquestions[i]].ToString();
                    var description = myDictionary[alldescriptions[i]].ToString();
                    Questions Question = new Questions()
                    {
                        Question = question,
                        Description = description,
                        Createdby = User.Identity.GetUserId(),
                        ModifiedBy = User.Identity.GetUserId(),
                        ModifiedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        PaConfiguredId = User.Identity.GetUserId(),
                        Isactive = true
                    };
                    QuestionRepo.Insert(Question);
                }
                return Redirect(returnUrl);
            }

            ViewBag.Error = "Sorry Something went wrong, Please enter the Questions again";
            return RedirectToAction("AddQuestion");
        }

        public ActionResult AddSkills()
        {
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
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ModelState.AddModelError("", "Could Not be saved, please ensure that that you fill all fields");
            return View();
        }

        public ActionResult AddRating()
        {
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
            return View(AppraisalManager.GetAllInitialization());
        }

        public ActionResult InitilizeAppraisal()
        {    
            ViewBag.Groups = new SelectList(GroupRepo.Get().OrderBy(x => x.GroupName), "Id", "GroupName", "Id");
            ViewBag.RatingType = new SelectList(AppraisalRatingRepo.Get().OrderBy(x => x.Name), "Id", "Name", "Id");
            ViewBag.appStatus = new SelectList(AppraisalStatusRepo.Get().OrderBy(x => x.Name), "Id", "Name", "Id");
            ViewBag.appMode = new SelectList(AppraisalMode.Get().OrderBy(X => X.Name), "Id", "Name", "Id");
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult InitilizeAppraisal(AppraisalInitilizationViewModel model)
        {
            var aa =AppraisalManager.InitializationCodeGeneration(10, false);
            var period = AppraisalManager.GetPeriodByName(model.Period);
            if (ModelState.IsValid)
            {
                AppraisalInitialization initilize = new AppraisalInitialization()
                {
                    GroupId = model.Group,
                    AppraisalMode = model.AppraisalMode,
                    AppraisalStatus = model.AppraisalStatus,
                    DueDate = model.DueDate,
                    FromYear = model.FromYear.Year,
                    InitilizationCode = aa,
                    Period = period.Id,
                    RatingType = model.RatingType,
                    ToYear = model.ToYear.Year,
                    CreatedBy = User.Identity.GetUserId(),
                    ModifiedBy = User.Identity.GetUserId(),
                    CreatedDate = DateTime.Now
                };
                InitializtionRepo.Insert(initilize);
                ModelState.Clear();
                ViewBag.Success = "Appraisal Initilzation Successful";
                return RedirectToAction("AllInitializedAppraisal");
            }
            return RedirectToAction("InitilizeAppraisal");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnableAppraisal(int InitilizationId)
        {
            bool result = AppraisalManager.EnableAppraisal(InitilizationId);
            if (result != false)
            {
                ViewBag.Success = "Sucessfuly Enabled the Appraisal, Please wait while it is on-going";
                return View("AllInitializedAppraisal");
            }
            ViewBag.Error = "Something went wrong, please Try enabling again";
            return View("AllInitializedAppraisal");
        }

        public ActionResult SubscribeToAppraisal(string Code)
        {
            if (Code != null)
            {
                var result = AppraisalManager.SubscribeToAppraisal(Code, User.Identity.GetUserId());
                if (result != false)
                {
                    return View("");
                }
            }
            return View();
        }
       
       // [Authorize/*(Roles = "System Admin, HR")*/]
        [EdgeIdentityFilter]
        public ActionResult ConfigureAppraisal()
        {
            
            ViewBag.BusinessUnit = new SelectList(AppraisalManager.GetBusinessUnitsByLocation(User.Identity.GetUserId()), "BusId", "unitname", "BusId");
            ViewBag.AppraisalStatus = new SelectList(AppraisalStatusRepo.Get().OrderBy(x => x.Name).ToList(), "Id", "Name", "Id");
            ViewBag.Eligibility = new SelectList(StatusRepo.Get().OrderBy(X => X.employemntStatus).Select(x =>new { Text = x.employemntStatus, Value = x.empstId }).ToList(), "Value", "Text", "Value");
            ViewBag.parameter = new SelectList(ParameterRepo.Get().OrderBy(x => x.ParameterName).Select(x => new  { Text = x.ParameterName, Value = x.Id}), "Value", "Text", "Value");
            return View("FormWizard");
        }

        [SubscriptionFilter]
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult ConfigureAppraisal(FormCollection model)
        {
            if (ModelState.IsValid)
            {
                //var result = AppraisalManager.AddOrUpdateAppraisalConfiguration(model, User.Identity.GetUserId());
                //if (result != false)
                //{
                //    ModelState.Clear();
                //    ViewBag.Success = "Appraisal Configured Successfully, Please Add line Managers";
                //    return Json(new { success = "True" }, JsonRequestBehavior.AllowGet);
                //}
            }
            ViewBag.Error = "Something went wrong, Appraisal not configured";
            return Json(new { message = "False" });
        }
    }
}
using Microsoft.AspNet.Identity;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Controllers
{
    public class AppraisalController : Controller
    {
        IParameter ParameterRepo;
        IQuestions QuestionRepo;
        ISkills skillRepo;
        IRating RatingRepo;
        IGroups GroupRepo;
        public AppraisalController(IParameter param, IQuestions questParam, ISkills skillParam, IRating RParam, IGroups GParam)
        {
            ParameterRepo = param;
            QuestionRepo = questParam;
            skillRepo = skillParam;
            RatingRepo = RParam;
            GroupRepo = GParam;
        }
        public ActionResult AddParameter()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
            //ViewBag.parameter = new SelectList(ParameterRepo.Get().OrderBy(x => x.Id), "Id", "ParameterName", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddQuestion(FormCollection collection, string returnUrl)
        {
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
            collection.CopyTo(myDictionary);
            var aa = myDictionary.Keys.ToList();
            var allquestions = aa.Where(x => x.ToLower().StartsWith("quest")).ToList();
            var alldescriptions = aa.Where(x => x.ToLower().StartsWith("desc")).ToList();
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
            return View();
        }

        public ActionResult AddSkills()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                        AppraisalRating Rating = new AppraisalRating()
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

        public ActionResult InitilizeAppraisal()
        {
            ViewBag.Groups = new SelectList(GroupRepo.Get().OrderBy(x => x.GroupName), "Id", "GroupName", "Id");
            //ViewBag.Eligibility = 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InitilizeAppraisal(AppraisalInitilizationViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }
    }
}
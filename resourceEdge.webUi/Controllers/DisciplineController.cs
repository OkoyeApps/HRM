using resourceEdge.Domain.Entities;
using resourceEdge.webUi.Infrastructure.Core;
using resourceEdge.webUi.Infrastructure.Handlers;
using resourceEdge.webUi.Infrastructure.SystemManagers;
using resourceEdge.webUi.Models;
using resourceEdge.webUi.Models.SystemModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Controllers
{
  
    public class DisciplineController : Controller
    {
        DisciplinaryManager disciplineManager;

        public DisciplineController(DisciplinaryManager disciplineParam)
        {
            disciplineManager = disciplineParam;
        }

        // GET: Discipline
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult allViolation()
        {
            ViewBag.PageTitle = "All Violations";
            return View(disciplineManager.AllViolation());
        }
        public ActionResult allConsequence()
        {
            ViewBag.PageTitle = "All Consequences";
            return View(disciplineManager.AllConsequence());
        }
        public ActionResult AddViolation()
        {
            ViewBag.PageTitle = "Add Violation";
            return View();
        }
        [CustomAuthorizationFilter(Roles = "HR, System Admin")]
        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult AddViolation(FormCollection collection)
        {
            var result = disciplineManager.AddOrUpdateViolation(collection);
            if (result)
            {
                this.AddNotification("Violation added!", NotificationType.SUCCESS);
                return RedirectToAction("allViolation");
            }
            this.AddNotification("Oops violation(s) could not be added, please try again and if failure persists, please contact your system administrator", NotificationType.ERROR);
            return View(collection);
        }
        [CustomAuthorizationFilter(Roles = "HR, System Admin")]
        public ActionResult EditViolation(int id)
        {
            ViewBag.PageTitle = "Update Violations";
            var result = disciplineManager.GetViolationById(id);
            if (result != null)
            {
            return View(result);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditViolation(Violation model)
        {
            var result = disciplineManager.AddOrUpdateViolation(null, model, model.ID);
            if (result)
            {
                this.AddNotification("Violation Updated!", NotificationType.SUCCESS);
                return RedirectToAction("allViolation");
            }
            this.AddNotification("Oops violation could not be Updated, please try again and if failure persists, please contact your system administrator", NotificationType.ERROR);
            return View(model);
        }
        [CustomAuthorizationFilter(Roles = "HR, System Admin")]
        public ActionResult AddConsequence()
        {
            ViewBag.PageTitle = "Add Consequence";
            return View();
        }
        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult AddConsequence(FormCollection collection)
        {
            var result = disciplineManager.AddOrUpdateConsequences(collection);
            if (result)
            {
                this.AddNotification("Consequence(s) added!", NotificationType.SUCCESS);
                return RedirectToAction("allConsequence");
            }
            this.AddNotification("Oops Consequence(s) could not be added, please try again and if failure persists, please contact your system administrator", NotificationType.ERROR);
            return View(collection);
        }
        [CustomAuthorizationFilter(Roles = "HR, System Admin")]
        public ActionResult EditConsequence(int id)
        {
            ViewBag.PageTitle = "Update Consequence";
            var result = disciplineManager.GetConsequenceById(id);
            if (result != null)
            {
                return View(result);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditConsequence(Consequence model)
        {
            var result = disciplineManager.AddOrUpdateConsequences(null, model, model.ID);
            if (result)
            {
                this.AddNotification("Consequence Updated!", NotificationType.SUCCESS);
                return RedirectToAction("allConsequence");
            }
            this.AddNotification("Oops Consequence could not be Updated, please try again and if failure persists, please contact your system administrator", NotificationType.ERROR);
            return View(model);
        }

        [CustomAuthorizationFilter(Roles = "HR, System Admin")]
        public ActionResult AllIncident()
        {
            ViewBag.PageTitle = "All RaisedIncident(s)";
            var userDetails = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            var result = disciplineManager.AllIncident(userDetails.GroupId, userDetails.LocationId);
            return View(result);
        }
        [CustomAuthorizationFilter(Roles = "HR, System Admin")]
        public ActionResult RaiseIncident()
        {
            var userDetails = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            DropDownManager dropManager = new DropDownManager();
            DisciplinaryIncidentViewModel model = new DisciplinaryIncidentViewModel
            {
                Consequences = dropManager.GetConsequence(),
                Units = dropManager.GetBusinessUnit(userDetails.GroupId, userDetails.LocationId),
                Violation = dropManager.GetViolation(),
            };
           
            
            ViewBag.PageTitle = "Raise Incident";
            return View(model);
        }
        [CustomAuthorizationFilter(Roles = "HR, System Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult RaiseIncident(DisciplinaryIncidentViewModel model)
        {
            if (model.DateOfOccurence > DateTime.Now)
            {
                this.AddNotification("Please Date of occurence must not be a future date, please edit and try again", NotificationType.ERROR);
                 return RedirectToAction("RaiseIncident");
            }
            if (model.ExpiryDate.Date < DateTime.Now.Date || model.ExpiryDate.Date == DateTime.Now.Date)
            {
                this.AddNotification("Please an offender has to be given atleast 24 hrs to respond, Edit the Expiry date and try again", NotificationType.ERROR);
                return RedirectToAction("RaiseIncident");
            }
            var result = disciplineManager.AddOrUpdateDisciplineIncident(model,null);
            if (result)
            {
                this.AddNotification("Operation Successful", NotificationType.SUCCESS);
                return RedirectToAction("AllIncident");
            }
            this.AddNotification("Oops Incident could not be Raised, please try again and if failure persists, please contact your system administrator", NotificationType.ERROR);
            return RedirectToAction("RaiseIncident");
        }
    
        public ActionResult IncidentDetail(int id)
        {
            var userDetails = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            ViewBag.PageTitle = "Details";
            var result = disciplineManager.IncidentDetail(userDetails.GroupId, userDetails.LocationId, id);
            if (result != null)
            {
                return View(result);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
        public ActionResult EditIncident(int id)
        {
           
            var result = disciplineManager.GetIncidentById(id);
            if (result != null)
            {
                ViewBag.pageTitle = "Disciplinary Response";
                return View(result);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditIncident(DisciplineListItem model)
        {

            //var Update = new DisciplinaryIncidentViewModel
            //{
            //    EmployeeStatement = model.Response
            //};
            var result = disciplineManager.AddOrUpdateDisciplineIncident(null, model, model.ID);
            if (result)
            {
                this.AddNotification("Statement updated", NotificationType.SUCCESS);
                return Redirect("/selfservice/myincident");
            }
            this.AddNotification("Sorry statement could not be updated, if failure continues, please see your system administrator", NotificationType.ERROR);
            return Redirect("/selfservice/myincident");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DeleteIncident(int id)
        {
            var result = disciplineManager.DeleteIncident(id);
            if (result)
            {
                this.AddNotification("Incident Deleted", NotificationType.SUCCESS);
                return RedirectToAction("allincident");
            }
            this.AddNotification("Sorry statement could not be updated, if failure continues, please see your system administrator", NotificationType.ERROR);
            return Redirect("/selfservice/myincident");
        }
    }
}
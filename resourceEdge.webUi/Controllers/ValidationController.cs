using Microsoft.AspNet.Identity;
using resourceEdge.webUi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Controllers
{
    public class ValidationController : Controller
    {
        /// <summary>
        /// This Controller is used to only provide front end validation with the backend.
        /// It can be removed and yet no harm would be brought to the the project.
        /// i used this controller in order to use Ajax to perform validations from the backend
        /// therefore this controller only return results 
        /// </summary>
        /// <returns >The result is always bool (True or False) where true means valid and false means not valid</returns>
        /// 
        BackendValidationManager validation;
        public ValidationController()
        {
            validation = new BackendValidationManager();
        }



        // GET: Validation
        public JsonResult ValidateUser(string empId)
        {
            var result = validation.ValidateUser(empId);
            if (result != false)
            {
                return Json(new {messge = "False" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { messge = "True" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult validateDate(DateTime? Date1, DateTime? Date2)
        {
            if (Date1 != null && Date2 != null)
            {
            var result = validation.validateDates(Date1.Value, Date2.Value);
                if (result != true)
                {
                    return Json(new { messge = "False" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { messge = "True" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { message = "Invalid" }, JsonRequestBehavior.AllowGet);
        }
        [Route("subcription")]
        public ViewResult NotSubscribed(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            ViewBag.PageTitle = "No Subscription";
            return View("NotSubscribed");
        }
        [Route("NoLeave")]
        public ViewResult NoLeave()
        {
            ViewBag.PageTitle = "No Leave";
            return View("NoLeave");
        }
        [Route("UnAuthorized")]
        public ActionResult UnAuthorizesAccess()
        {
            ViewBag.PageTitle = "UnAuthorized";
            return View("UnAuthorized");
        }
    }
}
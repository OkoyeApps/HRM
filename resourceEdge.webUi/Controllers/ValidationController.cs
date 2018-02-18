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
        /// therefore this controller only return json result 
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
           //var mainDate1 =  DateTime.Parse(Date1);
           // var mainDate2 = DateTime.Parse(Date2);
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
    }
}
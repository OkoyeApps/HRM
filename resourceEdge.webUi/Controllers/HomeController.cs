using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Concrete;
using resourceEdge.webUi.Infrastructure;
using System.Threading.Tasks;

namespace resourceEdge.webUi.Controllers
{   
    public class HomeController : Controller
    {
        Iproduct repo;
        public HomeController(Iproduct productRepoparam)
        {
            
            repo = productRepoparam;
        }
        public ActionResult Index()
        {
            return View();
//            return View(repo.product);
        }

        public ActionResult About()
        {
            return View(new products());
        }
        [HttpPost]
        public ActionResult About(products product)
        {
            repo.Addproducts(product);

            //ViewBag.Message = "Your application description page.";
            return RedirectToAction("index");
        }

        public async Task<ActionResult> Contact()
        {
            NotificationManager sendmail = new NotificationManager();
           // var a  =   await sendmail.sendEmailNotification("tenece", "okoyeemma442@gmail.com", "Checking mail", "Chuks.okoye@tenece.com");
            //sendmail.SendEmail("Account message");
            return View();
        }
    }
}
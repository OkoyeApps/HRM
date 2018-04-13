using Microsoft.AspNet.Identity;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.webUi.Infrastructure.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Controllers
{

    [RoutePrefix("Request"), CustomAuthorizationFilter]
    public class RequisitionController : Controller
    {
        private IRequisition RequisitionRepo;
        private IBusinessUnits BunitsRepo;
        private IEmploymentStatus statusRepo;
        private IJobtitles JobRepo;
        IReportManager managerRepo;
        IEmployees EmployeeRepo;
       public RequisitionController(IRequisition RParam, IBusinessUnits BParam, IEmploymentStatus SParam, IJobtitles jParam, IReportManager RMParam, IEmployees EParam)
        {
            RequisitionRepo = RParam;
            BunitsRepo = BParam;
            statusRepo = SParam;
            JobRepo = jParam;
            managerRepo = RMParam;
            EmployeeRepo = EParam;
        }

        // GET: Requsition
        public ActionResult Index()
        {
            var result = RequisitionRepo.Get();
            return View(RequisitionRepo.Get());
        }

        // GET: Requsition/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Requsition/Create
        public ActionResult Create()
        {
            ViewBag.EmpStatus = new SelectList(statusRepo.Get().Select(x => new { name = x.employemntStatus, id = x.empstId }), "id", "name", "id");
            ViewBag.businessUnits = new SelectList(BunitsRepo.Get().Select(x=>new {name = x.unitname, id = x.Id }).OrderBy(x => x.id), "id", "name", "id");
            ViewBag.jobTitles = new SelectList(JobRepo.Get().Select(x=> new {name = x.jobtitlename, id = x.JobId }).OrderBy(x => x.name), "id", "name", "id");
            ViewBag.ReportManager = new SelectList(managerRepo.Get().Select(x => new { name = x.FullName, id = x.ManagerUserId }).OrderBy(x => x.name), "id", "name", "id");
            ViewBag.Employee = new SelectList(EmployeeRepo.Get().Select(x=> new { name = x.FullName, id = x.empID}).OrderBy(x=>x.id), "id", "name", "id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RequisitionViewModel model)
        {
            Requisition Request = new Requisition();
            try
            {
                if (ModelState.IsValid)
                {
                        Request.BusinessunitId = model.BusinessunitId;
                        Request.DepartmentId = model.DepartmentId;
                        Request.JobTitle = model.JobTitle;
                        Request.PositionId = model.PositionId;
                        Request.Approver1 = model.Approver1;
                        Request.Approver2 = model.Approver2;
                        Request.Approver3 = model.Approver3;
                        Request.ClientId = model.ClientId;
                        Request.EmpType = model.EmpType;
                        Request.JobDescription = model.JobDescription;
                        Request.OnboardDate = model.OnboardDate;
                        Request.ReqSkills = model.ReqSkills;
                        Request.RequisitionCode = model.RequisitionCode;
                        Request.ReportingId = model.ReportingId;
                        Request.ReqStatus = model.ReqStatus;
                        Request.ReqPriority = model.ReqPriority.ToString();
                        Request.ReqExpYears = model.ReqExpYears;
                        Request.ReqQualification = model.ReqQualification;
                        Request.ReqNoPositions = model.ReqNoPositions;
                        Request.AdditionalInfo = model.AdditionalInfo;
                        Request.Createdby = User.Identity.GetUserId();
                        Request.Modifiedby = User.Identity.GetUserId();
                        Request.CreatedDate = DateTime.Now;
                        Request.modifiedDate = DateTime.Now;
             
                    RequisitionRepo.Insert(Request);
                    TempData["Success"] = "Requisition Successfully created";
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something went wrong. Please kindly make sure all Required fields are filled + Exception : " + ex.Message;
                throw ex;
            }
            return View(model);
        }

        // GET: Requsition/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Requsition/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Requsition/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Requsition/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

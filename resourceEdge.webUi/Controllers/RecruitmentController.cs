using Microsoft.AspNet.Identity;
using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using resourceEdge.webUi.Infrastructure;
using resourceEdge.webUi.Infrastructure.Core;
using resourceEdge.webUi.Infrastructure.Handlers;
using resourceEdge.webUi.Infrastructure.SystemManagers;
using resourceEdge.webUi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Controllers
{

    [RoutePrefix("Recruitment"), CustomAuthorizationFilter]
    public class RecruitmentController : Controller
    {
        private IRequisition RequisitionRepo;
        private IBusinessUnits BunitsRepo;
        private IEmploymentStatus statusRepo;
        private IJobtitles JobRepo;
        IReportManager managerRepo;
        IEmployees EmployeeRepo;
        RecruitmentManager RecruitmentManager;
        EmployeeManager EmpManager;
        DropDownManager dropDownManager;
        FileManager FManager;
       public RecruitmentController(IRequisition RParam, IBusinessUnits BParam, IEmploymentStatus SParam, IJobtitles jParam, IReportManager RMParam, IEmployees EParam, IFiles FParam, FileManager fmParam)
        {
            RequisitionRepo = RParam;
            BunitsRepo = BParam;
            statusRepo = SParam;
            JobRepo = jParam;
            managerRepo = RMParam;
            EmployeeRepo = EParam;
            RecruitmentManager = new RecruitmentManager();
            EmpManager = new EmployeeManager(EParam, FParam,RMParam);
            dropDownManager = new DropDownManager();
            FManager = fmParam;
        }

        // GET: Requsition
        public ActionResult Index()
        {
            var result = RequisitionRepo.Get();
            ViewBag.PageTitle = "Requisition Dashboard";
            var UserFromSession = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            ViewBag.AllRequisitons = RecruitmentManager.GenerateRequisitionDashboard(UserFromSession.GroupId);
            return View();
        }

        // GET: Requsition/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Requsition/Create
        public ActionResult Create()
        {
            var UserFromSession = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            ViewBag.PageTitle = "Recruitment";
            ViewBag.RecruitmentId = RecruitmentManager.GenerateRequisitionCode(UserFromSession.GroupId);
            ViewBag.EmpStatus = dropDownManager.GetEmploymentStatus();
            ViewBag.businessUnits = dropDownManager.GetBusinessUnit(UserFromSession.GroupId);
            ViewBag.jobTitles = dropDownManager.GetBusinessUnit(UserFromSession.GroupId, UserFromSession.LocationId); 
            ViewBag.ReportManager = new SelectList(managerRepo.GetManagersByLocationAndGroup(UserFromSession.GroupId, UserFromSession.LocationId).Select(x => new { name = x.FullName, id = x.ManagerUserId }).OrderBy(x => x.name), "id", "name", "id");
            ViewBag.Approval1 = new SelectList(EmpManager.GetLocationHeadsDetails(UserFromSession.LocationId).Select(x => new { name = x.FullName, id = x.userId }).OrderBy(x => x.name), "id", "name", "id");
            //fix approval 2 later for the manager in charge to approve
            ViewBag.Approval2 = new SelectList(EmpManager.GetReportManagerBusinessUnit(UserFromSession.UnitId).Select(x => new { name = x.FullName, Id = x.userId }), "Id", "name", "Id");
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
                if (true)
                {
                        Request.BusinessUnitId = model.BusinessunitId;
                        Request.DepartmentId = model.DepartmentId;
                        Request.JobTitleId = model.JobTitle;
                        Request.PositionId = model.PositionId;
                        Request.Approver1 = model.Approver1;
                        Request.Approver2 = model.Approver2;
                        Request.Approver3 = model.Approver3;
                        Request.ClientId = model.ClientId;
                        Request.EmpType = model.EmpType;
                        Request.JobDescription = model.JobDescription;
                        Request.JobTitleId = model.JobTitle;
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
                        this.AddNotification("Requisition created",NotificationType.SUCCESS);
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
               this.AddNotification("Something went wrong. Please kindly make sure all Required fields are filled", NotificationType.ERROR);
                throw ex;
            }
            return View(model);
        }
        public ActionResult AllCandidate()
        {
            var UserFromSession = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            var result = RecruitmentManager.AllCandidate(UserFromSession.GroupId);
            return View(result);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ApproveRequisition(int id)
        {
            var result = RecruitmentManager.ApproveRecruitment(id);
            if (result)
            {
                this.AddNotification("Requistion Approved!", NotificationType.SUCCESS);
                return RedirectToAction("AllRequisition");
            }
            this.AddNotification("Oops! Something went wrong, please try again", NotificationType.ERROR);
            return RedirectToAction("AllRequisition");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DenyRequisition(int id)
        {
            var result = RecruitmentManager.DenyRecruitment(id);
            if (result)
            {
                this.AddNotification("Requistion Denied!", NotificationType.SUCCESS);
                return RedirectToAction("AllRequisition");
            }
            this.AddNotification("Oops! Something went wrong, please try again", NotificationType.ERROR);
            return RedirectToAction("AllRequisition");
        }
        public ActionResult AddCandidate()
        {
            ViewBag.PageTitle = "Add Candidate";
            ViewBag.RequisitionId =new SelectList(RequisitionRepo.Get().Select(X=> new { name = X.RequisitionCode, Id = X.id }), "Id", "name", "Id");
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddCandidate(CandidateViewModel model, HttpPostedFileBase File)
        {
            if (File != null)
            {
                var fileDetails = new FileInfo(File.FileName);
                if (!fileDetails.Extension.Contains(".pdf") || !fileDetails.Extension.Contains(".doc") || !fileDetails.Extension.Contains(".docx"))
                {
                    this.AddNotification("Sorry Only files with extensions of .Pdf or Doc are allowed", NotificationType.ERROR);
                    return RedirectToAction("AddCandidate");
                }
                var Size = FManager.ValidateResume(File);
                if (!Size)
                {
                    this.AddNotification("Sorry, maximum file size is 100(kb)", NotificationType.ERROR);
                    return RedirectToAction("AddCandidate");
                }
            }
            var UserFromSession = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            var result = RecruitmentManager.AddOrUpdateCandidate(model, File, UserFromSession.GroupId);
            if (result)
            {
                this.AddNotification("Candidate Added!", NotificationType.SUCCESS);
                return RedirectToAction("AllCandidate");
            }
            this.AddNotification("Oops! Something went wrong, please try again", NotificationType.ERROR);
            return RedirectToAction("AddCandidate");
        }
        public ActionResult EditCandidate(int id)
        {
            var candidate = RequisitionRepo.GetById(id);
            if (candidate != null)
            {
                ViewBag.PageTitle = "Edit Candidate";
                return View(candidate);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditCandidate(Candidate model)
        {
            return View();
        }

        public ActionResult AllInterview()
        {
            ViewBag.PageTitle = "All Interview";
            var UserFromSession = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            return View(RecruitmentManager.AllInterview(UserFromSession.GroupId));
        }

        public ActionResult AddInterview()
        {
            ViewBag.dropDown = RecruitmentManager.GenerateinterviewDropDown();
            ViewBag.PageTitle = "Schedule Interview";
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddInterview(InterviewViewModel model)
        {
            var result = RecruitmentManager.AddorUpdateInterView(model);
            if (result)
            {
                this.AddNotification("interview Scheduled", NotificationType.SUCCESS);
                return RedirectToAction("AddInterview");
            }
            this.AddNotification("Oops! something went wrong and interview couls not be scheduled. please try again", NotificationType.ERROR);
            return RedirectToAction("AddInterview");
        }
        public ActionResult AddFeedBack(int id)
        {
            var generatedResult = RecruitmentManager.GenerateInterviewForEdit(id);
            if (generatedResult != null)
            {
                ViewBag.PageTitle = $"Feedback For {generatedResult.InterviewName}";
                return View(generatedResult);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        public ActionResult EditInterview(int id)
        {
            var interview = RecruitmentManager.GenerateInterviewForEdit(id);
            if (interview != null)
            {
                ViewBag.PageTitle = $"Feedback For {interview.InterviewName}";
                return View(interview);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditInterview(InterviewViewModel model )
        {
            var result = RecruitmentManager.AddorUpdateInterView(model, model.Id);
            if (result)
            {
                this.AddNotification("Yay! Interview Updated", NotificationType.SUCCESS);
                return RedirectToAction("AllInterview");
            }
            this.AddNotification("Oops! something went wrong, Please try again later", NotificationType.ERROR);
            return RedirectToAction("EditInterview", new { id = model.Id });
        }
        public ActionResult InterviewDetails(int id)
        {
            var result = RecruitmentManager.GenerateInterviewDetail(id);
            if (result.Item1 != null)
            {
                ViewBag.Model = result;
                ViewBag.PageTitle = $"{result.Item1.InterviewName} Details";
                return View(result);
            }
            this.AddNotification("Oops! something went wrong, please make sure the requested interview exist", NotificationType.ERROR);
            return RedirectToAction("AllInterview");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DeleteInterview(int Id)
        {
            var result = RecruitmentManager.DeleteInterview(Id);
            if (result != false)
            {
                this.AddNotification("Interview Deleted", NotificationType.SUCCESS);
                return RedirectToAction("AllInterview");
            }
            this.AddNotification("Oops! something went wrong, please try again", NotificationType.ERROR);
            return RedirectToAction("AllInterview");
        }
        public ActionResult AllCandidateForInterview()
        {
            ViewBag.PageTitle = "All Candidate(s) For Interview";
            var UserFromSession = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            var AllCandidates = RecruitmentManager.GetAllCandidateInterview(UserFromSession.GroupId);       
            return View(AllCandidates);
        }
        public ActionResult AddCandidateForInterview()
        {
            ViewBag.PageTitle = "Add Candidate(s) for Interview";
            var UserFromSession = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            ViewBag.Candidate = RecruitmentManager.CandidatesForInterviewDropDown(UserFromSession.GroupId);
            ViewBag.Interview = RecruitmentManager.GetAllInterviews();
            return View(new CandidateInterviewViewModel());
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddCandidateForInterview(CandidateInterviewViewModel model)
        {
            var result = RecruitmentManager.AddCandidateForInterview(model);
            if (result)
            {
                this.AddNotification("Candidate(s) Added for interview", NotificationType.SUCCESS);
                return RedirectToAction("AllCandidateForInterview");
            }
            return RedirectToAction("AddCandidateForInterview");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult RemoveCandidateForInterview(int Id)
        {
            var result = RecruitmentManager.removecandidateForInterview(Id);
            if (result)
            {
                this.AddNotification("Candidate(s) Removed for interview", NotificationType.SUCCESS);
                return RedirectToAction("AllCandidateForInterview");
            }
            return RedirectToAction("AllCandidateForInterview");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ApproveCandidateFromInterview(int Id)
        {
            var result = RecruitmentManager.AcceptCandidate(Id);
            if (result)
            {
                this.AddNotification("Candidate Accepted!", NotificationType.SUCCESS);
                return RedirectToAction("AllCandidateForInterview");
            }
            this.AddNotification("Oops! Something went wrong, Please try again", NotificationType.ERROR);
            return RedirectToAction("AllCandidateForInterview");
        }

        public ActionResult SelectedCandidates()
        {
            ViewBag.PageTitle = "All Selected Candidates";
            var UserFromSession = (SessionModel)Session["_ResourceEdgeTeneceIdentity"];
            var result = RecruitmentManager.GetSelectedCandidate(UserFromSession.GroupId);
            return View(result);
        }
        public ActionResult candidateDetails(int id)
        {
            ViewBag.PageTitle = "Candidate Details";
            var result = RecruitmentManager.GetCandidateDetails(id);
            return View(result);
        }
        public ActionResult DownloadResume(string FileId)
        {
            var FileToDownload = RecruitmentManager.GetCandidateResume(FileId);
            if (FileToDownload != null)
            {
                var fileFullDetails = FileToDownload.FirstOrDefault();
                var file = fileFullDetails.Value;
            return File(file.FirstOrDefault().Key, file.FirstOrDefault().Value, fileFullDetails.Key);
            }
            this.AddNotification("Oops! seems like the Resume has been deleted or does not exist", NotificationType.WARNING);
            return RedirectToAction("AllCandidateForInterview");
        }
    }
}

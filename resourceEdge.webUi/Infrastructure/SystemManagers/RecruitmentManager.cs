using Microsoft.AspNet.Identity;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;
using resourceEdge.Domain.ViewModels;
using resourceEdge.webUi.Infrastructure.Core;
using resourceEdge.webUi.Models.SystemModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Infrastructure
{
    public class RecruitmentManager
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        //public RecruitmentManager(UnitOfWork unitOfWorkParam)
        //{
        //    unitOfWork = unitOfWorkParam;
        //}

        public string GenerateRequisitionCode(int groupId)
        {
            StringBuilder builder = new StringBuilder();
            var reqCode = unitOfWork.identityCodes.Get(filter: x => x.GroupId == groupId).FirstOrDefault().requisition_code;
            var RecruitmentCount = unitOfWork.Requisition.Get().Count();
            string count = RecruitmentCount.ToString();
            if (RecruitmentCount < 10)
            {
                builder.Append("00");
                builder.Append(RecruitmentCount);
                count = builder.ToString();
            }
            else if (RecruitmentCount > 10 && RecruitmentCount <= 99)
            {
                builder.Append("0");
                builder.Append(RecruitmentCount);
                count = builder.ToString();
            }
            return reqCode + "/" + count;
        }
        public IEnumerable<dynamic> GetBusinessUnit(int groupId, int Location)
        {
            var BusinessUnit = unitOfWork.BusinessUnit.Get(filter: x => x.GroupId == groupId).Select(x => new { Id = x.Id, Name = x.unitname });
            return BusinessUnit;
        }

        public IEnumerable<RequisitionListItems> GetAllRequisition()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();

            if (HttpContext.Current.User.IsInRole("Location Head"))
            {
                var allRequisition = unitOfWork.Requisition.Get(filter: x => x.Approver1 == userId, includeProperties: "BusinessUnit,Department,JobTitle,Position")
                       .Select(x => new RequisitionListItems
                       {
                           BusinessUnitName = x.BusinessUnit.unitname,
                           DepartmentName = x.Department.deptname,
                           Job = x.JobTitle.jobtitlename,
                           Position = x.Position.positionname,
                           ReqCode = x.RequisitionCode,
                           Id = x.id,
                           RaisedBy = unitOfWork.employees.Get(y => y.userId == x.Approver2).FirstOrDefault().FullName,
                           Status = x.AppStatus1
                       });
                return allRequisition;
            }
            if (HttpContext.Current.User.IsInRole("Manager"))
            {
                var allRequisition = unitOfWork.Requisition.Get(filter: x => x.Approver2 == userId && x.AppStatus1 == true , includeProperties: "BusinessUnit,Department,JobTitle,Position")
                 .Select(x => new RequisitionListItems
                 {
                     BusinessUnitName = x.BusinessUnit.unitname,
                     DepartmentName = x.Department.deptname,
                     Job = x.JobTitle.jobtitlename,
                     Position = x.Position.positionname,
                     ReqCode = x.RequisitionCode,
                     Id = x.id,
                     RaisedBy = unitOfWork.employees.Get(y => y.userId == x.Approver2).FirstOrDefault().FullName,
                     Status = x.AppStatus2
                 });
                return allRequisition;
            }
            return null;
        }
        public bool ApproveRecruitment(int id)
        {
            if (HttpContext.Current.User.IsInRole("Location Head"))
            {
                var requisitionForApproval = unitOfWork.Requisition.Get(filter: x => x.id == id && x.AppStatus1 == null).FirstOrDefault();
                if (requisitionForApproval != null)
                {
                    requisitionForApproval.AppStatus1 = true;
                    unitOfWork.Requisition.Update(requisitionForApproval);
                    unitOfWork.Save();
                    return true;
                }
            }
            if (HttpContext.Current.User.IsInRole("Manager"))
            {
                var requisitionForApproval = unitOfWork.Requisition.Get(filter: x => x.id == id && x.AppStatus2 == null).FirstOrDefault();
                if (requisitionForApproval != null)
                {
                    requisitionForApproval.AppStatus2 = true;
                    unitOfWork.Requisition.Update(requisitionForApproval);
                    unitOfWork.Save();
                    return true;
                }
            }
            return false;
        }
        public bool DenyRecruitment(int id)
        {
            if (HttpContext.Current.User.IsInRole("Location Head"))
            {
                var requisitionForApproval = unitOfWork.Requisition.Get(filter: x => x.id == id && x.AppStatus1 == null).FirstOrDefault();
                if (requisitionForApproval != null)
                {
                    requisitionForApproval.AppStatus1 = false;
                    unitOfWork.Requisition.Update(requisitionForApproval);
                    unitOfWork.Save();
                    return true;
                }
            }
            if (HttpContext.Current.User.IsInRole("Manager"))
            {
                var requisitionForApproval = unitOfWork.Requisition.Get(filter: x => x.id == id && x.AppStatus2 == null).FirstOrDefault();
                if (requisitionForApproval != null)
                {
                    requisitionForApproval.AppStatus2 = false;
                    unitOfWork.Requisition.Update(requisitionForApproval);
                    unitOfWork.Save();
                    return true;
                }
            }
            return false;
        }

        public bool AddOrUpdateCandidate(CandidateViewModel model, HttpPostedFileBase File, int groupId, int? Id = null)
        {
            if (Id == null)
            {
                if (model.FirstName != null && model.LastName != null && model.PhoneNumber != null && model.Email != null && model.Skills != null)
                {
                    Candidate candidate = new Candidate()
                    {
                        GroupId = groupId,
                        City = model.City,
                        Country = model.Country,
                        EducationSummary = model.EducationSummary,
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Experience = model.Experience,
                        PhoneNumber = model.PhoneNumber,
                        Qualification = model.Qualification,
                        RequisitionId = model.RequisitionId,
                        Skills = model.Skills,
                        State = model.State,
                        IsActive = true,
                        CreatedBy = HttpContext.Current.User.Identity.GetUserId(),
                        CreatedDate = DateTime.Now
                    };
                    if (File != null)
                    {
                        var Foldername = HttpContext.Current.Server.MapPath("~/Files/Cv/");
                        string fileName = Path.GetFileNameWithoutExtension(File.FileName);
                        string extenstion = Path.GetExtension(File.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssff") + extenstion;
                        candidate.Resume = "~/Files/Cv/" + fileName;
                        fileName = Path.Combine(Foldername + fileName);
                        File.SaveAs(fileName);
                    }
                    unitOfWork.Candidate.Insert(candidate);
                    unitOfWork.Save();

                    if (model.CandidateWork != null)
                    {
                        CandidateWorkDetail workDetail = new CandidateWorkDetail()
                        {
                            CompanyAddress = model.CandidateWork.CompanyAddress,
                            CompanyDesignation = model.CandidateWork.CompanyDesignation,
                            CompanyFrom = model.CandidateWork.CompanyFrom,
                            CompanyTo = model.CandidateWork.CompanyTo,
                            CompanyName = model.CandidateWork.CompanyName,
                            CompanyPhoneNumber = model.CandidateWork.CompanyPhoneNumber,
                            CompanyWebsite = model.CandidateWork.CompanyWebsite,
                            IsActive = true,
                            CandidateId = candidate.Id,
                            CreatedBy = HttpContext.Current.User.Identity.GetUserId(),
                            CreatedDate = DateTime.Now
                        };
                        unitOfWork.CandidateWorkDetail.Insert(workDetail);
                        unitOfWork.Save();
                        return true;
                    }
                    return true;
                }
                else
                {
                    var candidate = unitOfWork.Candidate.GetByID(Id);
                    if (candidate != null)
                    {
                        candidate.FirstName = model.FirstName;
                        candidate.LastName = model.LastName;
                        candidate.PhoneNumber = model.PhoneNumber;
                        if (candidate.Resume != null)
                        {
                            //Update this code later and put an alert that you can't update any detail inputed later
                        }
                    }
                }
            }
            return false;
        }

        public bool AddorUpdateInterView(InterviewViewModel model, int? interviewId = null)
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            TimeSpan currentTime, timeforDate;
            DateTime? newDate = null;
            if (model.InterviewDate != null && model.InterviewTime != null)
            {
                currentTime = TimeSpan.ParseExact(model.InterviewTime, "g", culture);
                timeforDate = model.InterviewDate.TimeOfDay;
                newDate = model.InterviewDate.Subtract(timeforDate);
                newDate = newDate.Value.Add(currentTime);
            }
            if (interviewId == null)
            {
                ///what happened here is that since c# does not automatically provide a time except with the timespan datatype 
                ///we then use convert the string time we got from our model to its equivalent timespan ,
                ///then since we have the date at which the interview would be sheduled,  but with the current system time 
                ///we get the time property of the interview date and substract it from the interviewdate
                ///which will give us the default time of 12:00AM.
                ///wwith this now we can add our parsed time to the newdate we have and that becomes the time for the interview.
                ///If more efficient way is seen later then it would be modified...
                Interview interview = new Interview()
                {
                    City = model.City,
                    Country = model.Country,
                    CreatedBy = HttpContext.Current.User.Identity.GetUserId(),
                    CreatedOn = DateTime.Now,
                    InterviewDate = model.InterviewDate,
                    Interviewer = model.Interviewer,
                    InterviewName = model.InterviewName,
                    InterviewStatusId = model.InterviewStatusId,
                    InterviewTime = newDate.Value,
                    InterviewTypeId = model.InterviewTypeId,
                    LocationId = model.LocationId,
                    RequisitionId = model.RequisitionId,
                    State = model.State,
                    UseCount = 1
                };
                unitOfWork.Interview.Insert(interview);
                unitOfWork.Save();
                return true;
            }
            else
            {
                var interview = unitOfWork.Interview.GetByID(interviewId);
                if (interview != null)
                {
                    if (!HttpContext.Current.Request.UrlReferrer.AbsolutePath.ToLower().Contains("feedback"))
                    {
                        interview.City = model.City;
                        interview.Country = model.Country;
                        interview.InterviewDate = model.InterviewDate != null ? model.InterviewDate : interview.InterviewDate;
                        interview.Interviewer = model.Interviewer;
                        interview.InterviewTime = model.InterviewTime != null ? newDate.Value : interview.InterviewTime;
                        interview.InterviewTypeId = model.InterviewTypeId;
                        interview.State = model.State;
                    }
                    else
                    {
                        interview.FeedBack = model.FeedBack;
                        interview.FeedBackSummary = model.FeedBackSummary;
                    }
                    unitOfWork.Interview.Update(interview);
                    unitOfWork.Save();
                    return true;
                }
            }
            return false;
        }
        public InterviewViewModel GenerateInterviewForEdit(int id)
        {
            var interview = unitOfWork.Interview.Get(filter: x => x.Id == id, includeProperties: "InterviewStatus,InterviewType,Requisition").FirstOrDefault(); ;
            if (interview != null)
            {
                InterviewViewModel interviewModel = new InterviewViewModel()
                {
                    City = interview.City,
                    Country = interview.Country,
                    Id = interview.Id,
                    InterviewDate = interview.InterviewDate,
                    InterviewName = interview.InterviewName,
                    InterviewTime = interview.InterviewTime.ToShortTimeString().ToString(),
                    InterviewStatusId = interview.InterviewStatusId,
                    InterviewTypeId = interview.InterviewTypeId,
                    RequisitionId = interview.RequisitionId,
                    Interviewer = interview.Interviewer,
                    State = interview.State
                };

                interviewModel.InterviewType = new SelectList(unitOfWork.InterviewType.Get(), "Id", "Name", new { Id = interviewModel.InterviewTypeId });
                interviewModel.InterviewStatus = new SelectList(unitOfWork.InterviewStatus.Get(), "Id", "Name", new { Id = interviewModel.InterviewStatusId });
                interviewModel.Location = new SelectList(unitOfWork.Locations.Get(), "Id", "State", new { Id = interviewModel.LocationId });
                interviewModel.Requisition = new SelectList(unitOfWork.Requisition.Get(), "id", "RequisitionCode", new { Id = interviewModel.RequisitionId });
                interviewModel.EligibleInterview = new SelectList(unitOfWork.employees.Get().Select(x => new { Id = x.userId, name = x.FullName }), "Id", "name", new { Id = interviewModel.Interviewer });
                return interviewModel;
            }
            return null;
        }
        public bool CloseInterview(int id)
        {
            var interview = unitOfWork.Interview.GetByID(id);
            if (interview != null)
            {
                interview.IsCompleted = true;
                interview.IsActive = false;
                unitOfWork.Interview.Update(interview);
                return true;
            }
            return false;
        }
        public bool OpenInterView(int id)
        {
            var interview = unitOfWork.Interview.GetByID(id);
            if (interview != null)
            {
                interview.IsCompleted = null;
                interview.IsActive = true;
                interview.UseCount = interview.UseCount++;
                unitOfWork.Interview.Update(interview);
                return true;
            }
            return false;
        }

        public bool DeleteInterview(int Id)
        {
            var interviewToDelete = unitOfWork.Interview.GetByID(Id);
            if (interviewToDelete != null)
            {
                var candidateAndInterview = unitOfWork.CandidateInterview.Get(filter: x => x.InterviewId == interviewToDelete.Id).ToList();
                if (candidateAndInterview != null && candidateAndInterview.Count > 0)
                {
                    candidateAndInterview.ForEach(x => unitOfWork.CandidateInterview.Delete(x));
                }
                unitOfWork.Interview.Delete(interviewToDelete);
                unitOfWork.Save();
                return true;
            }
            return false;
        }
        public IEnumerable<Interview> AllInterview(int groupId)
        {
            return unitOfWork.Interview.Get(filter: x => x.Requisition.GroupId == groupId, includeProperties: "Requisition", orderBy: x => x.OrderBy(y => y.InterviewDate));
        }
        public InterviewDropDown GenerateinterviewDropDown()
        {
            var requisition = new SelectList(unitOfWork.Requisition.Get(filter: x => x.AppStatus1 == true
                                            && (x.AppStatus2 == true || x.AppStatus2 == null) && (x.AppStatus3 == null || x.AppStatus3 == true))
                                            .Select(X => new { name = X.RequisitionCode, Id = X.id }), "Id", "name");
            var status = new SelectList(unitOfWork.InterviewStatus.Get().Select(X => new { name = X.Name, Id = X.Id }), "Id", "name");
            var type = new SelectList(unitOfWork.InterviewType.Get().Select(x => new { name = x.Name, Id = x.Id }), "Id", "name");
            InterviewDropDown dropDown = new InterviewDropDown()
            {
                interviewStatus = status,
                InterviewType = type,
                Requisition = requisition
            };
            return dropDown;
        }
        public Tuple<Interview, IList<Candidate>, EmloyeeDetailistItem> GenerateInterviewDetail(int id)
        {
            EmloyeeDetailistItem detail = null;
            var interview = unitOfWork.Interview.Get(x => x.Id == id, includeProperties: "InterviewType,Requisition,CandidateInterview").FirstOrDefault();
            IList<Candidate> allCandidates = new List<Candidate>();
            foreach (var item in interview.CandidateInterview)
            {
                var candidate = unitOfWork.Candidate.GetByID(item.CandidateId);
                if (true)
                {
                    allCandidates.Add(candidate);
                }
            }
            if (interview.Requisition != null)
            {
                detail = new EmloyeeDetailistItem
                {
                    BusinessUnitName = unitOfWork.BusinessUnit.GetByID(interview.Requisition.BusinessUnitId).unitname,
                    DepartmentName = unitOfWork.Department.GetByID(interview.Requisition.DepartmentId).deptname,
                    JobTitle = unitOfWork.jobTitles.GetByID(interview.Requisition.JobTitleId).jobtitlename,
                    PositionName = unitOfWork.positions.GetByID(interview.Requisition.PositionId).positionname
                };
            }
            return Tuple.Create(interview, allCandidates, detail);
        }

        public SelectList CandidatesForInterviewDropDown(int groupId)
        {
            var PendingStatus = unitOfWork.CandidateStatus.Get(filter: x => x.Name == "Pending").FirstOrDefault();
            var candidate = new SelectList(unitOfWork.Candidate.Get(filter: x => x.GroupId == groupId && x.Status == null).Select(x => new { name = x.FirstName + "" + x.LastName, Id = x.Id }), "Id", "name", "Id");
            return candidate;
        }
        public SelectList GetAllInterviews()
        {
            var interviews = new SelectList(unitOfWork.Interview.Get(filter: x => x.IsCompleted == null).Select(x => new { name = x.InterviewName, Id = x.Id }), "Id", "name", "Id");
            return interviews;
        }
        public Interview GetInterview(int id)
        {
            var interview = unitOfWork.Interview.Get(filter: x => x.Id == id, includeProperties: "InterviewStatus,InterviewType,Requisition").FirstOrDefault(); ;
            return interview;
        }
        public bool AddCandidateForInterview(CandidateInterviewViewModel model)
        {
            if (model.CandidateId.Count > 0)
            {
                foreach (var item in model.CandidateId)
                {
                    var isCandidateAlreadyScheduled = unitOfWork.CandidateInterview.Get(filter: x => x.Candidate.Id == item).Any(x => (x.InterView.Id == model.InterviewId && x.InterView.IsCompleted == null));
                    if (!isCandidateAlreadyScheduled)
                    {
                        CandidateInterview candidateInterview = new CandidateInterview()
                        {
                            CandidateId = item,
                            InterviewId = model.InterviewId
                        };
                        unitOfWork.CandidateInterview.Insert(candidateInterview);
                    }
                }
                unitOfWork.Save();
                return true;
            }
            return false;
        }
        public bool removecandidateForInterview(int? id)
        {
            if (id != null)
            {
                var candidate = unitOfWork.CandidateInterview.Get(filter: x => x.CandidateId == id).FirstOrDefault();
                if (candidate != null)
                {
                    unitOfWork.CandidateInterview.Delete(candidate);
                }
                unitOfWork.Save();
                return true;
            }
            return false;
        }
        public AllCandidateInterviewViewModel GetAllCandidateInterview(int groupId)
        {
            AllCandidateInterviewViewModel Candidates = new AllCandidateInterviewViewModel();
            unitOfWork.CandidateInterview.Get(filter: x => x.Candidate.Status == null, includeProperties: "InterView,Candidate").Where(x => x.Candidate.GroupId == groupId).ToList().ForEach(x => Candidates.candidates.Add(new CandidateInterviewForDetails { InterviewName = x.InterView.InterviewName, CandidateName = x.Candidate.FirstName + x.Candidate.LastName, Id = x.Candidate.Id }));
            var IdsAndResume = CandidateResume();
            Candidates.IdsAndResume = IdsAndResume;
            return Candidates;
        }

        public bool AcceptCandidate(int Id)
        {
            var candidate = unitOfWork.Candidate.Get(filter: x => x.Id == Id).FirstOrDefault();
            if (candidate != null)
            {
                candidate.Status = true;
                candidate.IsActive = false;
                unitOfWork.Candidate.Update(candidate);
                unitOfWork.Save();
                return true;

            }
            return false;
        }
        public bool RejectCandidate(int Id)
        {
            var candidate = unitOfWork.Candidate.Get(filter: x => x.Id == Id).FirstOrDefault();
            if (candidate != null)
            {
                candidate.Status = false;
                candidate.IsActive = false;
                unitOfWork.Candidate.Update(candidate);
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        public IEnumerable<CandidateViewModel> GetAllShortlistedCandidates(int groupId)
        {
            var candidate = unitOfWork.Candidate.Get(filter: x => x.GroupId == groupId)
                .Select(x => new CandidateViewModel
                { FirstName = x.FirstName, LastName = x.LastName, Email = x.Email, PhoneNumber = x.PhoneNumber });
            return candidate;
        }
        public Dictionary<int, string> CandidateResume()
        {
            Dictionary<int, string> IdAndResume = new Dictionary<int, string>();
            var candidate = unitOfWork.Candidate.Get(filter: x => x.Resume != null).Select(x => new { Id = x.Id, path = x.Resume }).ToList();
            candidate.ForEach(x => IdAndResume.Add(x.Id, x.path));
            return IdAndResume;
        }
        public Dictionary<string, Dictionary<string, string>> GetCandidateResume(string path)
        {
            string fileName = string.Empty;
            string ContentType = string.Empty;
            string Name = string.Empty;
            Dictionary<string, string> FileToReturn = new Dictionary<string, string>();
            Dictionary<string, Dictionary<string, string>> FullFileDetails = new Dictionary<string, Dictionary<string, string>>();
            if (!string.IsNullOrWhiteSpace(path))
            {
                var CompletePath = HttpContext.Current.Server.MapPath(path);
                var IsPathValid = Directory.Exists(CompletePath);
                FileInfo fileInfo = new FileInfo(CompletePath);
                if (fileInfo.Exists)
                {
                    fileName = fileInfo.FullName;
                    ContentType = fileInfo.Extension;
                    Name = fileInfo.Name;

                    if (ContentType.Contains(".pdf"))
                    {
                        ContentType = "application/pdf";
                    }

                    else if (ContentType.Contains(".docx"))
                    {
                        ContentType = "application/docx";
                    }
                    FileToReturn.Add(fileName, ContentType);
                    FullFileDetails.Add(Name, FileToReturn);
                    return FullFileDetails;
                }
            }
            return null;
        }
        ///TODO Next is the Requisition dashboard and the candidate dashboard
        ///
        public Tuple<IList<RequisitionListItems>, IList<RequisitionListItems>, IList<RequisitionListItems>> GenerateRequisitionDashboard(int groupId)
        {
            IList<RequisitionListItems> AllApprovedResqusition = new List<RequisitionListItems>();
            IList<RequisitionListItems> AllDeniedResqusition = new List<RequisitionListItems>();
            IList<RequisitionListItems> AllResqusition = new List<RequisitionListItems>();
            unitOfWork.Requisition.Get(filter: x => x.GroupId == groupId && x.AppStatus1 == true && x.AppStatus2 == true, includeProperties: "BusinessUnit,Department,JobTitle,Position").ToList()
                .ForEach(x => AllApprovedResqusition.Add(new RequisitionListItems
                {
                    BusinessUnitName = x.BusinessUnit.unitname,
                    DepartmentName = x.Department.deptname,
                    Job = x.JobTitle.jobtitlename,
                    Position = x.Position.positionname,
                    ReqCode = x.RequisitionCode,
                    Id = x.id,
                    RaisedBy = unitOfWork.employees.Get(y => y.userId == x.Approver2).FirstOrDefault().FullName
                }));
            unitOfWork.Requisition.Get(filter: x => x.GroupId == groupId && x.AppStatus1 == false, includeProperties: "BusinessUnit,Department,JobTitle,Position").ToList()
                    .ForEach(x => AllDeniedResqusition.Add(new RequisitionListItems
                    {
                        BusinessUnitName = x.BusinessUnit.unitname,
                        DepartmentName = x.Department.deptname,
                        Job = x.JobTitle.jobtitlename,
                        Position = x.Position.positionname,
                        ReqCode = x.RequisitionCode,
                        Id = x.id,
                        RaisedBy = unitOfWork.employees.Get(y => y.userId == x.Approver2).FirstOrDefault().FullName
                    }));
            unitOfWork.Requisition.Get(filter: x => x.GroupId == groupId, includeProperties: "BusinessUnit,Department,JobTitle,Position").ToList()
                 .ForEach(x => AllResqusition.Add(new RequisitionListItems
                 {
                     BusinessUnitName = x.BusinessUnit.unitname,
                     DepartmentName = x.Department.deptname,
                     Job = x.JobTitle.jobtitlename,
                     Position = x.Position.positionname,
                     ReqCode = x.RequisitionCode,
                     Id = x.id,
                     Status = x.AppStatus1,
                     RaisedBy = unitOfWork.employees.Get(y => y.userId == x.Approver2).FirstOrDefault().FullName

                 }));
            return Tuple.Create(AllApprovedResqusition, AllDeniedResqusition, AllResqusition);
        }

        public IEnumerable<CandidateViewModel> AllCandidate(int groupId)
        {
            var candidate = unitOfWork.Candidate.Get(filter: x => x.GroupId == groupId)
                .Select(x => new CandidateViewModel
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Status = x.Status,
                    PhoneNumber = x.PhoneNumber
                });
            return candidate;
        }
        public IEnumerable<CandidateViewModel> GetSelectedCandidate(int groupId)
        {
            var candidate = unitOfWork.Candidate.Get(filter: x => x.GroupId == groupId && x.Status == true)
                    .Select(x => new CandidateViewModel
                    {
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email,
                        Status = x.Status,
                        PhoneNumber = x.PhoneNumber
                    });
            return candidate;
        }
        public CandidateWorkDetail GetCandidateDetails(int id)
        {
            var candidate = unitOfWork.CandidateWorkDetail.Get(filter: x => x.CandidateId == id, includeProperties: "Candidate").FirstOrDefault();
            return candidate;
        }
    }
}
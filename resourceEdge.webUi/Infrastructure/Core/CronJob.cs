using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;
using resourceEdge.webUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace resourceEdge.webUi.Infrastructure.Core
{
    public class CronJob
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        NotificationManager manager = new NotificationManager();
        ApplicationUserManager UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        public async Task SendAccountDetails()
        {
            var AccountDetails = unitOfWork.MailDispatch.Get(filter: x => x.Type == Domain.Infrastructures.MailType.Account && x.Delivered == false);
            if (AccountDetails != null || AccountDetails.Count() > 0)
            {
                foreach (var item in AccountDetails)
                {
                    try
                    {
                        EmailObject mailObject;
                        mailObject = new EmailObject { Body = item.Message, Subject = item.Subject, Reciever = item.Reciever, Sender = item.Sender, Footer = item.GroupName, FullName = item.FullName, Type = item.Type };

                        var result = await manager.sendEmailNotification(mailObject);
                        if (result != false)
                        {
                            item.Delivered = true;
                            unitOfWork.MailDispatch.Update(item);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                unitOfWork.Save();
            }
        }

        public void AddSubscriptionCodeToMail()
        {
            var subscription = unitOfWork.AppraisalInitialization.Get(filter: x => x.StartDate == DateTime.Today).ToList();
            int i = 0;
            Dictionary<string, string> groupId = new Dictionary<string, string>();
            Dictionary<string, DateTime> StartDate = new Dictionary<string, DateTime>();
            List<MailDispatcher> mails = new List<MailDispatcher>();
            foreach (var item in subscription)
            {
                if (item.GroupId != i)
                {
                    i = item.GroupId;
                    var Name = unitOfWork.Groups.GetByID(i).GroupName;

                    groupId.Add(Name, item.InitilizationCode);
                    StartDate.Add(Name, item.StartDate);
                }
            }
            Rolemanager Role = new Rolemanager();
            var UserRole = Role.GetRoleByName("HR");
            
            foreach (var item in UserRole.Users)
            {
                var employee = unitOfWork.employees.Get(includeProperties: "Group", filter: x => x.userId == item.UserId)
                    .Select(x => new MailDispatcher
                    {
                        Delivered = false,
                        Reciever = x.empEmail,
                        Type = Domain.Infrastructures.MailType.Appraisal,
                        GroupName = x.Group.GroupName, FullName = x.FullName
                    })
                        .FirstOrDefault();
                mails.Add(employee);
            }
            foreach (var item in mails)
            {
                item.Subject = "Subscription Code for Appraisal Process";
                item.Message = groupId[item.GroupName];
                item.TimeToSend = StartDate[item.GroupName];
                item.Sender = groupId[item.GroupName] + "Appraisal";
                unitOfWork.MailDispatch.Insert(item);
                unitOfWork.Save();
            }
        }

        public async Task SendSubScriptionCode()
        {
            var AppraisalSubscriptionDetails = unitOfWork.MailDispatch.Get(filter: x => x.Type == Domain.Infrastructures.MailType.Appraisal && x.Delivered == false);
            if (AppraisalSubscriptionDetails != null && AppraisalSubscriptionDetails.Count() > 0)
            {
                EmailObject mailObject;
                foreach (var item in AppraisalSubscriptionDetails)
                {
                    try
                    {
                        mailObject  = new EmailObject { Body = item.Message, Subject = item.Subject, Reciever = item.Reciever, Sender = item.Sender, Footer = item.GroupName, FullName = item.FullName };
                        var result = await manager.sendEmailNotification(mailObject);
                        if (result != false)
                        {
                            item.Delivered = true;
                            unitOfWork.MailDispatch.Update(item);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        public void ActivateQuestionsForAppraisal()
        {
            var subscription = unitOfWork.AppraisalInitialization.Get(filter: x => x.StartDate == DateTime.Today).ToList();
            if (subscription != null && subscription.Count() > 0)
            {
                IEnumerable<Question> Questions = new List<Question>();
                int count = subscription.Count;
                for (int i = 0; i < count; i++)
                {
                    Questions =  unitOfWork.Questions.Get(filter: x => x.GroupId == subscription[i].GroupId);
                }
                Questions.ToList().ForEach(x => x.Isactive = true);
                Questions.ToList().ForEach(x => unitOfWork.Questions.Update(x));
                unitOfWork.Save();
            }
        }

        public void ActivateEmployeeAppraisalMenu()
        {
            var subscription = unitOfWork.AppraisalInitialization.Get(filter: x => x.StartDate == DateTime.Today).ToList();
            if (subscription != null)
            {
            var menu = unitOfWork.Menu.GetByID(2);
            menu.Active = true;
            unitOfWork.Menu.Update(menu);
            unitOfWork.Save();
            }
        }

        public void GetAllppraisalForCalculation()
        {
            var allUserSubmitted = unitOfWork.AppraisalQuestion.Get().Select(x => x.UserId).ToArray();
            List<string> SpecifcUserIds = new List<string>();
            Dictionary<string, List<int>> appraisalQuestionAndUserId = new Dictionary<string, List<int>>();
            List<int> allQuestionForAppraiseeToAnswer = new List<int>();
            for (int i = 0; i < allUserSubmitted.Count(); i++)
            {
                if (!SpecifcUserIds.Contains(allUserSubmitted[i]))
                {
                    SpecifcUserIds.Add(allUserSubmitted[i]);
                }
            }
            if (SpecifcUserIds.Count >0 )
            {
               // bool isAppraisalComplete = false;
                foreach (var item in SpecifcUserIds)
                {
                    allQuestionForAppraiseeToAnswer = unitOfWork.Questions.Get(filter: x => x.UserIdForQuestion == item && x.Approved == true).Select(x=>x.Id).ToList();
                    appraisalQuestionAndUserId.Add(item, allQuestionForAppraiseeToAnswer);
                }
                foreach (var item in SpecifcUserIds)
                {
                    var questionsinAppraisal = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == item).Select(x=>x.QuestionId);
                    for (int i = 0; i < SpecifcUserIds.Count; i++)
                    {
                        if (appraisalQuestionAndUserId.ContainsKey(item))
                        {
                            var allQuestion = appraisalQuestionAndUserId[item];
                            for (int j = 0; j < questionsinAppraisal.Count(); j++)
                            {
                                if (!questionsinAppraisal.Contains(allQuestion[j]))
                                {
                                    appraisalQuestionAndUserId.Remove(item);
                                }
                            }
                        }
                    }
                }
                //Dictionary<string, double> TotalAppraisalScore = new Dictionary<string, double>();
                if (appraisalQuestionAndUserId.Count > 0)
                {
                    var allFinishedAppraisalUserId = appraisalQuestionAndUserId.Keys;
                    foreach (var item in allFinishedAppraisalUserId)
                    {
                        var CurrentUserAppraisal = unitOfWork.AppraisalQuestion.Get(filter: x => x.UserId == item).ToList();
                        var isAppraisalComplete = CurrentUserAppraisal.All(x=> x.L1Status == true && x.L2Status == true && x.L3Status == true && x.IsAccepted == true);
                        if (isAppraisalComplete)
                        {
                            var totalScore = CurrentUserAppraisal.Average(x => x.Answer);
                            //TotalAppraisalScore.Add(item, totalScore);
                            AppraisalResult result = new AppraisalResult();
                            result.UserId = item;
                            result.Score = totalScore;
                            //unitOfWork.AppraisalResult.Insert(result);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Remember that when an appraisal ends you need to close all actie subscription by checking the isactive field and disabling it...
        /// </summary>

        public void CloseAllFinishedAppraisal()
        {
            var OpenAppraisal = unitOfWork.AppraisalInitialization.Get(x => x.EndDate == DateTime.Now || x.EndDate > DateTime.Now);
            foreach (var item in OpenAppraisal)
            {
                item.AppraisalStatusId = 2;
                item.IsActive = false;
                unitOfWork.AppraisalInitialization.Update(item);
            }
            unitOfWork.Save();
            //Remember to push this to the summary table later
        }
        public void CloseAllExpiredIncidents()
        {
            var ExpiredIncidents = unitOfWork.Discipline.Get(filter: x => x.ExpiryDate.Date == DateTime.Now.Date).ToList();
            ExpiredIncidents.ForEach(x => x.IsActive = false);
            ExpiredIncidents.ForEach(X => unitOfWork.Discipline.Update(X));
        }

    }
}
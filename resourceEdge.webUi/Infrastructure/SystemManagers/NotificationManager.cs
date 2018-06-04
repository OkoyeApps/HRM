using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.IO;
using resourceEdge.webUi.Models;
using resourceEdge.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace resourceEdge.webUi.Infrastructure
{
    public class NotificationManager
    {
        ApplicationUserManager UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        public async Task<bool> sendEmailNotification(EmailObject Mail,  HttpPostedFileBase attachment=null)
        {
            //string body;

            //call the mabody formatter for the supply of mail address
            var Mailmessage = new MailMessage();
            var message = await this.FormatMailBody(Mail);
            if (attachment != null)
            {
                message.Attachments.Add(new Attachment(attachment.InputStream, Path.GetFileName(attachment.FileName)));
            }

            //var message = new MailMessage();
            //message.To.Add(new MailAddress(mailObject.Reciever));  // replace with valid value 
            //message.From = new MailAddress(mailObject.Sender);  // replace with valid value
            //message.Subject = mailObject.Subject;
            //message.Body = body; //Remeber to Replace the body later for all template
            //message.IsBodyHtml = true;
            
            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "emmaceogames@gmail.com",  // replace with valid value
                    Password = "Chukwudi1997"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                try
                {
                    await smtp.SendMailAsync(message);
                }catch(Exception ex)
                {
                    return false;
                }
                
                return true; 
            }
        }

        public async Task<MailMessage> FormatMailBody(EmailObject Mail)
        {
            var mailMessage = new MailMessage();
            
            string body = Mail.Body;
            if (Mail.Type == Domain.Infrastructures.MailType.Appraisal)
            {
                using (StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath("\\Infrastructure\\EmailTemplates\\") + "AppraisalSubscription.html"))
                {
                    body = await sr.ReadToEndAsync();
                }

            }
            if (Mail.Type == Domain.Infrastructures.MailType.Account)
            {
                using (StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath("\\Infrastructure\\EmailTemplates\\") + "Account.html"))
                {
                    body = await sr.ReadToEndAsync();
                }
                string[] messageToSend = Mail.Body.Split(','); //this is done here because every user created is stored in th maildispatch table and the username and password is seperated by commz.
                string UserName = messageToSend[0];
                string Password = messageToSend[1];
                body = body.Replace("{UserName}", UserName);
                body = body.Replace("{Password}", Password);
                body = body.Replace("{FullName}", Mail.FullName);
                body = body.Replace("{GroupName}", Mail.Footer);  
            }
           

            MailMessage message = new MailMessage();
            message.To.Add(new MailAddress(Mail.Reciever));
            message.From = new MailAddress(Mail.Sender);
            message.Subject = Mail.Subject;
            message.Body = body;            
            message.IsBodyHtml = true;
            return message; //Remeber this should return a mailmessage and not a string
        }
    }
}
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

namespace resourceEdge.webUi.Infrastructure
{
    public class NotificationManager
    {
        public async Task<bool> sendEmailNotification(EmailObject mailObject, HttpPostedFileBase attachment=null)
        {
            string body;

            using (StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath("\\EmailTemplates\\")))
            {
                body = await sr.ReadToEndAsync();
            }
            var Mailmessage = new MailMessage();
            if (attachment != null)
            {
                Mailmessage.Attachments.Add(new Attachment(attachment.InputStream, Path.GetFileName(attachment.FileName)));
            }

            var message = new MailMessage();
            message.To.Add(new MailAddress(mailObject.Reciever));  // replace with valid value 
            message.From = new MailAddress(mailObject.Sender);  // replace with valid value
            message.Subject = mailObject.Subject;
            message.Body = body; //Remeber to Replace the body later for all template
            message.IsBodyHtml = true;
            
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
                await smtp.SendMailAsync(message);
                return true; 
            }
        }

        public async Task<string> FormatMailBody(EmailObject mailObject)
        {
            var mailMessage = new MailMessage();
            string body;
            if (mailObject.Type == Domain.Infrastructures.MailType.Appraisal)
            {
                using (StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath("\\EmailTemplates\\") + "Account.html"))
                {
                    body = await sr.ReadToEndAsync();
                }

            }
            if (mailObject.Type == Domain.Infrastructures.MailType.Account)
            {
                using (StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath("\\EmailTemplates\\") + "AppraisalSubscription.html"))
                {
                    body = await sr.ReadToEndAsync();
                }
            }
            return null;
        }
    }
}
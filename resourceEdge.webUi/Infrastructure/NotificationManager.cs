using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace resourceEdge.webUi.Infrastructure
{
    public class NotificationManager
    {
        public async Task<string> sendEmailNotification(string FromName, string FromEmail, string Message, string recieptaintEmail)
        {
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(recieptaintEmail));  // replace with valid value 
            message.From = new MailAddress(FromEmail);  // replace with valid value
            message.Subject = "Account Details";
            message.Body = string.Format(body, FromName,FromEmail, Message); 
            //message.Body = "Tesing Email in Resource Edge";
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
                return "Finished"; 
            }
        }

        public void SendEmail(string body)
        {
            MailMessage MailMessage = new MailMessage("emmaceogames@gmail.com", "okoyeemma442@gmail.com");
            MailMessage.Subject = "Account Details";
            MailMessage.Body = body;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential("emmaceogames@gmail.com", "Chukwudi1997");
            smtpClient.EnableSsl = true;
            smtpClient.Send(MailMessage);
        }
    }
}
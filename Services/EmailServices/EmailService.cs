using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data.AccountManagement;
using IEduZimAPI.Services.EmailServices;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace IEduZimAPI.Service
{
    public class EmailService : Repository<Email>
    {
        public EmailService() : base() { }

        public static void Send(Models.Enums.EmailType type, Dictionary<string, string> dictionary) =>
            Task.Run(() => new EmailService()._send(type, dictionary));

        public void _send(Models.Enums.EmailType type, Dictionary<string, string> dictionary, string user = "")
        {
            string destination = dictionary["{emailAddress}"];
            var smtp = new SmtpClient
            {
                Host = Startup.configuration["EmailCredentials:Host"],
                Port = Convert.ToInt32(Startup.configuration["EmailCredentials:Port"]),
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(Startup.configuration["EmailCredentials:Username"], Startup.configuration["EmailCredentials:Password"])
            };
            string body = GetBody(type, dictionary);

            var email = base.Add(Email.newInstance(destination, dictionary["{subject}"] ?? dictionary["subject"], body), user);
            var message = new MailMessage()
            {
                Subject = dictionary["{subject}"],
                Body = body,
                IsBodyHtml = true,
                Priority = MailPriority.Normal,
                From = new MailAddress(Startup.configuration["EmailCredentials:Username"], "IEduZim")
            };
            message.To.Add(destination);
            smtp.Send(message);
            email.IsSent = true;
            Update(email.Id, email, user);
            //Delete from AspNetVerification
        }

        public string GetBody(Models.Enums.EmailType type, Dictionary<string, string> dictionary)
        {
            var innerBody = EmailTemplateService.ProcessEmailTemplate(dictionary, type);
            var mainBody = new Dictionary<string, string>
            {
                 {"{body}", innerBody},
                 {"{name}", dictionary["{emailAddress}"].Split('@')[0]},
                 {"{subject}", dictionary["{subject}"]},
                 {"{phoneNumber}", Startup.configuration["EmailCredentials:PhoneNumber"] }
            };
            return EmailTemplateService.ProcessEmailTemplate(mainBody, Models.Enums.EmailType.MainTemplate);
        }
    }
}
using System;

namespace IEduZimAPI.Models.Data.AccountManagement
{
    public class Email
    {
        public string Destination { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int Id { get; set; }
        public bool IsSent { get; set; } = false;
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public Email() { }

        public static Email newInstance(string destination, string subject, string body) =>
            new Email()
            {
                Destination = destination,
                Subject = subject,
                Body = body
            };
    }
}

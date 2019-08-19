using System.Collections.Generic;
using System.Net.Mail;
using System;

namespace NotificationEmails
{
    public class Email : MailMessage
    {      
        public Email(IEnumerable<string>receivers,string from, string subject, string body): base()
        {
            base.From = new MailAddress(from);
            base.Subject = subject;
            base.IsBodyHtml = true;
            base.Body = body;
            foreach (var email in receivers)
            {
                base.To.Add(email);
            }
        }
    }
}

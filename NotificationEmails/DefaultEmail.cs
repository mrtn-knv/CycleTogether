using System.Collections.Generic;
using System.Net.Mail;

namespace NotificationEmails
{
    public abstract class DefaultEmail
    {
        public string AppEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string BaseLink => "http://localhost:4000/api/route/";
        public MailMessage Generate(List<string> receivers)
        {
            var newMessage = new MailMessage();
            FillReceivers(newMessage, receivers);
            newMessage.From = new MailAddress(AppEmail);
            newMessage.Subject = Subject;
            newMessage.IsBodyHtml = true;
            newMessage.Body = Body;
            return newMessage;
        }
        private void FillReceivers(MailMessage newMessage, List<string> receivers)
        {
            foreach (var email in receivers)
            {
                newMessage.To.Add(email);
            }
        }
    }
}

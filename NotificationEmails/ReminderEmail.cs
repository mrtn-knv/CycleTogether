using System.Net.Mail;

namespace NotificationEmails
{
    public class ReminderEmail : DefaultEmail
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Subject => "Upcoming trip";
        public string Body => string.Format(@"<html><head></head><body><b>Check your trips!</b></body>");


        public ReminderEmail(string receiver, string sender)
        {
            Sender = sender;
            Receiver = receiver;
        }

        public override MailMessage Generate()
        {
            var newMessage = new MailMessage();
            newMessage.From = new MailAddress(Sender);
            newMessage.To.Add(Receiver);
            newMessage.Subject = Subject;
            newMessage.IsBodyHtml = true;
            newMessage.Body = Body;
            return newMessage;
        }
    }
}

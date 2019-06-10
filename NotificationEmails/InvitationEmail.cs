using System.Net.Mail;


namespace NotificationEmails
{
    public class InvitationEmail : DefaultEmail
    {
        public string Receiver { get; set; }
        public string Sender { get; set; }
        public string Subject => "Trip invitation";
        public string Body => string.Format(@"<html><head></head><body><b>Somebody has sent you an invitation for trip.</b></body>");

        public InvitationEmail(string receiver, string sender)
        {
            Receiver = receiver;
            Sender = sender;
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

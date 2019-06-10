using System.Net.Mail;
using Microsoft.Extensions.Options;
using CycleTogether.BindingModels;

namespace NotificationEmails
{
    public class NotificationCreator
    {
        private string Sender { get; set; }
        public NotificationCreator(IOptions<NotificationCredentials> credentials)
        {
            Sender = credentials.Value.DefaultSender;
        }
        public  DefaultEmail Create(string type, string receiver)
        {
            switch (type)
            {
                case "Invitation": return new InvitationEmail(receiver, Sender);
                case "Notification": return new ReminderEmail(receiver, Sender);
                default: return null;
            }
            
        }
    }
}

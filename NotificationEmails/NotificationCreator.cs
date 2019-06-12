using Microsoft.Extensions.Options;
using CycleTogether.BindingModels;

namespace NotificationEmails
{
    public class NotificationCreator
    {
        private string AppEmail { get; set; }
        public NotificationCreator(IOptions<NotificationCredentials> credentials)
        {
            AppEmail = credentials.Value.DefaultSender;
        }
        public  DefaultEmail Create(string type, string InvitationSender, string routeId)
        {
            switch (type)
            {
                case "invite": return new InvitationEmail(AppEmail, routeId, InvitationSender);
                case "remind": return new ReminderEmail(AppEmail, routeId);
                default: return null;
            }
            
        }
    }
}

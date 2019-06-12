
namespace NotificationEmails
{
    public class ReminderEmail : DefaultEmail
    {
        public string LinkToFollow { get; set; }

        public ReminderEmail(string appSender, string routeId)
        {
            AppEmail = appSender;
            Subject = "Upcoming trip";
            LinkToFollow = BaseLink + routeId;
            Body = string.Format(@"You have subscribed for an event tomorrow. For more details, please click <a href={0}>here</a>", LinkToFollow);
        }
        
    }
}

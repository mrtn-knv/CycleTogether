

namespace NotificationEmails
{
    public class InvitationEmail : DefaultEmail
    {
        public string Sender { get; set; }        
        public string LinkToFollow { get; set; }                

        public InvitationEmail(string appSender, string routeId, string senderName)
        {
            Sender = senderName;
            Subject = "Trip Invitation";
            LinkToFollow = BaseLink + routeId;
            Body =string.Format(@"<html><head></head><body><p>{0} has sent you invitation for a trip. For more details, please click <a href={1}>here</a></p></body>", Sender, LinkToFollow);
            AppEmail = appSender;
        }

        
        
    }
}

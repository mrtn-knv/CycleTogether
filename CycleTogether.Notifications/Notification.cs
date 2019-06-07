using CycleTogether.Contracts;
using CycleTogether.Claims;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace CycleTogether.Notifications
{
    public class Notification : INotification
    {
        private readonly ClaimsRetriever _claims;
        private readonly NotificationCredentials _mailCredentials;
        private SmtpClient _client;
        public Notification(ClaimsRetriever claims, IOptions<NotificationCredentials> mailCredentials)
        {
            _claims = claims;
            _mailCredentials = mailCredentials.Value;
            _client = Client();
            SetCredentials(_client);

        }
        public void SendNotification(string email, string notification)
        {
            throw new System.NotImplementedException();
        }
        private  void SetCredentials(SmtpClient client)
        {
            var credentials = new NetworkCredential(_mailCredentials.DefaultSender, _mailCredentials.DefaultPass);
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;
        }
        private SmtpClient Client()
        {
            var client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = _mailCredentials.DefaultHost;
            return client;
        }
    }
}

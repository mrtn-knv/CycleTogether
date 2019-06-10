using CycleTogether.Contracts;
using CycleTogether.Claims;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using System;
using CycleTogether.BindingModels;
using NotificationEmails;


namespace CycleTogether.Notifications
{
    public class Notification : INotification
    {
        private readonly ClaimsRetriever _claims;
        private readonly NotificationCredentials _mailCredentials;
        private SmtpClient _client;
        private readonly NotificationCreator _creator;
        public Notification(ClaimsRetriever claims, IOptions<NotificationCredentials> mailCredentials, NotificationCreator creator)
        {
            _claims = claims;
            _creator = creator;
            _mailCredentials = mailCredentials.Value;
            _client = Client();
            SetCredentials(_client);
        }
        public void SendNotification(string email, string notification)
        {
            var message = _creator.Create(notification, email).Generate();
            Send(message);
        }

        public string Send(MailMessage email)
        {
            try
            {
                _client.Send(email);
                return "OK";
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }

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
            client.Port = 587;

            return client;
        }
    }
}

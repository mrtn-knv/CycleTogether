using CycleTogether.Contracts;
using CycleTogether.Claims;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using System;
using CycleTogether.BindingModels;
using NotificationEmails;
using System.Collections.Generic;

namespace CycleTogether.Notifications
{
    public class Notification : INotification
    {
        private readonly ClaimsRetriever _claims;
        private readonly NotificationCredentials _mailCredentials;
        private readonly SmtpClient _client;
        private readonly NotificationCreator _creator;
        private readonly IRouteManager _routes;
        public Notification(ClaimsRetriever claims, IOptions<NotificationCredentials> mailCredentials, NotificationCreator creator, IRouteManager routes)
        {
            _routes = routes;
            _claims = claims;
            _creator = creator;            
            _mailCredentials = mailCredentials.Value;
            _client = Client();
            SetCredentials(_client);
        }
        public string SendNotification(string notification, string routeId, List<string> receiverEmails)
        {
            var invitationSender = _claims.FullName();
            return Send(_creator.Create(notification, invitationSender, routeId).Generate(receiverEmails));
        }

        public string SendReminder(string notification, string routeId)
        {
            var receiversEmails = _routes.Get(Guid.Parse(routeId)).SubscribedMails;
            return Send(_creator.Create(notification, null, routeId).Generate(receiversEmails));
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
            return new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                Host = _mailCredentials.DefaultHost,
                Port = 25
            };            
        }
    }
}

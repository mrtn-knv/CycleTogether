using CycleTogether.Contracts;
using CycleTogether.Claims;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using System;
using CycleTogether.BindingModels;
using NotificationEmails;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace CycleTogether.Notifications
{
    public class Notification : INotification
    {
        private readonly ClaimsRetriever _claims;
        private readonly SmtpClient _client;
        private readonly EmailProperties _emailProperties;
        private readonly NotificationCredentials _emailCredentials;
        private readonly IRouteManager _routes;
        public Notification(ClaimsRetriever claims, EmailProperties emailProperties, NotificationCredentials emailCredentials, IRouteManager routes)
        {
            _routes = routes;
            _claims = claims;
            _emailProperties = emailProperties;
            _emailCredentials = emailCredentials;
            _client = Client();
            SetCredentials(_client);
        }
        public void SendInvitation(string routeId, List<string> receiverEmails)
        {
            Send(InvitationEmail(routeId, receiverEmails));
        }

        private Email InvitationEmail(string routeId, List<string> receiverEmails)
        {
            var invitationSender = _claims.FullName();
            var body = string.Format(_emailProperties.InvitationBody, invitationSender, _emailProperties.BaseLink + routeId);
            return new Email(receiverEmails, _emailCredentials.DefaultSender, _emailProperties.SubjectInvitation, body);
        }

        public void SendReminder(string routeId)
        {
            Send(NotificationEmail(routeId));
        }

        private Email NotificationEmail(string routeId)
        {
            var receiversEmails = _routes.Get(Guid.Parse(routeId)).SubscribedMails;
            var body = string.Format(_emailProperties.ReminderBody, _emailProperties.BaseLink + routeId);
            return new Email(receiversEmails, _emailCredentials.DefaultSender, _emailProperties.SubjectReminder, body);
        }

        public void Send(MailMessage email)
        {
            try
            {
                _client.Send(email);
                
            }
            catch (Exception ex)
            {

                var exception = ex.ToString();
            }
        }
        private  void SetCredentials(SmtpClient client)
        {
            var credentials = new NetworkCredential(_emailCredentials.DefaultSender, _emailCredentials.DefaultPass);
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;
        }
        private SmtpClient Client()
        {
            return new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                Host = _emailCredentials.DefaultHost,
                Port = 25
            };            
        }
    }
}

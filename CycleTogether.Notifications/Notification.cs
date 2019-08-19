using CycleTogether.Contracts;
using CycleTogether.Claims;
using System.Net.Mail;
using System.Net;
using System;
using CycleTogether.BindingModels;
using NotificationEmails;
using System.Collections.Generic;
using WebModels;
using Hangfire;

namespace CycleTogether.Notifications
{
    public class Notification : INotification
    {        
        private readonly ClaimsRetriever _claims;
        private readonly SmtpClient _client;
        private readonly EmailProperties _emailProperties;
        private readonly NotificationCredentials _emailCredentials;
        private readonly IRouteManager _routes;
        private readonly ISubscription _subcription;

        public Notification(ClaimsRetriever claims, EmailProperties emailProperties, NotificationCredentials emailCredentials, IRouteManager routes, ISubscription subcription)
        {
            _routes = routes;
            _subcription = subcription;
            _claims = claims;
            _emailProperties = emailProperties;
            _emailCredentials = emailCredentials;
            _client = Client();
            SetCredentials(_client);
        }
        public void SendInvitation(string routeId, List<User> receiverEmails)
        {
            Send(InvitationEmail(routeId, receiverEmails));
        }

        private Email InvitationEmail(string routeId, List<User> users)
        {
            var invitationSender = _claims.FullName();
            var body = string.Format(_emailProperties.InvitationBody, invitationSender, _emailProperties.BaseLink + routeId);
            return new Email(GetUsersEmails(users), _emailCredentials.DefaultSender, _emailProperties.SubjectInvitation, body);
        }

        private IEnumerable<string> GetUsersEmails(List<User> users)
        {
            foreach (var user in users)
            {
                yield return user.Email;
            }
        }

        public void SendReminder(string routeId)
        {
            var sendDate = _routes.Get(Guid.Parse(routeId)).StartTime.Day - DateTime.Now.Day;
            var job = BackgroundJob.Schedule(
                () => Send(NotificationEmail(routeId)),
                TimeSpan.FromDays(sendDate));            
        }

        private Email NotificationEmail(string routeId)
        {
            var receiversEmails = _subcription.SubscribedEmails(routeId);
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

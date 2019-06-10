using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace NotificationEmails
{
    public abstract class DefaultEmail
    {
        public abstract MailMessage Generate();
    }
}

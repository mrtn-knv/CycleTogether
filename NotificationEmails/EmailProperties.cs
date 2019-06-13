using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationEmails
{
    public class EmailProperties
    {
        public string BaseLink { get; set; }
        public string SubjectInvitation { get; set; }
        public string InvitationBody { get; set; }
        public string SubjectReminder { get; set; }
        public string ReminderBody { get; set; }
    }
}

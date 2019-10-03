using System;
using System.Collections.Generic;
using System.Net.Mail;
using Newtonsoft.Json;

namespace NotificationEmails
{
    public class Email : MailMessage
    {
        [JsonConverter(typeof(MailAddressConverter))]
        public MailAddress EmailAddress { get; set; }
        public Email(IEnumerable<string>receivers,string from, string subject, string body): base()
        {
            base.From = new MailAddress(from);
            base.Subject = subject;
            base.IsBodyHtml = true;
            base.Body = body;
            foreach (var email in receivers)
            {
                base.To.Add(email);
            }
        }
    }


    public class MailAddressConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(MailAddress);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var text = reader.Value as string;
            MailAddress mailAddress;

            return IsValidMailAddress(text, out mailAddress) ? mailAddress : null;
        }

        private bool IsValidMailAddress(string text, out MailAddress mailAddress)
        {
            try
            {
                mailAddress = new MailAddress(text);
                return true;
            }
            catch (Exception)
            {
                mailAddress = null;
                return false;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var mailAddress = value as MailAddress;
            writer.WriteValue(mailAddress == null ? string.Empty : mailAddress.ToString());
        }
    }
}

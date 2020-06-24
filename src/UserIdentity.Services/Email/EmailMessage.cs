using System.Net.Mail;
using SendGrid.Helpers.Mail;

namespace UserIdentity.Services.Email
{
    public class EmailMessage
    {
        public string To { get; set; }
        public string From { get; set; }
        public string PlainContent { get; set; }
        public string HtmlContent { get; set; }
        public string Subject { get; set; }

        public SendGridMessage ToSendGrid()
        {
            var toAddress = new EmailAddress(To);
            var fromAddress = new EmailAddress(From);
            return MailHelper.CreateSingleEmail(fromAddress, toAddress, Subject, PlainContent, HtmlContent);
        }

        public MailMessage ToMailMessage()
        {
            return new MailMessage(new MailAddress(From), new MailAddress(To))
            {
                Body = HtmlContent,
                IsBodyHtml = true,
                Subject = Subject
            };
        }
    }
}

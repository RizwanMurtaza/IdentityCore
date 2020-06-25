using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using SendGrid;

namespace UserIdentity.Services.Email
{
    public class EmailService : IEmailService
    {
        private const string FromAddress = "noreply@findyourdata.io";
        private readonly string _fromAddress;
        private readonly SmtpClient _smtpClient;
        private readonly SendGridClient _client;
        private readonly string _toOverride;
        private readonly string _username;
        private readonly string _password;

        public EmailService(string apiKey, string toOverride, string fromAddress = FromAddress)
        {
            _fromAddress = fromAddress;
            _client = new SendGridClient(apiKey);
            _toOverride = toOverride;
        }

        public EmailService(string ipAddress, string username, string password, string toOverride, string fromAddress = FromAddress)
        {
            _username = username;
            _password = password;
            _toOverride = toOverride;
            _fromAddress = fromAddress;
            _smtpClient = new SmtpClient(ipAddress)
            {
                Credentials = new NetworkCredential { UserName = username, Password = password }
            };
        }

        public async Task SendEmailAsync(EmailMessage message)
        {
            if (!string.IsNullOrEmpty(_toOverride))
            {
                message.To = _toOverride;
            }
            message.From = _fromAddress;
            if (_client != null)
            {

                var sendGridMessage = message.ToSendGrid();
                await _client.SendEmailAsync(sendGridMessage);
            }
            else
            {
                await _smtpClient.SendMailAsync(message.ToMailMessage());
            }
        }
    }
}

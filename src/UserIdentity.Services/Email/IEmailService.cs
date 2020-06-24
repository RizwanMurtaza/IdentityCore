using System.Threading.Tasks;

namespace UserIdentity.Services.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage message);
    }
}
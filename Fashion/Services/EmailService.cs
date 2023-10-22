using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
namespace Fashion.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            var smtpClient = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailSettings.DisplayName, _emailSettings.Password),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.Email),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}

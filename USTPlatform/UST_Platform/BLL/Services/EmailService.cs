using BLL.Services.IServices;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BLL.Services
{
    public class EmailService : IEmailService
    {
        private const string _sender = "ust.platform.ua@gmail.com";
        private const string _senderName = "UST Platform";
        private readonly string _apiKey;

        public EmailService(IConfiguration configuration)
        {
            _apiKey = configuration["EmailServiceKey:DefaultKey"] !;
        }

        public async Task SendEmailAsync(string token, string userEmail, string subject, string emailText)
        {
            var client = new SendGridClient(_apiKey);

            var from = new EmailAddress(_sender, _senderName);
            var to = new EmailAddress(userEmail);

            var msg = MailHelper.CreateSingleEmail(from, to, subject, emailText, emailText);

            await client.SendEmailAsync(msg);
        }
    }
}

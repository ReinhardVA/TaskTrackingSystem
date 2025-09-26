using System.Net;
using System.Net.Mail;
using TaskTrackingSystem.Application.Common.Interfaces;

namespace TaskTrackingSystem.Infrastructure.Services
{
    public class EmailSenderService : IEmailSender
    {
        private readonly SmtpClient _smtpClient;

        public EmailSenderService()
        {
            _smtpClient = new SmtpClient("smtp.gmail.com") // SMTP sunucusu
            {
                Port = 587,
                Credentials = new NetworkCredential("username", "password"),
                EnableSsl = true,
            };
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var mail = new MailMessage("gmailaddress", to, subject, body);
            await _smtpClient.SendMailAsync(mail);
        }
    }
}

using leaveApplication2.Models;
using System.Net.Mail;
using System.Net;
using leaveApplication2.Data;

namespace leaveApplication2.Repostories
{
    public class EmailRepository : IEmailRepository
    {
 
        private readonly SmtpClient _smtpClient;

        public EmailRepository()
        {
            _smtpClient = new SmtpClient("smtp.example.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("your_email@example.com", "your_password"),
                EnableSsl = true,
            };
        }

        public async Task SendEmailAsync(EmailModel email)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(email.SenderEmail),
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email.RecipientEmail);

            await _smtpClient.SendMailAsync(mailMessage);
        }
    }   
}

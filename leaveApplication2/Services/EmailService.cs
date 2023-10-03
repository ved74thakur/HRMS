using leaveApplication2.Models;
using leaveApplication2.Repostories;

namespace leaveApplication2.Services
{
    public class EmailService : IEmailService
    {
    
        private readonly IEmailRepository _emailRepository;

        public EmailService(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        public async Task SendEmailAsync(EmailModel email)
        {
            await _emailRepository.SendEmailAsync(email);
   
        }

    }
}

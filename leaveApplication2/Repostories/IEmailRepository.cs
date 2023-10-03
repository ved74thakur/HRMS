using leaveApplication2.Models;

namespace leaveApplication2.Repostories
{
    public interface IEmailRepository
    {
        Task SendEmailAsync(EmailModel email);
    }
}

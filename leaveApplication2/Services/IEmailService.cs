using leaveApplication2.Models;

namespace leaveApplication2.Services
{
    public interface IEmailService
    {
        Task SendLeaveApprovalEmail(AppliedLeave newAppliedLeave, string mode = "Add");
        Task SendLeaveApprovedEmail(AppliedLeave newAppliedLeave);
        Task SendLeaveRejectedEmail(AppliedLeave newAppliedLeave);
        Task SendLeaveReminderEmail(AppliedLeave appliedLeave);
        Task SendEmployeeCreatedEmail(Employee employee);
        Task SendPasswordResetMail(Employee employee);

        Task SendErrorMail(string email, string body, string subject);
        Task SendDeleteEmail(AppliedLeave newAppliedLeave);



        Task SendCancelRequestEmail(AppliedLeave newAppliedLeave);

    }
}

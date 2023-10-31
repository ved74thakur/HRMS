using Leave.EmailTemplate;
using leaveApplication2.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace leaveApplication2.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmployeeService _employeeService;
        private readonly GenericEmail _genericEmail;
        public async Task SendLeaveApprovalEmail(AppliedLeave newAppliedLeave)
        {
            var appliedLeaveTypeId = newAppliedLeave.appliedLeaveTypeId;
            var employee = await _employeeService.GetEmployeeByIdAsync(newAppliedLeave.employeeId);
            if(employee == null)
            {
                return ;
            }
            long reportingPersonIdValue = employee.ReportingPersonId ?? 0; // Provide a default value if reportingPersonId is null
            var getreportingperson = await _employeeService.GetEmployeeByIdAsync(reportingPersonIdValue);
            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            var body = "";

            body += $"<p>Employee: {employee.firstName} {employee.lastName} has requested for leave approval</p>";
            body += $"<p>Leave Type :{newAppliedLeave.LeaveReason}</p>";
            body += $"<p>Applied from :{newAppliedLeave.StartDate} to {newAppliedLeave.EndDate}</p>";
            body += "<p>Please click one of the following buttons to approve or reject leave:</p>";
            body += $"<a href='http://192.168.1.29:86/api/appliedLeave/UpdateIsApprovedAsync/{appliedLeaveTypeId}/true' style='display: inline-block; background-color: green; color: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Approve</a>";
            body += $"<a href='http://192.168.1.29:86/api/appliedLeave/UpdateIsRejectedAsync/{appliedLeaveTypeId}/true' style='display: inline-block; background-color: red; color: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Reject</a>";




            await _genericEmail.SendEmailAsync(getreportingperson.emailAddress, "Leave Approval", body, employee.emailAddress, "");
        }

        public async Task SendLeaveApprovedEmail(AppliedLeave approvedLeave)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(approvedLeave.employeeId);
            var body = $"<p>Your leave request has been approved for the period: {approvedLeave.StartDate} to {approvedLeave.EndDate}.</p>";

            await _genericEmail.SendEmailAsync(employee.emailAddress, "Leave Approved", body);
        }

        public async Task SendLeaveRejectedEmail(AppliedLeave rejectedLeave)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(rejectedLeave.employeeId);
            var body = $"<p>Your leave request for the period: {rejectedLeave.StartDate} to {rejectedLeave.EndDate} has been rejected.</p>";

            await _genericEmail.SendEmailAsync(employee.emailAddress, "Leave Rejected", body);
        }
    }
}

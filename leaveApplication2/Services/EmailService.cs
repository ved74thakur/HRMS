using Leave.EmailTemplate;
using leaveApplication2.Dtos;
using leaveApplication2.Models;
using leaveApplication2.Models.leaveApplication2.Models;
using leaveApplication2.Other;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace leaveApplication2.Services
{
    public class EmailService:  IEmailService
    {
        private readonly IEmployeeService _employeeService;
        private readonly GenericEmail _genericEmail;
        private readonly IConfiguration _configuration;
        private readonly IFinancialYearService _financialYearService;
        private readonly ILeaveAllocationService _leaveAllocationService;


        public EmailService(IEmployeeService employeeService, GenericEmail genericEmail, IConfiguration configuration, IFinancialYearService financialYearService, ILeaveAllocationService leaveAllocationService)
        {
            _employeeService = employeeService;
            _genericEmail = genericEmail;
            _configuration = configuration;
            _financialYearService = financialYearService;
            _leaveAllocationService = leaveAllocationService;
        }

        public async Task SendLeaveApprovalEmail(AppliedLeave newAppliedLeave)
        {
            var appliedLeaveTypeId = newAppliedLeave.appliedLeaveTypeId;
            var employee = await _employeeService.GetEmployeeByIdAsync(newAppliedLeave.employeeId);
            var reportingPersonId = employee.ReportingPersonId ?? 0;
            var reportingEmployee = await _employeeService.GetEmployeeByIdAsync(reportingPersonId);
          
            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");

            var WebsiteURL = _configuration["BaseURL:WebsiteURL"];

            //add leaveAllocation Year dynamically
            // Expression<Func<FinancialYear, bool>> financialYearFilter = la => la.ActiveYear == true;
            // var activeFinancialYear = await _financialYearService.GetActiveFinancialYearsAsync(financialYearFilter);
            // var financialYearId = activeFinancialYear.First().financialYearId;

            //  //Expression<Func<LeaveAllocation, bool>> filter = la => la.financialYearId == financialYearId;
            //var leaveAllocation = await _leaveAllocationService.GetLeaveAllocationAsync(filter);

            var approveEncryption = EncryptionHelper.Encrypt(newAppliedLeave.appliedLeaveTypeId + "|" + "APR" + "|" +14);
            var rejectEncryption = EncryptionHelper.Encrypt(newAppliedLeave.appliedLeaveTypeId + "|" + "REJ" + "|" + 14);

//#if (DEBUG)
//            //  var approveEncryption = EncryptionHelper.Encrypt(newAppliedLeave.appliedLeaveTypeId + "|" + "APR" + "|" + leaveAllocation.leaveAllocationId);
//            //var rejectEncryption = EncryptionHelper.Encrypt(newAppliedLeave.appliedLeaveTypeId + "|" + "REJ" + "|" + leaveAllocation.leaveAllocationId);
//#elif (RELEASE)
//            var approveEncryption = EncryptionHelper.Encrypt(newAppliedLeave.appliedLeaveTypeId + "|" + "APR" + "|" + leaveAllocation.leaveAllocationId);
//            var rejectEncryption = EncryptionHelper.Encrypt(newAppliedLeave.appliedLeaveTypeId + "|" + "REJ" + "|" + leaveAllocation.leaveAllocationId);
//#endif


            var body = "";

            body += $"<p>Employee: {employee.firstName} {employee.lastName} has requested for leave approval</p>";
            body += $"<p>Leave Type :{newAppliedLeave.LeaveReason}</p>";
            body += $"<p>Applied from :{newAppliedLeave.StartDate} to {newAppliedLeave.EndDate}</p>";


            await _genericEmail.SendEmailAsync(employee.emailAddress, "Leave Approval" + System.DateTime.Now, body);

            body += "<p>Please click one of the following buttons to approve or reject leave:</p>";
            body += $"<a href='{WebsiteURL}/appliedleavestatus/{approveEncryption}' style='display: inline-block; background-color: green; color: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Approve</a>";
            body += $"<a href='{WebsiteURL}/appliedleavestatus/{rejectEncryption}' style='display: inline-block; background-color: red; color: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Reject</a>";


            await _genericEmail.SendEmailAsync(reportingEmployee.emailAddress, "Leave Approval" + System.DateTime.Now, body);
        }

        public async Task SendLeaveApprovedEmail(AppliedLeave newAppliedLeave)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(newAppliedLeave.employeeId);
            
            var body = "";

            body += $"<p>Employee: {employee.firstName} {employee.lastName}</p>";
            body += $"<p>Leave Type :{newAppliedLeave.LeaveReason}</p>";
            body += $"<p>Applied from :{newAppliedLeave.StartDate} to {newAppliedLeave.EndDate}</p>";
            body = $"<p>Your leave request has been approved for the period: {newAppliedLeave.StartDate} to {newAppliedLeave.EndDate}.</p>";


            await _genericEmail.SendEmailAsync(employee.emailAddress, "Leave Approved" + System.DateTime.Now, body);

        }

        public async Task SendLeaveRejectedEmail(AppliedLeave newAppliedLeave)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(newAppliedLeave.employeeId);
          
            var body = "";

            body += $"<p>Employee: {employee.firstName} {employee.lastName}</p>";
            body += $"<p>Leave Type :{newAppliedLeave.LeaveReason}</p>";
            body += $"<p>Applied from :{newAppliedLeave.StartDate} to {newAppliedLeave.EndDate}</p>";
            body = $"<p>Your leave request has been rejected for the period: {newAppliedLeave.StartDate} to {newAppliedLeave.EndDate}.</p>";


            await _genericEmail.SendEmailAsync(employee.emailAddress, "Leave Rejected" + System.DateTime.Now, body);
           
        }
        //two more button for approving or cancel request
        public async Task SendCancelRequestEmail(AppliedLeave newAppliedLeave)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(newAppliedLeave.employeeId);
            var reportingPersonId = employee.ReportingPersonId ?? 0;
            var reportingEmployee = await _employeeService.GetEmployeeByIdAsync(reportingPersonId);
            var WebsiteURL = _configuration["BaseURL:WebsiteURL"];
            var approveCancelEncryption = EncryptionHelper.Encrypt(newAppliedLeave.appliedLeaveTypeId + "|" + "APC" + "|" + 14);
            var rejectRejectEncryption = EncryptionHelper.Encrypt(newAppliedLeave.appliedLeaveTypeId + "|" + "REC" + "|" + 14);
            var body = "";

            body += $"<p>Employee: {employee.firstName} {employee.lastName} has requested for Leave Cancel Request </p>";
            body += $"<p>Leave Type :{newAppliedLeave.LeaveReason}</p>";
            body += $"<p>Applied from :{newAppliedLeave.StartDate} to {newAppliedLeave.EndDate}</p>";
            await _genericEmail.SendEmailAsync(employee.emailAddress, "Leave Cancel" + System.DateTime.Now, body);

            body += "<p>Please click one of the following buttons to approve or reject leave cancel request:</p>";
            body += $"<a href='{WebsiteURL}/appliedleavestatus/{approveCancelEncryption}' style='display: inline-block; background-color: green; color: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Approve</a>";
            body += $"<a href='{WebsiteURL}/appliedleavestatus/{rejectRejectEncryption}' style='display: inline-block; background-color: red; color: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Reject</a>";



            await _genericEmail.SendEmailAsync(reportingEmployee.emailAddress, "Leave Cancel Request" + System.DateTime.Now, body);
        }


        public async Task SendDeleteEmail(AppliedLeave newAppliedLeave)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(newAppliedLeave.employeeId);
            var body = "";

            body += $"<p>Employee: {employee.firstName} {employee.lastName}</p>";
            body += $"<p>Leave Type :{newAppliedLeave.LeaveReason}</p>";
            body += $"<p>Applied from :{newAppliedLeave.StartDate} to {newAppliedLeave.EndDate}</p>";
            body = $"<p>Your leave request has been deleted for the period: {newAppliedLeave.StartDate} to {newAppliedLeave.EndDate}.</p>";


            await _genericEmail.SendEmailAsync(employee.emailAddress, "Leave Deleted" + System.DateTime.Now, body);

        }

      
        public async Task SendEmployeeCreatedEmail(Employee employee)
        {
            
            var body = "";
            var subject = $"Employee: {employee.firstName} {employee.lastName} - Account Registration and Password Reset";
            body += $"<p>You have been successfully registered.</p>";
            body += $"<p>Please reset your password to successfully login into system.</p>";
            body += $"<a href='http://192.168.1.5:85/updatepassword/{employee.employeeId}' style='display: inline-block; background-color: blue; color: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Reset Password</a>";
            body += $"<p>Please click on above link to reset password</p>";

            await _genericEmail.SendEmailAsync(employee.emailAddress, subject, body);
        }

        public async Task SendPasswordResetMail(Employee employee)
        {
            var body = "";
            var subject = $"Employee: {employee.firstName} {employee.lastName} - Password Reset";
            body += $"<p>Please reset your password</p>";
            body += $"<a href='http://192.168.1.5:85/updatepassword/{employee.employeeId}' style='display: inline-block; background-color: blue; color: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Reset Password</a>";
            body += $"<p>Please click on above button to reset password</p>";

            await _genericEmail.SendEmailAsync(employee.emailAddress, subject, body);
        }

        public async Task SendErrorMail(string email,string body, string  subject)
        {
            subject = "Error " + " | " + subject + " | "+ System.DateTime.Now;
            await _genericEmail.SendEmailAsync(email, subject, body);
        }

   
    }
}

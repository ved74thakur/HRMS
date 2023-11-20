﻿using Leave.EmailTemplate;
using leaveApplication2.Dtos;
using leaveApplication2.Models;
using leaveApplication2.Other;
using Microsoft.Extensions.Configuration;

namespace leaveApplication2.Services
{
    public class EmailService:  IEmailService
    {
        private readonly IEmployeeService _employeeService;
        private readonly GenericEmail _genericEmail;
        private readonly IConfiguration _configuration;


        public EmailService(IEmployeeService employeeService, GenericEmail genericEmail, IConfiguration configuration)
        {
            _employeeService = employeeService;
            _genericEmail = genericEmail;
            _configuration = configuration;
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


#if (DEBUG)
            var approveEncryption = EncryptionHelper.Encrypt(newAppliedLeave.appliedLeaveTypeId + "|" + "APR" + "|" + 4);
            var rejectEncryption = EncryptionHelper.Encrypt(newAppliedLeave.appliedLeaveTypeId + "|" + "REJ" + "|" + 4);
#elif (RELEASE)
            var approveEncryption = EncryptionHelper.Encrypt(newAppliedLeave.appliedLeaveTypeId + "|" + "APR" + "|" + 11);
            var rejectEncryption = EncryptionHelper.Encrypt(newAppliedLeave.appliedLeaveTypeId + "|" + "REJ" + "|" + 11);
#endif


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

﻿using Leave.EmailTemplate;
using leaveApplication2.Dtos;
using leaveApplication2.Models;
using leaveApplication2.Models.leaveApplication2.Models;
using leaveApplication2.Other;
using leaveApplication2.Repostories;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace leaveApplication2.Services
{
    public class EmailService:  IEmailService
    {
        private readonly IEmployeeService _employeeService;
        private readonly GenericEmail _genericEmail;
        private readonly IConfiguration _configuration;
      
        private readonly IFinancialYearRepository _financialYearRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;


        public EmailService(IEmployeeService employeeService, GenericEmail genericEmail, IConfiguration configuration, IFinancialYearRepository financialYearRepository, ILeaveAllocationRepository leaveAllocationRepository)
        {
            _employeeService = employeeService;
            _genericEmail = genericEmail;
            _configuration = configuration;
            _financialYearRepository = financialYearRepository;
            _leaveAllocationRepository = leaveAllocationRepository;
        }

        public async Task SendLeaveApprovalEmail(AppliedLeave newAppliedLeave,string mode = "Add")
        {
            var appliedLeaveTypeId = newAppliedLeave.appliedLeaveTypeId;
            var employee = await _employeeService.GetEmployeeByIdAsync(newAppliedLeave.employeeId);
            var reportingPersonId = employee.ReportingPersonId ?? 0;
            var reportingEmployee = await _employeeService.GetEmployeeByIdAsync(reportingPersonId);
          
            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");

            var WebsiteURL = _configuration["BaseURL:WebsiteURL"];

            Expression<Func<FinancialYear, bool>> filterActiveYear = x =>
                    x.ActiveYear == true;
            var activeFinalYear = await _financialYearRepository.GetFinancialYearByIdAsync(filterActiveYear);

            Expression<Func<LeaveAllocation, bool>> filterAllocationYear = x =>
                 x.financialYearId == activeFinalYear.financialYearId;

            var allocationFinalYear = await _leaveAllocationRepository.GetLeaveAllocationAsync(filterAllocationYear);

         
            var approveEncryption = EncryptionHelper.Encrypt(newAppliedLeave.appliedLeaveTypeId + "|" + "APR" + "|" + allocationFinalYear.leaveAllocationId);
            var rejectEncryption = EncryptionHelper.Encrypt(newAppliedLeave.appliedLeaveTypeId + "|" + "REJ" + "|" + allocationFinalYear.leaveAllocationId);
            var body = "";
            var subject = mode == "Update" ? "Leave Approval: Update" : "Leave Approval";


            body += $"<p>Employee: {employee.firstName} {employee.lastName} has requested for leave approval</p>";
            body += $"<p>Leave Type :{newAppliedLeave.LeaveReason}</p>";
            body += $"<p>Applied from :{newAppliedLeave.StartDate.ToString("dd/MM/yyyy")} to {newAppliedLeave.EndDate.ToString("dd/MM/yyyy")}</p>";
            

            await _genericEmail.SendEmailAsync(employee.emailAddress, subject + "" + System.DateTime.Now, body);

            body += "<p>Please click one of the following buttons to approve or reject leave:</p>";
            body += $"<a href='{WebsiteURL}/appliedleavestatus/{approveEncryption}' style='display: inline-block; background-color: green; color: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Approve</a>";
            body += $"<a href='{WebsiteURL}/appliedleavestatus/{rejectEncryption}' style='display: inline-block; background-color: red; color: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Reject</a>";


            await _genericEmail.SendEmailAsync(reportingEmployee.emailAddress, subject + "" + System.DateTime.Now, body);
        }

        public async Task SendLeaveApprovedEmail(AppliedLeave newAppliedLeave)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(newAppliedLeave.employeeId);
            
            var body = "";

            body += $"<p>Employee: {employee.firstName} {employee.lastName}</p>";
            body += $"<p>Leave Type :{newAppliedLeave.LeaveReason}</p>";
            body += $"<p>Applied from :{newAppliedLeave.StartDate.ToString("dd/MM/yyyy")} to {newAppliedLeave.EndDate.ToString("dd/MM/yyyy")}</p>";
            body = $"<p>Your leave request has been approved for the period: {newAppliedLeave.StartDate} to {newAppliedLeave.EndDate}.</p>";


            await _genericEmail.SendEmailAsync(employee.emailAddress, "Leave Approved" + "" + System.DateTime.Now, body);

        }

        public async Task SendLeaveRejectedEmail(AppliedLeave newAppliedLeave)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(newAppliedLeave.employeeId);
          
            var body = "";

            body += $"<p>Employee: {employee.firstName} {employee.lastName}</p>";
            body += $"<p>Leave Type :{newAppliedLeave.LeaveReason}</p>";
            body += $"<p>Applied from :{newAppliedLeave.StartDate.ToString("dd/MM/yyyy")} to {newAppliedLeave.EndDate.ToString("dd/MM/yyyy")}</p>";
            body = $"<p>Your leave request has been rejected for the period: {newAppliedLeave.StartDate} to {newAppliedLeave.EndDate}.</p>";


            await _genericEmail.SendEmailAsync(employee.emailAddress, "Leave Rejected" + "" + System.DateTime.Now, body);
           
        }
    
        public async Task SendCancelRequestEmail(AppliedLeave newAppliedLeave)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(newAppliedLeave.employeeId);
            var reportingPersonId = employee.ReportingPersonId ?? 0;
            var reportingEmployee = await _employeeService.GetEmployeeByIdAsync(reportingPersonId);
            var WebsiteURL = _configuration["BaseURL:WebsiteURL"];
            Expression<Func<FinancialYear, bool>> filterActiveYear = x =>
                   x.ActiveYear == true;
            var activeFinalYear = await _financialYearRepository.GetFinancialYearByIdAsync(filterActiveYear);

            Expression<Func<LeaveAllocation, bool>> filterAllocationYear = x =>
                 x.financialYearId == activeFinalYear.financialYearId;

            var allocationFinalYear = await _leaveAllocationRepository.GetLeaveAllocationAsync(filterAllocationYear);
            var approveCancelEncryption = EncryptionHelper.Encrypt(newAppliedLeave.appliedLeaveTypeId + "|" + "APC" + "|" + allocationFinalYear.leaveAllocationId);
            var rejectRejectEncryption = EncryptionHelper.Encrypt(newAppliedLeave.appliedLeaveTypeId + "|" + "REC" + "|" + allocationFinalYear.leaveAllocationId);
            var body = "";

            body += $"<p>Employee: {employee.firstName} {employee.lastName} has requested for Leave Cancel Request </p>";
            body += $"<p>Leave Type :{newAppliedLeave.LeaveReason}</p>";
            body += $"<p>Applied from :{newAppliedLeave.StartDate.ToString("dd/MM/yyyy")} to {newAppliedLeave.EndDate.ToString("dd/MM/yyyy")}</p>";
            await _genericEmail.SendEmailAsync(employee.emailAddress, "Leave Cancel" + "" + System.DateTime.Now, body);

            body += "<p>Please click one of the following buttons to approve or reject leave cancel request:</p>";
            body += $"<a href='{WebsiteURL}/appliedleavestatus/{approveCancelEncryption}' style='display: inline-block; background-color: green; color: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Approve</a>";
            body += $"<a href='{WebsiteURL}/appliedleavestatus/{rejectRejectEncryption}' style='display: inline-block; background-color: red; color: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Reject</a>";



            await _genericEmail.SendEmailAsync(reportingEmployee.emailAddress, "Leave Cancel Request" + "" + System.DateTime.Now, body);
        }
        //changes
        public async Task SendLeaveReminderEmail(AppliedLeave appliedLeave)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(appliedLeave.employeeId);
            var reportingPersonId = employee.ReportingPersonId ?? 0;
            var reportingEmployee = await _employeeService.GetEmployeeByIdAsync(reportingPersonId);
            var WebsiteURL = _configuration["BaseURL:WebsiteURL"];
            Expression<Func<FinancialYear, bool>> filterActiveYear = x =>
                 x.ActiveYear == true;
            var activeFinalYear = await _financialYearRepository.GetFinancialYearByIdAsync(filterActiveYear);

            Expression<Func<LeaveAllocation, bool>> filterAllocationYear = x =>
                 x.financialYearId == activeFinalYear.financialYearId;

            var allocationFinalYear = await _leaveAllocationRepository.GetLeaveAllocationAsync(filterAllocationYear);
            var approveEncryption = EncryptionHelper.Encrypt(appliedLeave.appliedLeaveTypeId + "|" + "APR" + "|" + allocationFinalYear.leaveAllocationId);
            var rejectEncryption = EncryptionHelper.Encrypt(appliedLeave.appliedLeaveTypeId + "|" + "REJ" + "|" + allocationFinalYear.leaveAllocationId);

            var body = "";
            var subject = "Leave Reminder";

            body += $"<p>Dear {reportingEmployee.firstName} {reportingEmployee.lastName},</p>";
            body += $"<p>This is a reminder to review and approve/reject the leave request for {employee.firstName} {employee.lastName}.</p>";
            body += $"<p><strong>Leave Details:</strong></p>";
            body += $"<p>Employee: {employee.firstName} {employee.lastName}</p>";
            body += $"<p>Leave Type: {appliedLeave.LeaveReason}</p>";
            body += $"<p>Applied from: {appliedLeave.StartDate.ToString("dd/MM/yyyy")} to {appliedLeave.EndDate.ToString("dd/MM/yyyy")}</p>";

            body += $"<a href='{WebsiteURL}/appliedleavestatus/{approveEncryption}' style='display: inline-block; background-color: green; color: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Approve</a>";
            body += $"<a href='{WebsiteURL}/appliedleavestatus/{rejectEncryption}' style='display: inline-block; background-color: red; color: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Reject</a>";
            body += $"<p>Thank you!</p>";


            await _genericEmail.SendEmailAsync(reportingEmployee.emailAddress,   subject + " "+ System.DateTime.Now, body);

        }


        public async Task SendDeleteEmail(AppliedLeave newAppliedLeave)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(newAppliedLeave.employeeId);
            var body = "";

            body += $"<p>Employee: {employee.firstName} {employee.lastName}</p>";
            body += $"<p>Leave Type :{newAppliedLeave.LeaveReason}</p>";
            body += $"<p>Applied from :{newAppliedLeave.StartDate.ToString("dd/MM/yyyy")} to {newAppliedLeave.EndDate.ToString("dd/MM/yyyy")}</p>";
            body = $"<p>Your leave request has been deleted for the period: {newAppliedLeave.StartDate} to {newAppliedLeave.EndDate}.</p>";


            await _genericEmail.SendEmailAsync(employee.emailAddress, "Leave Deleted" + "" + System.DateTime.Now, body);

        }

      
        public async Task SendEmployeeCreatedEmail(Employee employee)
        {
            
            var body = "";
            var WebsiteURL = _configuration["BaseURL:WebsiteURL"];
            var subject = $"Employee: {employee.firstName} {employee.lastName} - Account Registration and Password Reset";
            body += $"<p>You have been successfully registered.</p>";
            body += $"<p>Please reset your password to successfully login into system.</p>";
            //changes to make employeeId encrypted
            body += $"<a href='{WebsiteURL}/updatepassword/{employee.employeeId}' style='display: inline-block; background-color: blue: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Reset Password</a>";
            //body += $"<a href='http://192.168.1.5:85/updatepassword/{employee.employeeId}' style='display: inline-block; background-color: blue; color: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Reset Password</a>";
            body += $"<p>Please click on above button to reset password</p>";

            await _genericEmail.SendEmailAsync(employee.emailAddress, subject + "" + System.DateTime.Now, body);
        }

        public async Task SendPasswordResetMail(Employee employee)
        {
            var body = "";
            var WebsiteURL = _configuration["BaseURL:WebsiteURL"];
            var subject = $"Employee: {employee.firstName} {employee.lastName} - Password Reset";
            body += $"<p>Please reset your password</p>";
            //changes to make employeeId encrypted
            body += $"<a href='{WebsiteURL}/updatepassword/{employee.employeeId}' style='display: inline-block; background-color:blue: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Reset Password</a>";
            //body += $"<a href='http://192.168.1.5:85/updatepassword/{employee.employeeId}' style='display: inline-block; background-color: blue; color: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Reset Password</a>";
            body += $"<p>Please click on above button to reset password</p>";

            await _genericEmail.SendEmailAsync(employee.emailAddress, subject + "" + System.DateTime.Now, body);
        }

      

        public async Task SendErrorMail(string email,string body, string  subject)
        {
            subject = "Error " + " | " + subject + " | "+ System.DateTime.Now;
            await _genericEmail.SendEmailAsync(email, subject + System.DateTime.Now, body);
        }

   
    }
}

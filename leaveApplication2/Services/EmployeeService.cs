
using leaveApplication2.Data;
using leaveApplication2.Models;
using leaveApplication2.Repostories;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using leaveApplication2.Dtos;

namespace leaveApplication2.Services
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IConfiguration _configuration;
        //private readonly SmtpClient _smtpClient;
        //private readonly SmtpSettings _smtpSettings;

        public EmployeeService(IEmployeeRepository employeeRepository, IConfiguration configuration)
        {
            /*
            _smtpSettings = smtpSettings.Value;
            if (string.IsNullOrWhiteSpace(_smtpSettings.Host) ||
                string.IsNullOrWhiteSpace(_smtpSettings.Username) ||
                string.IsNullOrWhiteSpace(_smtpSettings.Password))
            {
                throw new ArgumentException("SMTP configuration is incomplete.");
            }

            _smtpClient = new SmtpClient(_smtpSettings.Host)
            {
                Port = _smtpSettings.Port,
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                EnableSsl = true,
            };
            */
            _employeeRepository = employeeRepository;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await _employeeRepository.GetEmployeesAsync();
        }
        

        public async Task<Employee> GetEmployeeByIdAsync(long id)
        {
            var singleEmployee =  await _employeeRepository.GetEmployeeByIdAsync(id);
            return singleEmployee;
        }



        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            //employee.SetPassword(employee.passwordHash);
            var createdEmployee = await _employeeRepository.CreateEmployeeAsync(employee);
            return createdEmployee;
        }
        public async Task<Employee> RegisterEmployeeAsync(Employee employee)
        {

            var registerEmployee = await _employeeRepository.RegisterEmployeeAsync(employee);
            return registerEmployee;
        }
        //public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        //{

        //    var getSingleEmployeeById = await _employeeRepository.GetEmployeeByIdAsync(employee.employeeId);
        //    if (getSingleEmployeeById == null)
        //    {

        //    }


        //    var updatedEmployee = await _employeeRepository.UpdateEmployeeAsync(employee);
        //    return updatedEmployee;
        //}
        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            using var transaction = _employeeRepository.BeginTransaction(System.Data.IsolationLevel.ReadCommitted); // Specify the isolation level

            try
            {
                var getSingleEmployeeById = await _employeeRepository.GetEmployeeByIdAsync(employee.employeeId);
                if (getSingleEmployeeById == null)
                {
                    throw new ApplicationException("Employee not found"); // You can use a custom exception or a more appropriate one
                }

                // Update the properties of getSingleEmployeeById with the values from the employee object
                getSingleEmployeeById.firstName = employee.firstName;
                getSingleEmployeeById.emailAddress = employee.emailAddress;
                getSingleEmployeeById.mobileNo = employee.mobileNo;
                getSingleEmployeeById.lastName = employee.lastName;
                getSingleEmployeeById.genderId = employee.genderId;
                getSingleEmployeeById.designationId = employee.designationId;
                getSingleEmployeeById.isActive = employee.isActive;
                getSingleEmployeeById.dateOfJoining = employee.dateOfJoining;
                getSingleEmployeeById.dateOfBirth = employee.dateOfBirth;
             //   getSingleEmployeeById.employeePassword = employee.employeePassword;

                // Save the changes to the database
                var updatedEmployee = await _employeeRepository.UpdateEmployeeAsync(getSingleEmployeeById);

                // Commit the transaction if everything is successful
                transaction.Commit();

                return updatedEmployee;
            }
            catch (Exception ex)
            {
                // Handle the error and optionally log it
                transaction.Rollback(); // Rollback the transaction if an error occurs
                throw; // Re-throw the exception for higher-level error handling
            }
        }
        public async Task<Employee> GetEmployeeByEmailAsync(string email)
        {
            return await _employeeRepository.GetEmployeeByEmailAsync(email);
        }


        public async Task DeleteEmployeeAsync(long id)
        {
            await _employeeRepository.DeleteEmployeeAsync(id);
        }

        public async Task<Employee> EmployeeLoginAsync(EmployeeLoginDto employee)
        {
            var loggedEmployee = await _employeeRepository.EmployeeLoginAsync(new Employee() { emailAddress = employee.email, employeePassword = employee.password });
            
            

            return loggedEmployee;
        }

        public async Task<Employee> UpdateEmployeePasswordAsync(long employeeId, EmployeeLoginDto employee)
        {
            var selectedEmployee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            if (selectedEmployee == null)
            {
                return null;
            }
            selectedEmployee.employeePassword = employee.password;
            var updatedEmployeePassword = await _employeeRepository.UpdateEmployeeAsync(selectedEmployee);
            return updatedEmployeePassword;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

      


        /*public async Task<bool> VerifyPasswordAsync(long id, string password)
        {
            var user = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (user == null)
            {
                return false;

            }

            return user.VerifyPassword(password);

        }
        */
        /*public async Task<bool> SendActivationEmailAsync(long id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return false;
            }



            string activationToken = employee.GenerateActivationToken();
            employee.ActivationToken = activationToken;
            await _employeeRepository.UpdateEmployeeRegistrationById(id, employee);
            //string activationLink = $"{_configuration["http://localhost:5024/api]}/activate/{id}/{activationToken}";
            string activationLink = $"http://localhost:5024/api/activate/{id}/{activationToken}";

            bool emailSent = await SendEmailAsync(employee.employeeEmail, activationLink);

            return emailSent;
        }
        */
        /*
        private async Task<bool> SendEmailAsync(string recipientEmail, string activationLink)
        {
            try
            {
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpSettings.Username),
                    Subject = "Account Activation",
                    Body = $"Click the following link to activate your account: {activationLink}",
                    IsBodyHtml = true
                };

                mailMessage.To.Add(recipientEmail);

                using (_smtpClient)
                {
                    await _smtpClient.SendMailAsync(mailMessage);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        */
        /*
        public async Task<bool> ActivateEmployeeAsync(ActivationRequest activationRequest)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(activationRequest.employeeId);
            if (employee == null || string.IsNullOrEmpty(employee.ActivationToken))
            {
                return false; // Employee not found or no activation token
            }

            if (employee.ActivationToken == activationRequest.ActivationToken)
            {
                // Activation token matches, activate the employee
                employee.activationStatusId = 2; // Set activation status to 2
                employee.ActivationToken = null; // Clear activation token
                await _employeeRepository.UpdateEmployeeRegistrationById(employee.employeeId, employee);
                return true;
            }

            return false; // Activation token doesn't match
        }
        */



    }
}


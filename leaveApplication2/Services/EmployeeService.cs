
using leaveApplication2.Data;
using leaveApplication2.Models;
using leaveApplication2.Repostories;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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

        public EmployeeService(IEmployeeRepository employeeRepository, IConfiguration configuration, IOptions<SmtpSettings> smtpSettings)
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

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetAllEmployeesAsync();
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
        public async Task<Employee> UpdateEmployeeRegistrationById(long id, Employee request)
        {

            var updateEmployeeRegistration = await _employeeRepository.UpdateEmployeeRegistrationById(id, request);
            return updateEmployeeRegistration;

        }

        public async Task DeleteEmployeeAsync(long id)
        {
            await _employeeRepository.DeleteEmployeeAsync(id);
        }

        public async Task<Employee> EmployeeLoginAsync(EmployeeLoginDto employee)
        {
            var loggedEmployee = await _employeeRepository.EmployeeLoginAsync(new Employee() {  employeeEmail = employee.email, employeePassword = employee.password });
            
            

            return loggedEmployee;
        }


        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(Employee employee)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, employee.employeeEmail)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Secret").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
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


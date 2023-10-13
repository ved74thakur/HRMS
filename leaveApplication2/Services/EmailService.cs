using leaveApplication2.Models;
using leaveApplication2.Repostories;
using Microsoft.EntityFrameworkCore;

namespace leaveApplication2.Services
{
    public class EmailService : IEmailService
    {
    
        
        private readonly IEmployeeRepository _employeeRepository;

        public EmailService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;

        }

        public async Task<string> VerifyEmployeeEmailAsync(string employeeEmail)
        {
            // Check if the email already exists in the database

            var employeeEmailVerify = await _employeeRepository.VerifyEmployeeEmailAsync(employeeEmail);
            return employeeEmailVerify;
        }

    }
}

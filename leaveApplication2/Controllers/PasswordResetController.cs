using Leave.EmailTemplate;
using leaveApplication2.Dtos;
using leaveApplication2.Models;
using leaveApplication2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace leaveApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordResetController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly GenericEmail _genericEmail;
        private readonly ILogger<EmployeeController> _logger;




        public PasswordResetController(GenericEmail genericEmail, ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {

            _employeeService = employeeService;
            _genericEmail = genericEmail;
            _logger = logger;

        }

        [HttpPost("VerifyEmailAsync")]
        public async Task<ActionResult> VerifyEmailAsync([FromBody] EmployeeEmailDto email)
        {
            var employee = await _employeeService.GetEmployeeByEmailAsync(email.email);

            if (employee != null && employee.emailAddress != null && employee.emailAddress == email.email)
            {
                DateTime currentDateTime = DateTime.Now;
                string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                var body = "";

                // CSS style for the email body
                string emailStyle = @"
            <style>
                p {
                    font-size: 16px;
                    color: #333;
                }
                a {
                    text-decoration: none;
                    background-color: #007BFF;
                    color: white;
                    padding: 10px 20px;
                }
            </style>
        ";

                body += $"<html><head>{emailStyle}</head><body>";
                body += $"<p>Email sent on: {formattedDateTime}</p>";
                body += "<p>Please click the following button:</p>";
                body += $"<a href='http://localhost:3000/updatepassword/{employee.employeeId}'>Reset Password</a>";
                body += "</body></html>";

                await _genericEmail.SendEmailAsync(employee.emailAddress, "Reset Password", body);
                return Ok("Password reset email sent.");
            }

            return NotFound("Email not found.");
        }





    }
}

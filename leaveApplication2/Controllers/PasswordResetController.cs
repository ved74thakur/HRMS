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

                // Updated CSS style for the email body
                string emailStyle = @"
<style>
    body {
        background-color: #f5f5f5; /* Light grey background color */
        display: flex;
        flex-direction: column;
        align-items: center; /* Center align content horizontally */
        justify-content: center; /* Center align content vertically */
        height: 100vh; /* Ensure content takes up the full viewport height */
    }
    p {
        font-size: 16px;
        color: #333;
        text-align: center; /* Center-align text */
    }
    a {
        text-decoration: none;
        background-color: #007BFF; /* Blue background color */
        color: white; /* White text color */
        padding: 10px 20px;
        display: inline-block; /* Ensures the button size is based on content */
        border-radius: 5px; /* Rounded button corners */
    }
</style>
";


                body += $"<html><head>{emailStyle}</head><body>";
                body += $"<p>Email sent on: {formattedDateTime}</p>";
                body += $"<p>Please click the following button for resetting your password:</p>";
                body += $"<a href='http://192.168.1.5:86/updatepassword/{employee.employeeId}'>Reset Password</a>";
                body += "</body></html>";
                await _genericEmail.SendEmailAsync(employee.emailAddress, "Reset Password", body);
                return Ok("Password reset email sent.");
            }

            return NotFound("Email not found.");
        }


        //


    }
}

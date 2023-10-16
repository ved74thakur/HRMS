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

            if (employee != null && employee.employeeEmail != null && employee.employeeEmail == email.email)
            {
                DateTime currentDateTime = DateTime.Now;
                string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                var body = $"";

                body += $"<p>Email sent on: {formattedDateTime}</p>";
                body += "<p>Please click one of the following buttons:</p>";
                body += $"<a href=http://localhost:3000/updatepassword/{employee.employeeId}";

                await _genericEmail.SendEmailAsync(employee.employeeEmail, "Reset Password", body);
                return Ok("Password reset email sent.");
            }

            return NotFound("Email not found.");
        }

     


    }
}

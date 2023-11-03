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
        private readonly ILogger<PasswordResetController> _logger;
        private readonly IEmailService _emailService;




        public PasswordResetController(GenericEmail genericEmail, ILogger<PasswordResetController> logger, IEmployeeService employeeService, IEmailService emailService)
        {

            _employeeService = employeeService;
            _genericEmail = genericEmail;
            _logger = logger;
            _emailService = emailService;

        }

        //[HttpPost("VerifyEmailAsync")]
        //public async Task<ActionResult> VerifyEmailAsync([FromBody] EmployeeEmailDto email)
        //{
        //    var employee = await _employeeService.GetEmployeeByEmailAsync(email.email);

        //    if (employee != null && employee.emailAddress != null && employee.emailAddress == email.email)
        //    {
        //        await _emailService.SendPasswordResetMail(employee);
        //        return Ok("Password reset email sent.");
        //    }

        //    return NotFound("Email not found.");
        //}
        [HttpPost("VerifyEmailAsync")]
        public async Task<ActionResult> VerifyEmailAsync([FromBody] EmployeeEmailDto email)
        {
            _logger.LogInformation($"Start VerifyEmailAsyncs");
            try
            {
                var employee = await _employeeService.GetEmployeeByEmailAsync(email.email);

                if (employee != null && employee.emailAddress != null && employee.emailAddress == email.email)
                {
                    _logger.LogInformation($"Start GetAllEmployees null");
                    //no salutions found
                    await _emailService.SendPasswordResetMail(employee);
                    return StatusCode(200, "Password reset email sent.");

                    //this.CreateResponse<Employee> (,)
                }
                _logger.LogInformation($"Get the values of GetAllEmployeeAsync");
                _logger.LogInformation($"End GetAllEmployeeAsync");
                //Salutions found


                return StatusCode(404, "Email not found");
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return StatusCode(500, ex.Message);
            }

        }

        //


    }
}

using leaveApplication2.Models;
using leaveApplication2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace leaveApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeActivationController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        private readonly ILogger<EmployeeController> _logger;

        
        /*
        public EmployeeActivationController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {

            _employeeService = employeeService;
            
            _logger = logger;
        }

        [HttpPost("sendActivationEmail")]
        public async Task<IActionResult> SendActivationEmail([FromBody] ActivationRequest request)
        {
            if (request.employeeId == 0)
            {
                return BadRequest("Employee Id is required.");
            }

            bool emailSent = await _employeeService.SendActivationEmailAsync(request.employeeId);

            if (emailSent)
            {
                return Ok("Activation email sent successfully.");
            }
            else
            {
                return BadRequest("Failed to send activation email.");
            }
        }
        */

        /*
        [HttpPost("activateEmployee")]
        public async Task<IActionResult> ActivateEmployee([FromBody] ActivationRequest request)
        {
            if ((request.employeeId == 0) || string.IsNullOrEmpty(request.ActivationToken))
            {
                return BadRequest("Employee code and activation token are required.");
            }

            bool activationResult = await _employeeService.ActivateEmployeeAsync(request);

            if (activationResult)
            {
                return Ok("Employee account activated successfully.");
            }
            else
            {
                return BadRequest("Failed to activate employee account. Invalid code or token.");
            }
        }
        */

    }
}

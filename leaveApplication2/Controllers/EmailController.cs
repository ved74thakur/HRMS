using Leave.EmailTemplate;
using leaveApplication2.Models;
using leaveApplication2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace leaveApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<EmailController> _logger;
        public EmailController(IEmailService emailService, ILogger<EmailController> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        [HttpGet("getExistingEmail")]
        public async Task<IActionResult> GetExistingEmployeeEmail(string employeeEmail)
        {
            if (string.IsNullOrWhiteSpace(employeeEmail))
            {
                return BadRequest("Email is required.");
            }

            string existingEmail = await _emailService.VerifyEmployeeEmailAsync(employeeEmail);

            if (!string.IsNullOrWhiteSpace(existingEmail))
            {
                return Ok(existingEmail); // Email exists
            }

            return NotFound("Email not found"); // Email doesn't exist
        }
    }
}

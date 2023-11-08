using leaveApplication2.Dtos;
using leaveApplication2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace leaveApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }
        [HttpPost, Route("login")]
          public async Task<ActionResult<CommonResponse<LoginDetailDto>>> Login([FromBody] LoginInfo credentials)
      
        {
            if (string.IsNullOrEmpty(credentials.employeePassword) || string.IsNullOrEmpty(credentials.emailAdderss))
            {
                _logger.LogInformation("Username and password are required.");
                return BadRequest("Username and password are required.");
            }

            try
            {
                var user = await _authService.AuthenticateUser(credentials.emailAdderss, credentials.employeePassword);
                _logger.LogInformation("User successfully authenticated: {UserName}", credentials.employeePassword);
                //  return Ok(new { Token = token, RefreshToken = refreshToken });


                var successResponse = this.GetCommonResponse<LoginDetailDto>(StatusCodes.Status200OK, "Success", user);
                return successResponse;

              
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "An error occurred while authenticating user: {emailAdderss}", credentials.emailAdderss);

                var errorResponse = this.GetCommonResponse<LoginDetailDto>(StatusCodes.Status500InternalServerError, "An error occurred while authenticating user");
                return errorResponse;
            }
        }
    }
}

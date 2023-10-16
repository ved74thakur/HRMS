using leaveApplication2.Models;
using leaveApplication2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace leaveApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly IGenderService _genderService;
        private readonly ILogger<EmployeeController> _logger;
        public GenderController(IGenderService genderService,ILogger<EmployeeController> logger)
        {

            _genderService = genderService;     
            _logger = logger;
            

        }
        [HttpGet("GetGendersAsync")]
        public async Task<CommonResponse<IEnumerable<Gender>>> GetGendersAsync()
        {
            _logger.LogInformation($"Start GetGendersAsync");
            try
            {
                var employees = await _genderService.GetGendersAsync();
                if (employees == null)
                {
                    _logger.LogInformation($"Start GetGendersAsync null");
                    //no salutions found
                    return this.CreateResponse<IEnumerable<Gender>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                    //this.CreateResponse<Employee> (,)
                }
                _logger.LogInformation($"Get the values of GetGendersAsync");
                _logger.LogInformation($"End GetGendersAsync");
                //Salutions found

                return this.CreateResponse<IEnumerable<Gender>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", employees);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<IEnumerable<Gender>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}

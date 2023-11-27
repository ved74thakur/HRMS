using leaveApplication2.Models;
using leaveApplication2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace leaveApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationService _designationService;
        private readonly ILogger<EmployeeController> _logger;
        public DesignationController(IDesignationService designationService, ILogger<EmployeeController> logger)
        {

            _designationService = designationService;
            _logger = logger;


        }
        [HttpGet("GetDesignationsAsync")]
        public async Task<CommonResponse<IEnumerable<Designation>>> GetDesignationsAsync()
        {
            _logger.LogInformation($"Start GetDesignationsAsync");
            try
            {
                var employees = await _designationService.GetDesignationsAsync();
                if (employees == null)
                {
                    _logger.LogInformation($"Start GetDesignationsAsync null");
                    //no salutions found
                    return this.CreateResponse<IEnumerable<Designation>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                    //this.CreateResponse<Employee> (,)
                }
                _logger.LogInformation($"Get the values of GetDesignationsAsync");
                _logger.LogInformation($"End GetDesignationsAsync");
                //Salutions found

                return this.CreateResponse<IEnumerable<Designation>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", employees);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<IEnumerable<Designation>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}

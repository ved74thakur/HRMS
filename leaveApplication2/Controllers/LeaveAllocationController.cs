using leaveApplication2.Models;
using leaveApplication2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace leaveApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationController : ControllerBase
    {
        private readonly ILogger<LeaveAllocationController> _logger;
        private readonly ILeaveAllocationService _leaseAllocationService;
        public LeaveAllocationController(ILeaveAllocationService leaveAllocationService, ILogger<LeaveAllocationController> logger)
        {
            _leaseAllocationService = leaveAllocationService;
            _logger = logger;

        }
        [HttpGet("GetEmployeesAsync")]
        public async Task<CommonResponse<IEnumerable<LeaveAllocation>>> GetLeaveAllocationsAsync()
        {
            _logger.LogInformation($"Start GetAllEmployees");
            try
            {
                var employees = await _leaseAllocationService.GetLeaveAllocationsAsync();
                if (employees == null)
                {
                    _logger.LogInformation($"Start GetAllEmployees null");
                    //no salutions found
                    return this.CreateResponse<IEnumerable<LeaveAllocation>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                    //this.CreateResponse<Employee> (,)
                }
                _logger.LogInformation($"Get the values of GetAllEmployeeAsync");
                _logger.LogInformation($"End GetAllEmployeeAsync");
                //Salutions found

                return this.CreateResponse<IEnumerable<LeaveAllocation>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", employees);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<IEnumerable<LeaveAllocation>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}

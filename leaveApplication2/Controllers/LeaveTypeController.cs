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
    public class LeaveTypeController : ControllerBase
    {
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly ILogger<EmployeeController> _logger;
        public LeaveTypeController(ILeaveTypeService leaveTypeService, ILogger<EmployeeController> logger)
        {
            _leaveTypeService = leaveTypeService;
            _logger = logger;
        }

        [HttpGet("GetAllLeaveTypes")]
        public async Task<CommonResponse<IReadOnlyCollection<LeaveType>>> GetAllLeaveTypes()
        //public async Task<ActionResult<IReadOnlyCollection<LeaveType>>> GetAllLeaveTypes()
        {
            //  var leaveTypes = await _leaveTypeService.GetAllLeaveTypesAsync();
            // return Ok(leaveTypes);


            _logger.LogInformation($"Start GetAllEmployees");
            try
            {
                var employees = await _leaveTypeService.GetAllLeaveTypesAsync();
                if (employees == null)
                {
                    _logger.LogInformation($"Start GetAllEmployees null");
                    //no salutions found
                    return this.CreateResponse<IReadOnlyCollection<LeaveType>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                    //this.CreateResponse<Employee> (,)
                }
                _logger.LogInformation($"Get the values of GetAllEmployeeAsync");
                _logger.LogInformation($"End GetAllEmployeeAsync");
                //Salutions found

                return this.CreateResponse<IReadOnlyCollection<LeaveType>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", employees);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<IReadOnlyCollection<LeaveType>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveType>> GetLeaveType(int id)
        {
            var leaveType = await _leaveTypeService.GetLeaveTypeByIdAsync(id);
            if (leaveType == null)
            {
                return NotFound();
            }

            return Ok(leaveType);
        }
    }
}

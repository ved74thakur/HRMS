using leaveApplication2.Dtos;
using leaveApplication2.Models;
using leaveApplication2.Models.leaveApplication2.Models;
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
        private readonly ILeaveAllocationService _leaveAllocationService;
        public LeaveAllocationController(ILeaveAllocationService leaveAllocationService, ILogger<LeaveAllocationController> logger)
        {
            _leaveAllocationService = leaveAllocationService;
            _logger = logger;

        }
        [HttpGet("GetLeaveAllocationsAsync")]
        public async Task<CommonResponse<IEnumerable<LeaveAllocation>>> GetLeaveAllocationsAsync()
        {
            _logger.LogInformation($"Start GetAllEmployees");
            try
            {
                var employees = await _leaveAllocationService.GetLeaveAllocationsAsync();
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
        
        [HttpPost("CreateLeaveAllocationAsync")]
        public async Task<CommonResponse<ActionResult<LeaveAllocation>>> CreateLeaveAllocationAsync([FromBody] LeaveAllocation leaveAllocation)
        {
            _logger.LogInformation($"Start CreateLeaveAllocationAsync");

            try
            {
               var newLeaveAllocation  = await _leaveAllocationService.CreateLeaveAllocationAsync(leaveAllocation);
                
               if (newLeaveAllocation == null)
                {
                    _logger.LogInformation($"Start CreateLeaveAllocationAsync null");
                    //no salutions found
                  return this.CreateResponse<ActionResult<LeaveAllocation>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                }
                _logger.LogInformation($"Get the values of CreateLeaveAllocationAsync");
                _logger.LogInformation($"End CreateLeaveAllocationAsync");
                //Salutions found

                return this.CreateResponse<ActionResult<LeaveAllocation>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", newLeaveAllocation);
                


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while creating new leave request");
                //return this.CreateResponse<ActionResult<LeaveAllocation>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
            return null;

        }

        [HttpDelete("DeleteLeaveAllocationAsync/{leaveAllocationId}")]
        public async Task<CommonResponse<ActionResult<LeaveAllocation>>> DeleteLeaveAllocationAsync(int leaveAllocationId)
        {
            _logger.LogInformation($"Start DeleteLeaveAllocationAsync");

            try
            {
                var deletedLeaveAllocation = await _leaveAllocationService.DeleteLeaveAllocationAsync(leaveAllocationId);

                if (deletedLeaveAllocation == null)
                {
                    _logger.LogInformation($"Start DeleteFinancialYearAsync null");
                    // Financial year not found
                    return this.CreateResponse<ActionResult<LeaveAllocation>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "Leave Allocation not found.");
                }

                _logger.LogInformation($"Leave allocation deleted");
                // Financial year deleted successfully
                return this.CreateResponse<ActionResult<LeaveAllocation>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Financial year deleted successfully", deletedLeaveAllocation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the financial year");
                return this.CreateResponse<ActionResult<LeaveAllocation>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("UpdateLeaveAllocationAsync/{leaveAllocationId}")]
        public async Task<CommonResponse<ActionResult<LeaveAllocation>>> UpdateLeaveAllocationAsync(int leaveAllocationId)
        {
            _logger.LogInformation($"Start UpdateLeaveAllocationAsync");

            try
            {
                var updatedFinancialYear = await _leaveAllocationService.UpdateLeaveAllocationAsync(leaveAllocationId);
                //var newAppliedLeaveCreated = CreatedAtAction(nameof(GetEmployeeById), new { id = employee.employeeId }, employee);
                if (updatedFinancialYear == null)
                {
                    _logger.LogInformation($"Start UpdateLeaveAllocationAsync null");
                    //no salutions found
                    return this.CreateResponse<ActionResult<LeaveAllocation>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                }
                _logger.LogInformation($"Get the values of UpdateLeaveAllocationAsync");
                _logger.LogInformation($"End UpdateLeaveAllocationAsync");
                //Salutions found

                return this.CreateResponse<ActionResult<LeaveAllocation>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", updatedFinancialYear);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while creating new leave request");
                return this.CreateResponse<ActionResult<LeaveAllocation>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        //leaveAllocationCreation
        [HttpPost("CreateLeaveAllocationForAllLeaveTypes")]
        public async Task<CommonResponse<IEnumerable<LeaveAllocation>>> CreateLeaveAllocationForAllLeaveTypes([FromBody] LeaveAllocationRequestDto request)
        {
            _logger.LogInformation($"Start CreateLeaveAllocationForAllLeaveTypes");
            try
            {
                var leaveAllocations = await _leaveAllocationService.CreateLeaveAllocationForAllLeaveTypes(request.FinancialYear, request.LeaveTypeCounts);

                if (leaveAllocations == null || leaveAllocations.Count == 0)
                {
                    _logger.LogInformation($"No leave allocations created");
                    return this.CreateResponse<IEnumerable<LeaveAllocation>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No leave allocations created.");
                }

                _logger.LogInformation($"Leave allocations created successfully");
                return this.CreateResponse<IEnumerable<LeaveAllocation>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", leaveAllocations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating leave allocations");
                return this.CreateResponse<IEnumerable<LeaveAllocation>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
        }






    }
}

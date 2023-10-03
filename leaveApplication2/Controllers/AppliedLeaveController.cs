using leaveApplication2.Models;
using leaveApplication2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace leaveApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppliedLeaveController : ControllerBase
    {
        
        private readonly IAppliedLeaveService _leaveService;

        private readonly ILogger<EmployeeController> _logger;

        //private readonly IEmployeeLeaveService _employeeLeaveService;
        public AppliedLeaveController(IAppliedLeaveService leaveService, ILogger<EmployeeController> logger)
        {

            
            _leaveService = leaveService;
            _logger = logger;
            //_employeeLeaveService = employeeLeaveService;

        }
        //getAllAppliedLeave
        [HttpGet("GetAppliedLeavesAsync")]
        public async Task<CommonResponse<IEnumerable<AppliedLeave>>> GetAppliedLeavesAsync()
        {
            _logger.LogInformation($"Start GetAllEmployeesLeaves");
            try
            {
                var leaves = await _leaveService.GetAppliedLeavesAsync();
                if (leaves == null)
                {
                    _logger.LogInformation($"Start GetAllEmployeesLeaves null");
                    //no salutions found
                    return this.CreateResponse<IEnumerable<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                    //this.CreateResponse<Employee> (,)
                }
                _logger.LogInformation($"Get the values of GetAllEmployeeLeavesAsync");
                _logger.LogInformation($"End GetAllEmployeeLeavesAsync");
                //Salutions found

                return this.CreateResponse<IEnumerable<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", leaves);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<IEnumerable<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        //Create leaves
        [HttpPost("CreateAppliedLeaveAsync")]
        public async Task<CommonResponse<ActionResult<AppliedLeave>>> CreateAppliedLeaveAsync(AppliedLeave leave)
        {
            _logger.LogInformation($"Start CreateAppliedLeave");

            try
            {
                var newAppliedLeaveCreated = await _leaveService.CreateAppliedLeave(leave);
                //var newAppliedLeaveCreated = CreatedAtAction(nameof(GetEmployeeById), new { id = employee.employeeId }, employee);
                if (newAppliedLeaveCreated == null)
                {
                    _logger.LogInformation($"Start AddAppliedLeave null");
                    //no salutions found
                    return this.CreateResponse<ActionResult<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                }
                _logger.LogInformation($"Get the values of AddAppliedLeave");
                _logger.LogInformation($"End CreateAppliedLeave");
                //Salutions found

                return this.CreateResponse<ActionResult<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", newAppliedLeaveCreated);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while creating new leave request");
                return this.CreateResponse<ActionResult<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        //update leave
        [HttpPut("UpdateAppliedLeaveAsync/{id}")]
        public async Task<CommonResponse<ActionResult<AppliedLeave>>> UpdateAppliedLeaveAsync(long id, AppliedLeave leave)
        {
            _logger.LogInformation($"Start UpdateAppliedLeave");


            //return Ok(result);
            try
            {
                var updatedLeave = await _leaveService.UpdateAppliedLeaveAsync(id, leave);
                if (updatedLeave == null)
                {
                    _logger.LogInformation($"Start UpdateAppliedLeave null");
                    //no salutions found

                    return this.CreateResponse<ActionResult<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                }
                _logger.LogInformation($"Get the values of GetEmployeeByIdAsync");
                _logger.LogInformation($"End GetEmployeeByIdAsync");
                //Salutions found
                return this.CreateResponse<ActionResult<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", updatedLeave);
                // return this.CreateResponse<IEnumerable<Employee>>(StatusCode.Status200K, "Success", employee);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<ActionResult<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //getSingleLeave

        [HttpGet("GetAppliedLeaveByIdAsync/{id}")]
        public async Task<CommonResponse<AppliedLeave>> GetAppliedLeaveByIdAsync(long id)
        {
            _logger.LogInformation($"Start GetSingleAppliedLeave");
            try
            {
                var singleAppliedLeave = await _leaveService.GetAppliedLeaveByIdAsync(id);
                if (singleAppliedLeave == null)
                {
                    _logger.LogInformation($"Start GetEmployeeById null");
                    //no salutions found
                    return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                }
                _logger.LogInformation($"Get the values of GetEmployeeByIdAsync");
                _logger.LogInformation($"End GetEmployeeByIdAsync");
                //Salutions found
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", singleAppliedLeave);
                // return this.CreateResponse<IEnumerable<Employee>>(StatusCode.Status200K, "Success", employee);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        //delete leave

        [HttpDelete("DeleteAppliedLeaveByIdAsync/{id}")]
        public async Task<CommonResponse<AppliedLeave>> DeleteAppliedLeaveByIdAsync([FromRoute] long id)
        {
            var selectedAppliedLeave = await _leaveService.GetAppliedLeaveByIdAsync(id);
            if (selectedAppliedLeave == null)
            {
                _logger.LogInformation($"Start GetSingleAppliedLeave null");
                //no salutions found
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");
            }
            _logger.LogInformation($"Start DeleteAppliedLeave");
            try
            {
                await _leaveService.DeleteAppliedLeaveByIdAsync(id);

                // Successful deletion
                _logger.LogInformation($"End DeleteAppliedLeave");
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status204NoContent, "Success");
            }
            catch (Exception ex)
            {
                // Error occurred during deletion
                _logger.LogError(ex, "An error occurred while deleting the applied leave");
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

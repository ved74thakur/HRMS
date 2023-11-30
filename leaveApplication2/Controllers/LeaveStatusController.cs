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
    public class LeaveStatusController : ControllerBase
    {
        private readonly ILeaveStatusService _leaveStatusService;
        private readonly ILogger<LeaveStatusController> _logger;
        public LeaveStatusController(ILeaveStatusService service, ILogger<LeaveStatusController> logger)
        {
            _leaveStatusService = service;
            _logger = logger;
        }

        
        [HttpGet("GetLeaveStatusesAsync")]
        public async Task<CommonResponse<IEnumerable<LeaveStatus>>> GetLeaveStatusesAsync()
        {
            _logger.LogInformation($"Start GetLeaveStatusesAsync");
            try
            {
                var leaveStatus = await _leaveStatusService.GetLeaveStatusesAsync();
                if (leaveStatus == null)
                {
                    _logger.LogInformation($"Start GetAllLeaveStatusesAsync null");
                    //no salutions found
                    return this.CreateResponse<IEnumerable<LeaveStatus>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                    //this.CreateResponse<Employee> (,)
                }
                _logger.LogInformation($"Get the values of GetAllLeaveStatusesAsync");
                _logger.LogInformation($"End GetAllLeaveStatusesAsync");
                //Salutions found

                return this.CreateResponse<IEnumerable<LeaveStatus>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", leaveStatus);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<IEnumerable<LeaveStatus>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        //[HttpGet("GetLeaveStatusByIdAsync/{id}")]
        //public async Task<CommonResponse<LeaveStatus>> GetLeaveStatusByIdAsync(int id)
        //{
        //    _logger.LogInformation($"Start GetEmployeeByIdAsync");
        //    try
        //    {
        //        var singleLeaveStatus = await _leaveStatusService.GetLeaveStatusByIdAsync(id);
        //        if (singleLeaveStatus == null)
        //        {
        //            _logger.LogInformation($"Start GetEmployeeByIdAsync null");
        //            //no salutions found
        //            return this.CreateResponse<LeaveStatus>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

        //        }
        //        _logger.LogInformation($"Get the values of GetEmployeeByIdAsync");
        //        _logger.LogInformation($"End GetEmployeeByIdAsync");
        //        //Salutions found
        //        return this.CreateResponse<LeaveStatus>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", singleLeaveStatus);
        //        // return this.CreateResponse<IEnumerable<Employee>>(StatusCode.Status200K, "Success", employee);
        //    }
        //    catch (Exception ex)
        //    {
        //        //error occured
        //        _logger.LogError(ex, "An error occured while retrieving all salutions");
        //        return this.CreateResponse<LeaveStatus>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
        //    }

        //}

        //[HttpGet("GetLeaveStatusByCode")]
        //public async Task<ActionResult<CommonResponse<LeaveStatus>>> GetLeaveStatusByCodeAsync([FromQuery] string leaveStatusNameCode)
        //{
        //    _logger.LogInformation($"Start GetLeaveStatusByCode");
        //    try
        //    {
        //        var singleLeaveStatus = await _leaveStatusService.GetLeaveStatusByCodeAsync(leaveStatusNameCode);
        //        if (singleLeaveStatus == null)
        //        {
        //            _logger.LogInformation($"Start GetEmployeeByIdAsync null");
        //            //no salutions found
        //            return this.CreateResponse<LeaveStatus>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

        //        }
        //        _logger.LogInformation($"Get the values of GetEmployeeByIdAsync");
        //        _logger.LogInformation($"End GetEmployeeByIdAsync");
        //        //Salutions found
        //        return this.CreateResponse<LeaveStatus>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", singleLeaveStatus);
        //        // return this.CreateResponse<IEnumerable<Employee>>(StatusCode.Status200K, "Success", employee);
        //    }
        //    catch (Exception ex)
        //    {
        //        //error occured
        //        _logger.LogError(ex, "An error occured while retrieving all salutions");
        //        return this.CreateResponse<LeaveStatus>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
        //[HttpPost("UpdateLeaveStatusAsync")]
        //public async Task<ActionResult<LeaveStatus>> UpdateLeaveStatusAsync([FromBody] LeaveStatus leaveStatus)
        //{
        //    if (leaveStatus == null)
        //    {
        //        return BadRequest("LeaveStatus object is null");
        //    }

        //    try
        //    {
        //        var leaveStatusUpdated = await _leaveStatusService.UpdateLeaveStatusAsync(leaveStatus);
        //        return Ok(leaveStatusUpdated);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred: {ex.Message}");
        //    }


        //}
    }
}

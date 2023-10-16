using Leave.EmailTemplate;
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
        private readonly GenericEmail _genericEmail;
        private readonly ILogger<EmployeeController> _logger;

        //private readonly IEmployeeLeaveService _employeeLeaveService;
        public AppliedLeaveController(GenericEmail genericEmail,IAppliedLeaveService leaveService, ILogger<EmployeeController> logger)
        {

            
            _leaveService = leaveService;
            _logger = logger;
            _genericEmail = genericEmail;
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

                var previousAppliedLeaves =  await _leaveService.GetUnApprovedAppliedLeavesAsync(leave);

                if (previousAppliedLeaves.Count > 0)
                {
                    return this.CreateResponse<ActionResult<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "Already applied for the leave and approval is pending");
                }



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
                if (newAppliedLeaveCreated.IsApproved)
                {
                    return this.CreateResponse<ActionResult<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest, "Leave is already approved. Cannot approve again.");
                }
                if (newAppliedLeaveCreated.IsRejected)
                {
                    return this.CreateResponse<ActionResult<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest, "Leave is already rejected. Cannot reject again.");
                }
                //Salutions found
                var appliedLeaveTypeId = newAppliedLeaveCreated.appliedLeaveTypeId;

                DateTime currentDateTime = DateTime.Now;
                string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                var body = "";
                body += "<p>Please click one of the following buttons:</p>";
                body += $"<a href='http://localhost:5024/api/appliedLeave/UpdateIsApprovedAsync/{appliedLeaveTypeId}/true' style='display: inline-block; background-color: green; color: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Approve</a>";
                body += $"<a href='http://localhost:5024/api/appliedLeave/UpdateIsRejectedAsync/{appliedLeaveTypeId}/true' style='display: inline-block; background-color: red; color: white; padding: 5px 10px; text-align: center; text-decoration: none;'>Reject</a>";

                await _genericEmail.SendEmailAsync("ved.thakur@wonderbiz.in", "Leave Approval", body);

                return this.CreateResponse<ActionResult<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", newAppliedLeaveCreated);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while creating new leave request");
                return this.CreateResponse<ActionResult<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message  + ex.InnerException);
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
        //updateleavestatusasync

        [HttpPut("UpdateLeaveStatusAsync/{appliedLeaveTypeId}/{leaveStatusId}")]

        public async Task<CommonResponse<ActionResult<AppliedLeave>>> UpdateLeaveStatusAsync(long appliedLeaveTypeId, int leaveStatusId)
        {
            _logger.LogInformation($"Start UpdateAppliedLeave");


            //return Ok(result);
            try
            {
                var updatedLeaveStatus = await _leaveService.UpdateLeaveStatusAsync(appliedLeaveTypeId, leaveStatusId);
                if (updatedLeaveStatus == null)
                {
                    _logger.LogInformation($"Start UpdateAppliedLeave null");
                    //no salutions found

                    return this.CreateResponse<ActionResult<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");
                    
                }
                _logger.LogInformation($"Get the values of GetEmployeeByIdAsync");
                _logger.LogInformation($"End GetEmployeeByIdAsync");
                //Salutions found
                return this.CreateResponse<ActionResult<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", updatedLeaveStatus);
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
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success");
            }
            catch (Exception ex)
            {
                // Error occurred during deletion
                _logger.LogError(ex, "An error occurred while deleting the applied leave");
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        // Update IsRejected
        [HttpGet("UpdateIsRejectedAsync/{appliedLeaveTypeId}/{isRejected}")]
        public async Task<ActionResult<CommonResponse<AppliedLeave>>> UpdateIsRejectedAsync(long appliedLeaveTypeId, bool isRejected)
        {
            try
            {
                var updatedLeave = await _leaveService.UpdateIsRejectedAsync(appliedLeaveTypeId, isRejected);
                if (updatedLeave == null)
                {
                    return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");
                }

                _logger.LogInformation($"End DeleteAppliedLeave");
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Leave Rejected");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the applied leave");
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Update IsApproved
        [HttpGet("UpdateIsApprovedAsync/{appliedLeaveTypeId}/{isApproved}")]
        public async Task<ActionResult<CommonResponse<AppliedLeave>>> UpdateIsApprovedAsync([FromRoute] long appliedLeaveTypeId, [FromRoute] bool isApproved)
        {
            try
            {
                //var singleAppliedLeave = await _leaveService.GetAppliedLeaveByIdAsync(appliedLeaveTypeId);
                //if (singleAppliedLeave.IsApproved) {
                //    return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status208AlreadyReported, "Leave already approve");
                //}



                var updatedLeave = await _leaveService.UpdateIsApprovedAsync(appliedLeaveTypeId, isApproved);
                if (updatedLeave == null)
                {
                    // Leave not found
                    return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");
                }

                // Successful deletion
                _logger.LogInformation($"End DeleteAppliedLeave");
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Leave Approved");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the applied leave");
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Update IsApproved
        [HttpPut("UpdateIsApprovedCancelAsync/{appliedLeaveTypeId}/{isApproved}")]
        public async Task<ActionResult<CommonResponse<AppliedLeave>>> UpdateIsApprovedCancelAsync([FromRoute] long appliedLeaveTypeId, [FromRoute] bool isApproved)
        {
            try
            {
                var updatedLeave = await _leaveService.UpdateIsApprovedCancelAsync(appliedLeaveTypeId, isApproved);
                if (updatedLeave == null)
                {
                    // Leave not found
                    return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");
                }

                // Successful deletion
                _logger.LogInformation($"End DeleteAppliedLeave");
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the applied leave");
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

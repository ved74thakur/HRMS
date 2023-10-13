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
        public AppliedLeaveController(IAppliedLeaveService leaveService, ILogger<EmployeeController> logger, GenericEmail genericEmail)
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
                var appliedLeaveTypeId = newAppliedLeaveCreated.appliedLeaveTypeId;
               
                try
                {
                    await _genericEmail.SendEmailAsync("ved.thakur@wonderbiz.in", // Primary recipient
                            "Vacation Leave",
                            "I am a going on vacation today",  appliedLeaveTypeId);
                           
                }
                catch (Exception emailEx)
                {
                    // Handle email sending exceptions, log them, or take appropriate action.
                    _logger.LogError(emailEx, "An error occurred while sending the email.");
                }
                _logger.LogInformation($"Get the values of AddAppliedLeave");
                _logger.LogInformation($"End CreateAppliedLeave");
                //Salutions found

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
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status204NoContent, "Success");
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
            var selectedAppliedLeave = await _leaveService.GetAppliedLeaveByIdAsync(appliedLeaveTypeId);
            if (selectedAppliedLeave != null && selectedAppliedLeave.IsRejected)
            {
                
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status208AlreadyReported, "Leave Already Rejected");
            }
            else if (selectedAppliedLeave != null && selectedAppliedLeave.IsApproved)
            {
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest, "Leave already approved. Cannot be rejected");
            }
            try
            {
                var updatedLeave = await _leaveService.UpdateIsRejectedAsync(appliedLeaveTypeId, isRejected);
                if (updatedLeave == null)
                {
                    return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");
                }

                _logger.LogInformation($"End UpdateIsRejectedAsync");
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status204NoContent, "Leave Rejected");
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
            //add that anzar bhai wala validation
            try
            {

                var selectedAppliedLeave = await _leaveService.GetAppliedLeaveByIdAsync(appliedLeaveTypeId);

                if (selectedAppliedLeave != null && selectedAppliedLeave.IsApproved)
                {
                    // Leave not found
                    return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status208AlreadyReported, "Leave Already Approved");
                }
                else if(selectedAppliedLeave != null && selectedAppliedLeave.IsRejected) {
                    return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest, "Leave Already Rejected. It cannot be approved");
                }

                var updatedLeave = await _leaveService.UpdateIsApprovedAsync(appliedLeaveTypeId, isApproved); //ERROR CHE
                //if (updatedLeave != null && updatedLeave.IsApproved)
                //{
                    // Leave not found
                    //return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");
                //}

                // Successful deletion
                _logger.LogInformation($"End UpdateIsApprovedAsync");
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status204NoContent, "Leave Approved");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the applied leave");
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
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status204NoContent, "Success");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the applied leave");
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

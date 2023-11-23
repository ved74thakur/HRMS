using Leave.EmailTemplate;
using leaveApplication2.Dtos;

using leaveApplication2.Models;
using leaveApplication2.Other;
using leaveApplication2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using System.Linq.Expressions;

namespace leaveApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppliedLeaveController : ControllerBase
    {
        
        private readonly IAppliedLeaveService _leaveService;
        private readonly IEmailService _emailService;
        private readonly IEmployeeService _employeeService;
        private readonly GenericEmail _genericEmail;
        private readonly ILogger<EmployeeController> _logger;

        //private readonly IEmployeeLeaveService _employeeLeaveService;
        public AppliedLeaveController(GenericEmail genericEmail,IAppliedLeaveService leaveService, ILogger<EmployeeController> logger, IEmployeeService employeeService, IEmailService emailService)
        {

            
            _leaveService = leaveService;
            _logger = logger;
            _genericEmail = genericEmail;
            _employeeService = employeeService;
            _emailService = emailService;
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
                    return this.CreateResponse<IEnumerable<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No Employees Found.");

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
        //getAllAppliedLeaveByEmployeeId
        [HttpGet("GetAppliedLeavesByEmpIdAsync/{employeeId}")]
        public async Task<CommonResponse<IEnumerable<AppliedLeaveDTO>>> GetAppliedLeavesAsync(long employeeId)
        {
            _logger.LogInformation($"Start GetAllEmployeesLeaves");
            try
            {
                Expression<Func<AppliedLeave, bool>> filter = la => la.employeeId == employeeId;

               
                var leaves = await _leaveService.GetAppliedLeavesAsync(filter);
                if (leaves == null)
                {
                    _logger.LogInformation($"Start GetAllEmployeesLeaves null");
                    //no salutions found
                    return this.CreateResponse<IEnumerable<AppliedLeaveDTO>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No Employee found.");

                    //this.CreateResponse<Employee> (,)
                }
                _logger.LogInformation($"Get the values of GetAllEmployeeLeavesAsync");
                _logger.LogInformation($"End GetAllEmployeeLeavesAsync");
                //Salutions found

                return this.CreateResponse<IEnumerable<AppliedLeaveDTO>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", leaves);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<IEnumerable<AppliedLeaveDTO>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        //getAppliedLeavesOfAllEmployeesMappedUnderReportingId
        [HttpGet("GetAppliedLeavesByReportingPersonIdAsync/{employeeId}")]
        public async Task<CommonResponse<IEnumerable<AppliedLeaveDTO>>>GetAppliedLeavesByReportingPersonIdAsync(long employeeId)
        {
            _logger.LogInformation("Start GetAppliedLeavesByReportingPersonIdAsync");
            try
            {
                // Retrieve employees by reportingPersonId
                Expression<Func<Employee, bool>> filter = emp => emp.ReportingPersonId == employeeId;
                var employees = await _employeeService.GetEmployeesAsync(filter);

                if (employees == null)
                {
                    _logger.LogInformation("Start GetAppliedLeavesByReportingPersonIdAsync - No employees found.");
                    return this.CreateResponse<IEnumerable<AppliedLeaveDTO>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No employees found.", null);
                }

                // Extract employeeIds from the list of employees
                var employeeIds = employees.Select(emp => emp.employeeId).ToList();
               // employeeIds.Add(employeeId);    
                // Retrieve applied leaves for the selected employees
                Expression<Func<AppliedLeave, bool>> leavesFilter = la => employeeIds.Contains(la.employeeId);
                var leaves = await _leaveService.GetAppliedLeavesAsync(leavesFilter);

                _logger.LogInformation("Get the values of GetAppliedLeavesByReportingPersonIdAsync");
                _logger.LogInformation("End GetAppliedLeavesByReportingPersonIdAsync");

                return this.CreateResponse<IEnumerable<AppliedLeaveDTO>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", leaves);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving applied leaves by reportingPersonId");
                return this.CreateResponse<IEnumerable<AppliedLeaveDTO>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message, null);
            }
        }


        //Create leaves
        [HttpPost("CreateAppliedLeaveAsync")]
        public async Task<CommonResponse<ActionResult<AppliedLeave>>> CreateAppliedLeaveAsync(AppliedLeave leave)
        {
            _logger.LogInformation("Start CreateAppliedLeave");

            try
            {
               
                var newAppliedLeaveCreated = await _leaveService.CreateAppliedLeave(leave);

                if (newAppliedLeaveCreated == null)
                {
                    _logger.LogInformation("Start AddAppliedLeave null");
                    return this.CreateResponse<ActionResult<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutations found.");
                }

                _logger.LogInformation("Get the values of AddAppliedLeave");
                _logger.LogInformation("End CreateAppliedLeave");

                //if (newAppliedLeaveCreated.LeaveStatus.LeaveStatusCode == "APP")
                //{
                //    var approveEncryption = EncryptionHelper.Encrypt(newAppliedLeaveCreated.appliedLeaveTypeId + "|" + "APR" + "|" + 4);
                //    var rejectEncryption = EncryptionHelper.Encrypt(newAppliedLeaveCreated.appliedLeaveTypeId + "|" + "REJ" + "|" + 4);
                //}


                //await 

                await _emailService.SendLeaveApprovalEmail(newAppliedLeaveCreated);

                return this.CreateResponse<ActionResult<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Leave Applied Successfully", newAppliedLeaveCreated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new leave request");
                return this.CreateResponse<ActionResult<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message + ex.InnerException);
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
                return this.CreateResponse<ActionResult<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Leave updated successfully", updatedLeave);
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

        //[HttpPut("UpdateLeaveStatusAsync/{appliedLeaveTypeId}/{leaveStatusId}")]

        //public async Task<CommonResponse<ActionResult<AppliedLeave>>> UpdateLeaveStatusAsync(long appliedLeaveTypeId, int leaveStatusId)
        //{
        //    _logger.LogInformation($"Start UpdateAppliedLeave");


        //    //return Ok(result);
        //    try
        //    {
        //        var updatedLeaveStatus = await _leaveService.UpdateLeaveStatusAsync(appliedLeaveTypeId, leaveStatusId);
        //        if (updatedLeaveStatus == null)
        //        {
        //            _logger.LogInformation($"Start UpdateAppliedLeave null");
        //            //no salutions found

        //            return this.CreateResponse<ActionResult<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");
                    
        //        }
        //        _logger.LogInformation($"Get the values of GetEmployeeByIdAsync");
        //        _logger.LogInformation($"End GetEmployeeByIdAsync");
        //        //Salutions found
        //        return this.CreateResponse<ActionResult<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", updatedLeaveStatus);
        //        // return this.CreateResponse<IEnumerable<Employee>>(StatusCode.Status200K, "Success", employee);
        //    }
        //    catch (Exception ex)
        //    {
        //        //error occured
        //        _logger.LogError(ex, "An error occured while retrieving all salutions");
        //        return this.CreateResponse<ActionResult<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
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


        //cancel applied leave
        [HttpGet("CancelAppliedLeaveByIdAsync/{id}")]
        public async Task<CommonResponse<AppliedLeave>> CancelAppliedLeaveByIdAsync(long id)
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
                _logger.LogInformation($"Start CancelAppliedLeaveByIdAsync");
                var cancelledAppliedLeave = await _leaveService.CancelAppliedLeaveByIdAsync(id);
                _logger.LogInformation($"End GetEmployeeByIdAsync");
                //Salutions found
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", cancelledAppliedLeave);
                // return this.CreateResponse<IEnumerable<Employee>>(StatusCode.Status200K, "Success", employee);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        // Update IsRejected
        [HttpGet("UpdateIsRejectedAsync/{appliedLeaveTypeId}/{isRejected}")]
        public async Task<ActionResult<CommonResponse<AppliedLeave>>> UpdateIsRejectedAsync(long appliedLeaveTypeId, bool isRejected)
        {
            try
            {
                var existingLeave = await _leaveService.GetAppliedLeaveByIdAsync(appliedLeaveTypeId);
                if (existingLeave == null)
                {
                    return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "Leave not found.");
                }

                if (existingLeave.IsRejected)
                {
                    // Leave is already approved
                    return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest, "Leave is already rejected.");
                }
                var updatedLeave = await _leaveService.UpdateIsRejectedAsync(appliedLeaveTypeId, isRejected);
                if (updatedLeave == null)
                {
                    return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");
                }

                _logger.LogInformation($"End DeleteAppliedLeave");
                await _emailService.SendLeaveRejectedEmail(updatedLeave);
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
                var existingLeave = await _leaveService.GetAppliedLeaveByIdAsync(appliedLeaveTypeId);
                if (existingLeave == null)
                {
                    return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "Leave not found.");
                }

                if (existingLeave.IsApproved)
                {
                    // Leave is already approved
                    return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest, "Leave is already approved.");
                }


                var updatedLeave = await _leaveService.UpdateIsApprovedAsync(appliedLeaveTypeId, isApproved);
                if (updatedLeave == null)
                {
                    // Leave not found
                    return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "Leave not found.");
                }

                // Successful deletion
                _logger.LogInformation($"End DeleteAppliedLeave");
                await _emailService.SendLeaveApprovedEmail(updatedLeave);
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Leave Approved");
                // add approved email
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the applied leave");
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //amit upodate method
        [HttpGet("UpdateIsApprovedEmailAsync/{appliedLeaveTypeId}/{isApproved}")]
        public async Task<ActionResult<CommonResponse<AppliedLeave>>> UpdateIsApprovedEmailAsync([FromRoute] long appliedLeaveTypeId, [FromRoute] bool isApproved)
        {
            try
            {
                var updateStatus = await UpdateUpdateStatus(appliedLeaveTypeId, isApproved);

                return Ok(updateStatus.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the applied leave");
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public async Task<CommonResponse<AppliedLeave>> UpdateUpdateStatus([FromRoute] long appliedLeaveTypeId, [FromRoute] bool isApproved)
        {

            try
            {

                var existingLeave = await _leaveService.GetAppliedLeaveByIdAsync(appliedLeaveTypeId);
                if (existingLeave == null)
                {
                    return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "Leave not found.");
                }

                if (existingLeave.IsApproved)
                {
                    // Leave is already approved
                    return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest, "Leave is already approved.");
                }


                var updatedLeave = await _leaveService.UpdateIsApprovedAsync(appliedLeaveTypeId, isApproved);
                if (updatedLeave == null)
                {
                    // Leave not found
                    return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "Leave not found.");
                }

                // Successful deletion
                _logger.LogInformation($"End DeleteAppliedLeave");
                await _emailService.SendLeaveApprovedEmail(updatedLeave);
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


        [HttpPost("AppliedLeaveUpdateStatusAsync")]
        public async Task<ActionResult<CommonResponse<AppliedLeave>>> AppliedLeaveUpdateStatusAsync(AppliedLeaveUpdateStatus appliedLeaveUpdateStatus)
        {

            try
            {
                
                var updatedLeave = await _leaveService.AppliedLeaveUpdateStatusAsync(appliedLeaveUpdateStatus);
                
                if (updatedLeave == null)
                {
                    // Leave not found
                    return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "Leave not found.");
                }

                // Successful deletion
                _logger.LogInformation($"End DeleteAppliedLeave");
                //leave cancel status

                switch (updatedLeave.LeaveStatus.LeaveStatusCode)
                {
                    //for rejecting
                    case "REJ":
                        await _emailService.SendLeaveRejectedEmail(updatedLeave);
                        return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Leave Rejected");
                        break;
                    case "APR":
                        await _emailService.SendLeaveApprovedEmail(updatedLeave);
                        return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Leave Approved");
                        break;
                    case "CAR":
                        await _emailService.SendCancelRequestEmail(updatedLeave);
                        return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Leave Cancel Request sent");
                        break;
                    default:
                        break;

                }
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Leave status updated successfully");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating leave Status");
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        [HttpGet("AppliedLeaveUpdateStatusByEmailAsync/{code}")]
        public async Task<ActionResult<CommonResponse<AppliedLeave>>> AppliedLeaveUpdateStatusByEmailAsync(string code)
       {

            try
            {
                // var approveEncryption = EncryptionHelper.Encrypt(createdLeave.appliedLeaveTypeId + "|" + "APR" + "|" + 4);

                var DecryptCode =    EncryptionHelper.Decrypt(code).Split('|');


                var appliedLeaveUpdateStatus = new AppliedLeaveUpdateStatus(
                        appliedLeaveTypeId: Convert.ToInt32( DecryptCode[0]),
                        statusCode:  Convert.ToString(DecryptCode[1]),
                        leaveAllocationId: Convert.ToInt32(DecryptCode[2])
                  );


                var updatedLeave = await _leaveService.AppliedLeaveUpdateStatusAsync(appliedLeaveUpdateStatus);
                
          
                if (updatedLeave == null)
                {
                    // Leave not found
                    return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "Leave not found.");
                }
                switch (updatedLeave.LeaveStatus.LeaveStatusCode)
                {
                    //for rejecting
                    case "REJ":
                        await _emailService.SendLeaveRejectedEmail(updatedLeave);
                        return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Leave Rejected");
                        break;
                    case "APR":
                        await _emailService.SendLeaveApprovedEmail(updatedLeave);
                        return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Leave Approved");
                        break;
                    case "CAR":
                        await _emailService.SendCancelRequestEmail(updatedLeave);
                        return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Leave Cancel Request sent");
                        break;  
                    default:
                        break;

                }

                // Successful deletion
                //_logger.LogInformation($"End DeleteAppliedLeave");
                //await _emailService.SendLeaveApprovedEmail(updatedLeave);
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, updatedLeave.LeaveStatus.LeaveStatusName);
                // add approved email
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the applied leave");
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

    }
}

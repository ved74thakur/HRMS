using leaveApplication2.Models;
using leaveApplication2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace leaveApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppliedLeaveCommentController : ControllerBase
    {
        private readonly ILogger<AppliedLeaveCommentController> _logger;
        private readonly IAppliedLeaveCommentService _appliedLeaveCommentService;
        public AppliedLeaveCommentController(ILogger<AppliedLeaveCommentController> logger, IAppliedLeaveCommentService appliedLeaveCommentService)
        {
            _appliedLeaveCommentService = appliedLeaveCommentService;
            _logger = logger;
        }

        [HttpGet("GetAppliedLeavesCommentAsync")]
        public async Task<CommonResponse<IEnumerable<AppliedLeaveComment>>> GetAppliedLeavesCommentAsync()
        {
            _logger.LogInformation($"Start GetAppliedLeavesCommentAsync");
            try
            {
                var comments = await _appliedLeaveCommentService.GetAppliedLeavesCommentAsync();
                if (comments == null)
                {
                    _logger.LogInformation($"Start GetAllEmployeesLeaves null");
                    //no salutions found
                    return this.CreateResponse<IEnumerable<AppliedLeaveComment>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No Employees Found.");

                    //this.CreateResponse<Employee> (,)
                }
                _logger.LogInformation($"Get the values of GetAllEmployeeLeavesAsync");
                _logger.LogInformation($"End GetAllEmployeeLeavesAsync");
                //Salutions found

                return this.CreateResponse<IEnumerable<AppliedLeaveComment>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", comments);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<IEnumerable<AppliedLeaveComment>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost("CreateAppliedLeaveComment")]
        public async Task<CommonResponse<ActionResult<AppliedLeaveComment>>> CreateAppliedLeaveComment(AppliedLeaveComment comment)
        {
            _logger.LogInformation("Start CreateAppliedLeaveComment");

            try
            {

                var appliedLeaveComment = await _appliedLeaveCommentService.CreateAppliedLeaveComment(comment);

                if (appliedLeaveComment == null)
                {
                    _logger.LogInformation("Start CreateAppliedLeaveComment null");
                    return this.CreateResponse<ActionResult<AppliedLeaveComment>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutations found.");
                }

                _logger.LogInformation("Get the values of CreateAppliedLeaveComment");
                _logger.LogInformation("End CreateAppliedLeaveComment");
                return this.CreateResponse<ActionResult<AppliedLeaveComment>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Leave Applied Successfully", appliedLeaveComment);
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new leave request");
                return this.CreateResponse<ActionResult<AppliedLeaveComment>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message + ex.InnerException);
            }
        }

        [HttpGet("GetAppliedLeavesCommentByIdAsync/{appliedLeaveTypeId}/{LeaveStatusId}")]
        public async Task<CommonResponse<IEnumerable<AppliedLeaveComment>>> GetAppliedLeavesCommentAsync(long appliedLeaveTypeId, int LeaveStatusId)
        {
            _logger.LogInformation($"Start GetAppliedLeavesCommentAsync");
            try
            {
                var comments = await _appliedLeaveCommentService.GetAppliedLeavesCommentAsync(appliedLeaveTypeId, LeaveStatusId);
                if (comments == null)
                {
                    _logger.LogInformation($"Start GetAllEmployeesLeaves null");
                    //no salutions found
                    return this.CreateResponse<IEnumerable<AppliedLeaveComment>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No Employees Found.");

                    //this.CreateResponse<Employee> (,)
                }
                _logger.LogInformation($"Get the values of GetAllEmployeeLeavesAsync");
                _logger.LogInformation($"End GetAllEmployeeLeavesAsync");
                //Salutions found

                return this.CreateResponse<IEnumerable<AppliedLeaveComment>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", comments);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<IEnumerable<AppliedLeaveComment>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


    }
}

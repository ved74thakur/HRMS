using leaveApplication2.Dtos;
using leaveApplication2.Models;
using leaveApplication2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace leaveApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveReportController : ControllerBase
    {
        private readonly IAppliedLeaveService _leaveService;
        private readonly ILogger<LeaveReportController> _logger;
        public LeaveReportController(IAppliedLeaveService leaveService, ILogger<LeaveReportController> logger)
        {
            _leaveService = leaveService;
            _logger = logger;
        }

        
        [HttpPost("GetLeavesReportAsync")]
        public async Task<CommonResponse<IEnumerable<AppliedLeaveDTO>>> GetLeavesReportAsync([FromBody]LeaveReport leaveReport)
        {
            _logger.LogInformation($"Start GetLeavesReportAsync");
            try
            {
                if (leaveReport.startDate>= leaveReport.endDate)
                {
                    return this.CreateResponse<IEnumerable<AppliedLeaveDTO>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "Invalid date range. Start date must be before end date.");
                }

                Expression<Func<AppliedLeave, bool>> filter = la => la.employeeId == leaveReport.employeeId
                                                             && la.LeaveStatusId == leaveReport.LeaveStatusId
                                                             && la.StartDate >= leaveReport.startDate
                                                             && la.EndDate <= leaveReport.endDate;


                var leavesReport = await _leaveService.GetAppliedLeavesAsync(filter);
                if (leavesReport == null || !leavesReport.Any())
                {
                    _logger.LogInformation($"Start GetLeavesReportAsync null");
                    //no salutions found
                    return this.CreateResponse<IEnumerable<AppliedLeaveDTO>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No Leaves found.");

                    //this.CreateResponse<Employee> (,)
                }
                _logger.LogInformation($"Get the values of GetLeavesReportAsync");
                _logger.LogInformation($"End GetLeavesReportAsync");
                //Salutions found

                return this.CreateResponse<IEnumerable<AppliedLeaveDTO>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", leavesReport);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<IEnumerable<AppliedLeaveDTO>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}

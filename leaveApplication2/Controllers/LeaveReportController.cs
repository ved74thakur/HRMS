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
        private readonly ILeaveReportService _leaveReportService;
        public LeaveReportController(IAppliedLeaveService leaveService, ILogger<LeaveReportController> logger, ILeaveReportService leaveReportService)
        {
            _leaveService = leaveService;
            _leaveReportService = leaveReportService;
            _logger = logger;
        }

        
        [HttpPost("GetLeavesReportAsync")]
        public async Task<CommonResponse<IEnumerable<AppliedLeave>>> GetLeavesReportAsync([FromBody]LeaveReport leaveReport)
        {
            _logger.LogInformation($"Start GetLeavesReportAsync");
            try
            {
         
                var leavesReport = await _leaveReportService.GetLeavesReportAsync(leaveReport);
                if (leavesReport == null)
                {
                    _logger.LogInformation($"Start GetLeavesReportAsync null");
                    //no salutions found
                    return this.CreateResponse<IEnumerable<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No Leaves found.");

                    //this.CreateResponse<Employee> (,)
                }
                _logger.LogInformation($"Get the values of GetLeavesReportAsync");
                _logger.LogInformation($"End GetLeavesReportAsync");
                //Salutions found

                return this.CreateResponse<IEnumerable<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", leavesReport);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<IEnumerable<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}

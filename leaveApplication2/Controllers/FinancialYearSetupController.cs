using leaveApplication2.Dtos;
using leaveApplication2.Models;
using leaveApplication2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace leaveApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialYearSetupController : ControllerBase
    {
        private readonly ILogger<LeaveAllocationController> _logger;
        private readonly ILeaveAllocationService _leaveAllocationService;
        private readonly IFinancialYearSetupService _financialYearSetupService;
        public FinancialYearSetupController(ILeaveAllocationService leaveAllocationService, ILogger<LeaveAllocationController> logger, IFinancialYearSetupService financialYearSetupService)
        {
            _leaveAllocationService = leaveAllocationService;
            _financialYearSetupService = financialYearSetupService;
            _logger = logger;
        }

        
        [HttpPost("CreateLeaveAllocationForAllLeaveTypes")]
        public async Task<CommonResponse<IEnumerable<LeaveAllocation>>> CreateLeaveAllocationForAllLeaveTypes([FromBody] LeaveAllocationRequestDto request)
        {
            _logger.LogInformation($"Start CreateLeaveAllocationForAllLeaveTypes");
            try
            {

                var leaveAllocations = await _leaveAllocationService.CreateLeaveAllocationForAllLeaveTypes(request.FinancialYear, request.LeaveTypeCounts);
                await _financialYearSetupService.CreateUpdatedEmployeeLeaveAsync();

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

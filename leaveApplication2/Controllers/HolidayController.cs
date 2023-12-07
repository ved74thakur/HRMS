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
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayService _holidayService;
        private readonly ILogger<HolidayController> _logger;

        public HolidayController(IHolidayService holidayService, ILogger<HolidayController> logger)
        {
            _holidayService = holidayService;
            _logger = logger;
        }
        //changes
        //Getting all holidays
        [HttpGet("GetHolidaysAsync")]
        public async Task<CommonResponse<IEnumerable<Holiday>>> GetHolidaysAsync()
        {
            _logger.LogInformation($"Start GetLeaveStatusesAsync");
            try
            {
                var holidays = await _holidayService.GetHolidaysAsync();
                if (holidays == null)
                {
                    _logger.LogInformation($"Start GetAllLeaveStatusesAsync null");
                    //no salutions found
                    return this.CreateResponse<IEnumerable<Holiday>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                    //this.CreateResponse<Employee> (,)
                }
                _logger.LogInformation($"Get the values of GetAllLeaveStatusesAsync");
                _logger.LogInformation($"End GetAllLeaveStatusesAsync");
                //Salutions found

                return this.CreateResponse<IEnumerable<Holiday>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", holidays);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<IEnumerable<Holiday>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        //GetAllHolidays
        [HttpGet("GetAllHolidaysAsync")]
        public async Task<CommonResponse<IEnumerable<Holiday>>> GetAllHolidaysAsync()
        {
            _logger.LogInformation($"Start GetAllHolidaysAsync");
            try
            {
                var holidays = await _holidayService.GetAllHolidaysAsync();
                if (holidays == null)
                {
                    _logger.LogInformation($"Start GetAllLeaveStatusesAsync null");
                    //no salutions found
                    return this.CreateResponse<IEnumerable<Holiday>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                    //this.CreateResponse<Employee> (,)
                }
                _logger.LogInformation($"Get the values of GetAllLeaveStatusesAsync");
                _logger.LogInformation($"End GetAllLeaveStatusesAsync");
                //Salutions found

                return this.CreateResponse<IEnumerable<Holiday>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", holidays);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<IEnumerable<Holiday>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        //getSingleHoliday

        [HttpGet("GetHolidayByIdAsync/{id}")]
        public async Task<CommonResponse<Holiday>> GetHolidayByIdAsync(int id)
        {
            _logger.LogInformation($"Start GetHolidayByIdAsync");
            try
            {
                var singleHoliday = await _holidayService.GetHolidayByIdAsync(id);
                if (singleHoliday == null)
                {
                    _logger.LogInformation($"Start GetEmployeeById null");
                    //no salutions found
                    return this.CreateResponse<Holiday>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                }
                _logger.LogInformation($"Get the values of GetEmployeeByIdAsync");
                _logger.LogInformation($"End GetEmployeeByIdAsync");
                //Salutions found
                return this.CreateResponse<Holiday>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", singleHoliday);
                // return this.CreateResponse<IEnumerable<Employee>>(StatusCode.Status200K, "Success", employee);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<Holiday>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        //Create holiday
        [HttpPost("CreateHoliday")]
        public async Task<CommonResponse<ActionResult<Holiday>>> CreateHoliday(Holiday holiday)
        {
            _logger.LogInformation($"Start CreateAppliedLeave");

            try
            {
                var newCreatedLeave = await _holidayService.CreateHoliday(holiday);
                //var newAppliedLeaveCreated = CreatedAtAction(nameof(GetEmployeeById), new { id = employee.employeeId }, employee);
                if (newCreatedLeave == null)
                {
                    _logger.LogInformation($"Start AddAppliedLeave null");
                    //no salutions found
                    return this.CreateResponse<ActionResult<Holiday>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                }
                _logger.LogInformation($"Get the values of AddAppliedLeave");
                _logger.LogInformation($"End CreateAppliedLeave");
                //Salutions found

                return this.CreateResponse<ActionResult<Holiday>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", newCreatedLeave);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while creating new leave request");
                return this.CreateResponse<ActionResult<Holiday>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        //update holiday
        [HttpPost("UpdateHoliday")]
        public async Task<CommonResponse<ActionResult<Holiday>>> UpdateHoliday(Holiday holiday)
        {
            _logger.LogInformation($"Start UpdateHoliday");

            try
            {
                var updatedHoliday = await _holidayService.UpdateHoliday(holiday);

                if (updatedHoliday == null)
                {
                    _logger.LogInformation($"Start UpdateHoliday null");
                    // No holiday found with the given Id.
                    return this.CreateResponse<ActionResult<Holiday>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No holiday found with the given Id.");
                }

                _logger.LogInformation($"Get the values of UpdateHoliday");
                _logger.LogInformation($"End UpdateHoliday");
                // Holiday updated successfully.
                return this.CreateResponse<ActionResult<Holiday>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", updatedHoliday);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the holiday");
                return this.CreateResponse<ActionResult<Holiday>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("DeleteHoliday/{id}")]
        public async Task<CommonResponse<ActionResult<Holiday>>> DeleteHoliday(int id)
        {
            _logger.LogInformation($"Start DeleteHoliday");

            try
            {
                var deletedHoliday = await _holidayService.DeleteHoliday(id);

                if (deletedHoliday == null)
                {
                    _logger.LogInformation($"Start DeleteHoliday null");
                    // No holiday found with the given Id.
                    return this.CreateResponse<ActionResult<Holiday>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No holiday found with the given Id.");
                }

                _logger.LogInformation($"Get the values of DeleteHoliday");
                _logger.LogInformation($"End DeleteHoliday");
                // Holiday deleted successfully.
                return this.CreateResponse<ActionResult<Holiday>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", deletedHoliday);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the holiday");
                return this.CreateResponse<ActionResult<Holiday>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}

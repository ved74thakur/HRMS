using Leave.EmailTemplate;
using leaveApplication2.Models;
using leaveApplication2.Models.leaveApplication2.Models;
using leaveApplication2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace leaveApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialYearController : ControllerBase
    {
        private readonly IFinancialYearService _financialYearService;
        private readonly ILogger<EmployeeController> _logger;

        //private readonly IEmployeeLeaveService _employeeLeaveService;
        public FinancialYearController(IFinancialYearService financialYearService, ILogger<EmployeeController> logger)
        {

            _financialYearService = financialYearService;
            _logger = logger;
        }

        
        [HttpGet("GetFinancialYearsAsync")]
        public async Task<CommonResponse<IEnumerable<FinancialYear>>> GetFinancialYearsAsync()
        {
            _logger.LogInformation($"Start GetFinancialYearsAsync");
            try
            {
                var financialYears = await _financialYearService.GetFinancialYearsAsync();
                if (financialYears == null)
                {
                    _logger.LogInformation($"Start GetFinancialYearsAsync null");
                    //no salutions found
                    return this.CreateResponse<IEnumerable<FinancialYear>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No FinancialYear Found.");

                    
                }
                _logger.LogInformation($"Get the values of GetFinancialYearsAsync");
                _logger.LogInformation($"End GetFinancialYearsAsync");
                //Salutions found

                return this.CreateResponse<IEnumerable<FinancialYear>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", financialYears);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<IEnumerable<FinancialYear>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet("GetFinancialYearByIdAsync/{financialYearId}")]
        public async Task<CommonResponse<FinancialYear>> GetFinancialYearByIdAsync(int financialYearId)
        {

            _logger.LogInformation($"Start GetFinancialYearByIdAsync");
            try
            {
                var singleFinancialYear = await _financialYearService.GetFinancialYearByIdAsync(financialYearId);
                if (singleFinancialYear == null)
                {
                    _logger.LogInformation($"Start GetFinancialYearByIdAsync null");
                    //no salutions found
                    return this.CreateResponse<FinancialYear>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                }
                _logger.LogInformation($"Get the values of GetFinancialYearByIdAsync");
                _logger.LogInformation($"End GetFinancialYearByIdAsync");
                //Salutions found
                return this.CreateResponse<FinancialYear>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", singleFinancialYear);
                // return this.CreateResponse<IEnumerable<Employee>>(StatusCode.Status200K, "Success", employee);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<FinancialYear>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost("CreateFinancialYearAsync")]
        public async Task<CommonResponse<ActionResult<FinancialYear>>> CreateFinancialYearAsync(FinancialYear financialYear)
        {
            _logger.LogInformation($"Start CreateFinancialYearAsync");

            try
            {
                var newFinancialYearCreated = await _financialYearService.CreateFinancialYearAsync(financialYear);
                //var newAppliedLeaveCreated = CreatedAtAction(nameof(GetEmployeeById), new { id = employee.employeeId }, employee);
                if (newFinancialYearCreated == null)
                {
                    _logger.LogInformation($"Start CreateEmployeeLeaveAsync null");
                    //no salutions found
                    return this.CreateResponse<ActionResult<FinancialYear>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                }
                _logger.LogInformation($"Get the values of CreateEmployeeLeaveAsync");
                _logger.LogInformation($"End CreateEmployeeLeave");
                //Salutions found

                return this.CreateResponse<ActionResult<FinancialYear>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", newFinancialYearCreated);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while creating new leave request");
                return this.CreateResponse<ActionResult<FinancialYear>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpDelete("DeleteFinancialYearAsync/{financialYearId}")]
        public async Task<CommonResponse<ActionResult<FinancialYear>>> DeleteFinancialYearAsync(int financialYearId)
        {
            _logger.LogInformation($"Start DeleteFinancialYearAsync");

            try
            {
                var deletedFinancialYear = await _financialYearService.DeleteFinancialYearAsync(financialYearId);

                if (deletedFinancialYear == null)
                {
                    _logger.LogInformation($"Start DeleteFinancialYearAsync null");
                    // Financial year not found
                    return this.CreateResponse<ActionResult<FinancialYear>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "Financial year not found.");
                }

                _logger.LogInformation($"Financial year deleted");
                // Financial year deleted successfully
                return this.CreateResponse<ActionResult<FinancialYear>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Financial year deleted successfully", deletedFinancialYear);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the financial year");
                return this.CreateResponse<ActionResult<FinancialYear>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("UpdateFinancialYearAsync/{financialYearId}")]
        public async Task<CommonResponse<ActionResult<FinancialYear>>> UpdateFinancialYearAsync(int financialYearId)
        {
            _logger.LogInformation($"Start UpdateFinancialYearAsync");

            try
            {
                var updatedFinancialYear = await _financialYearService.UpdateFinancialYearAsync(financialYearId);
                //var newAppliedLeaveCreated = CreatedAtAction(nameof(GetEmployeeById), new { id = employee.employeeId }, employee);
                if (updatedFinancialYear == null)
                {
                    _logger.LogInformation($"Start UpdateFinancialYearAsync null");
                    //no salutions found
                    return this.CreateResponse<ActionResult<FinancialYear>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                }
                _logger.LogInformation($"Get the values of UpdateFinancialYearAsync");
                _logger.LogInformation($"End UpdateFinancialYearAsync");
                //Salutions found

                return this.CreateResponse<ActionResult<FinancialYear>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", updatedFinancialYear);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while creating new leave request");
                return this.CreateResponse<ActionResult<FinancialYear>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet("GetActiveFinancialYearsAsync")]
        public async Task<CommonResponse<IEnumerable<FinancialYear>>> GetActiveFinancialYearsAsync()
        {
            _logger.LogInformation($"Start GetActiveFinancialYearsAsync");
            try
            {
                Expression<Func<FinancialYear, bool>> filter = la => la.ActiveYear == true;
                var activeFinancialYears = await _financialYearService.GetActiveFinancialYearsAsync(filter);


                if (activeFinancialYears == null)
                {
                    _logger.LogInformation($"Start GetActiveFinancialYearsAsync null");
                    //no salutions found
                    return this.CreateResponse<IEnumerable<FinancialYear>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No active financialYear found.");

                    //this.CreateResponse<Employee> (,)
                }
                _logger.LogInformation($"Get the values of GetActiveFinancialYearsAsync");
                _logger.LogInformation($"End GetActiveFinancialYearsAsync");
                //Salutions found

                return this.CreateResponse<IEnumerable<FinancialYear>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", activeFinancialYears);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<IEnumerable<FinancialYear>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

    }
}

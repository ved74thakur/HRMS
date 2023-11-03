
using leaveApplication2.Models;
using leaveApplication2.Repostories;
using leaveApplication2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace leaveApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeLeaveController : ControllerBase
    {
        private readonly IEmployeeLeaveService _employeeLeaveService;

        private readonly ILogger<EmployeeLeaveController> _logger;


        public EmployeeLeaveController(IEmployeeLeaveService employeeLeaveService, ILogger<EmployeeLeaveController> logger)
        {
            _employeeLeaveService = employeeLeaveService;
            _logger = logger;
        }
        //EmployeeLeaves 
        //Getting all employeeLeaves
        [HttpGet]
        public async Task<CommonResponse<IEnumerable<EmployeeLeave>>> GetAllEmployeesLeaves()
        {
            _logger.LogInformation($"Start GetAllEmployeesLeaves");
            try
            {
                var employeesLeaves= await _employeeLeaveService.GetAllEmployeesLeaveAsync();
                if (employeesLeaves == null)
                {
                    _logger.LogInformation($"Start GetAllEmployeesLeaveAsync null");
                    //no salutions found
                    return this.CreateResponse<IEnumerable<EmployeeLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                    //this.CreateResponse<Employee> (,)
                }
                _logger.LogInformation($"Get the values of GetAllEmployeeAsync");
                _logger.LogInformation($"End GetAllEmployeeAsync");
                //Salutions found

                return this.CreateResponse<IEnumerable<EmployeeLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", employeesLeaves);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<IEnumerable<EmployeeLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        //creating employeeLeave inventort
        
        [HttpPost("CreateEmployeeLeave")]
        public async Task<CommonResponse<ActionResult<EmployeeLeave>>> CreateEmployeeLeave(EmployeeLeave leave)
        {
            _logger.LogInformation($"Start CreateEmployeeLeave");

            try
            {
                var newEmployeeLeaveCreated = await _employeeLeaveService.CreateEmployeeLeaveAsync(leave);
                //var newAppliedLeaveCreated = CreatedAtAction(nameof(GetEmployeeById), new { id = employee.employeeId }, employee);
                if (newEmployeeLeaveCreated == null)
                {
                    _logger.LogInformation($"Start CreateEmployeeLeaveAsync null");
                    //no salutions found
                    return this.CreateResponse<ActionResult<EmployeeLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                }
                _logger.LogInformation($"Get the values of CreateEmployeeLeaveAsync");
                _logger.LogInformation($"End CreateEmployeeLeave");
                //Salutions found

                return this.CreateResponse<ActionResult<EmployeeLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", newEmployeeLeaveCreated);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while creating new leave request");
                return this.CreateResponse<ActionResult<EmployeeLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        //getbyleavetypid
        [HttpGet("GetSingeEmployeeLeave/{id}")]
        public async Task<CommonResponse<EmployeeLeave>> GetSingeEmployeeLeave(long id)
        {
            _logger.LogInformation($"Start GetEmployeeByIdAsync");
            try
            {
                var singleEmployeeLeave = await _employeeLeaveService.GetEmployeeLeaveByIdAsync(id);
                if (singleEmployeeLeave == null)
                {
                    _logger.LogInformation($"Start GetEmployeeLeaveByIdAsync null");
                    //no salutions found
                    return this.CreateResponse<EmployeeLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                }
                _logger.LogInformation($"Get the values of GetEmployeeLeaveByIdAsync");
                _logger.LogInformation($"End GetEmployeeLeaveByIdAsync");
                //Salutions found
                return this.CreateResponse<EmployeeLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", singleEmployeeLeave);
                // return this.CreateResponse<IEnumerable<Employee>>(StatusCode.Status200K, "Success", employee);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<EmployeeLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        [HttpGet("GetEmployeeLeaveByEmployeeId/{employeeId}")]
        public async Task<CommonResponse<IReadOnlyCollection<EmployeeLeave>>> GetEmployeeLeaveByEmployeeId(long employeeId)
        {
            _logger.LogInformation($"Start GetEmployeeByIdAsync");
            try
            {

              


                var singleEmployeeLeave = await _employeeLeaveService.GetEmployeeLeaveByEmployeeId(employeeId);
                if (singleEmployeeLeave == null)
                {
                    _logger.LogInformation($"Start GetEmployeeLeaveByIdAsync null");
                    //no salutions found
                    return this.CreateResponse<IReadOnlyCollection<EmployeeLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                }
                _logger.LogInformation($"Get the values of GetEmployeeLeaveByIdAsync");
                _logger.LogInformation($"End GetEmployeeLeaveByIdAsync");
                //Salutions found
                return this.CreateResponse<IReadOnlyCollection<EmployeeLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", singleEmployeeLeave);
                // return this.CreateResponse<IEnumerable<Employee>>(StatusCode.Status200K, "Success", employee);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<IReadOnlyCollection<EmployeeLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPut("UpdateEmployeeLeaveAsync/{id}")]
        public async Task<CommonResponse<ActionResult<EmployeeLeave>>> UpdateEmployeeLeaveAsync(long id, EmployeeLeave employeeLeave )
        {
            _logger.LogInformation($"Start UpdateEmployeeLeaveAsync");


            //return Ok(result);
            try
            {
                var updateEmployeeLeave = await _employeeLeaveService.UpdateEmployeeLeaveAsync(id, employeeLeave);
                if (updateEmployeeLeave == null)
                {
                    _logger.LogInformation($"Start UpdateAppliedLeave null");
                    //no salutions found

                    return this.CreateResponse<ActionResult<EmployeeLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                }
                _logger.LogInformation($"Get the values of GetEmployeeByIdAsync");
                _logger.LogInformation($"End GetEmployeeByIdAsync");
                //Salutions found
                return this.CreateResponse<ActionResult<EmployeeLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", updateEmployeeLeave);
                // return this.CreateResponse<IEnumerable<Employee>>(StatusCode.Status200K, "Success", employee);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<ActionResult<EmployeeLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}


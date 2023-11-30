using leaveApplication2.Models;
using leaveApplication2.Repostories;
using leaveApplication2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Security.Claims;
using leaveApplication2.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http.HttpResults;
using leaveApplication2.Dtos;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;

namespace leaveApplication2.Controllers     
{
    [Route("api/[controller]")]
    [ApiController]
   
    //added authorization
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmailService _emailService;
        private readonly IAppliedLeaveService _leaveService;
        private readonly IConfiguration _configuration;
        //private readonly IEmailService _emailService;

        private readonly ILogger<EmployeeController> _logger;
        
        //private readonly IEmployeeLeaveService _employeeLeaveService;
        public EmployeeController(IEmployeeService employeeService, IAppliedLeaveService leaveService, ILogger<EmployeeController> logger, IConfiguration configuration, IEmailService emailService)
        {

            _employeeService = employeeService;
            _leaveService = leaveService;
            _logger  = logger;
            _emailService = emailService;
            
            //_employeeLeaveService = employeeLeaveService;

        }
        //Employees 
        //Getting all employees
        [Authorize]
        [HttpGet("GetEmployeesAsync")]
        public async Task<CommonResponse<IEnumerable<Employee>>> GetEmployeesAsync()
        {
            _logger.LogInformation($"Start GetAllEmployees");
            try
            {
                var employees = await _employeeService.GetEmployeesAsync();
                if( employees == null)
                {
                    _logger.LogInformation($"Start GetAllEmployees null");
                    //no salutions found
                    return this.CreateResponse<IEnumerable<Employee>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");
                    
                   //this.CreateResponse<Employee> (,)
                }
                _logger.LogInformation($"Get the values of GetAllEmployeeAsync");
                _logger.LogInformation($"End GetAllEmployeeAsync");
                //Salutions found
                    
                return this.CreateResponse<IEnumerable<Employee>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", employees);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<IEnumerable<Employee>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        //getting employee by id
        [Authorize]
        [HttpGet("GetEmployeeByIdAsync/{employeeId}")]
        public async Task<CommonResponse<Employee>> GetEmployeeByIdAsync(long employeeId)
        {

            _logger.LogInformation($"Start GetEmployeeByIdAsync");
            try
            {
                var singleEmployee = await _employeeService.GetEmployeeByIdAsync(employeeId);
                if (singleEmployee == null)
                {
                    _logger.LogInformation($"Start GetEmployeeByIdAsync null");
                    //no salutions found
                    return this.CreateResponse<Employee>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                }
                _logger.LogInformation($"Get the values of GetEmployeeByIdAsync");
                _logger.LogInformation($"End GetEmployeeByIdAsync");
                //Salutions found
                return this.CreateResponse<Employee>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", singleEmployee);
                // return this.CreateResponse<IEnumerable<Employee>>(StatusCode.Status200K, "Success", employee);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<Employee>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [Authorize]
        [HttpGet("GetEmployeeDetailsByIdAsync/{employeeId}")]
        public async Task<CommonResponse<EmployeeInfoDto>> GetEmployeeDetailsByIdAsync(long employeeId)
        {

            _logger.LogInformation($"Start GetEmployeeByIdAsync");
            try
            {
                var singleEmployee = await _employeeService.GetEmployeeDetailsByIdAsync(employeeId);
                if (singleEmployee == null)
                {
                    _logger.LogInformation($"Start GetEmployeeByIdAsync null");
                    //no salutions found
                    return this.CreateResponse<EmployeeInfoDto>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                }
                _logger.LogInformation($"Get the values of GetEmployeeByIdAsync");
                _logger.LogInformation($"End GetEmployeeByIdAsync");
                //Salutions found
                return this.CreateResponse<EmployeeInfoDto>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", singleEmployee);
                // return this.CreateResponse<IEnumerable<Employee>>(StatusCode.Status200K, "Success", employee);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<EmployeeInfoDto>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        //create employee
        [Authorize]
        [HttpPost("CreateEmployeeAsync")]
        public async Task<CommonResponse<ActionResult<Employee>>> CreateEmployeeAsync([FromBody] Employee employee)
        {
            _logger.LogInformation($"Start CreateNewEmployee");

            try
            {

                var newEmployeeCreated = await _employeeService.CreateEmployeeAsync(employee);
                //var newAppliedLeaveCreated = CreatedAtAction(nameof(GetEmployeeById), new { id = employee.employeeId }, employee);
                if (newEmployeeCreated == null)
                {
                    _logger.LogInformation($"Start AddAppliedLeave null");
                    //no salutions found
                    return this.CreateResponse<ActionResult<Employee>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                }
                _logger.LogInformation($"Get the values of AddAppliedLeave");
                _logger.LogInformation($"End CreateAppliedLeave");
                //Salutions found
                await _emailService.SendEmployeeCreatedEmail(employee);
                await _employeeService.CreateEmployeeReportingAsync(employee.employeeId, employee.ReportingPersonId ?? 0);
                

                return this.CreateResponse<ActionResult<Employee>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Employee Registered Successfully", newEmployeeCreated);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while creating new leave request");
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

                return this.CreateResponse<ActionResult<Employee>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, errorMessage);
            }

        }

        
        [HttpPost("UpdateEmployeeAsync")]
        public async Task<CommonResponse<ActionResult<Employee>>> UpdateEmployeeAsync([FromBody] Employee employee)
        {
            _logger.LogInformation($"Start UpdateEmployeeByIdAsync");


            //return Ok(result);
            try
            {
               
                var updatedEmployee = await _employeeService.UpdateEmployeeAsync(employee);

                _logger.LogInformation($"End GetEmployeeByIdAsync");
                //Salutions found
                return this.CreateResponse<ActionResult<Employee>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", updatedEmployee);
                // return this.CreateResponse<IEnumerable<Employee>>(StatusCode.Status200K, "Success", employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while creating new leave request");
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

                return this.CreateResponse<ActionResult<Employee>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, errorMessage);
            }
        }
        [Authorize]
        [HttpDelete("DeleteEmployeeAsync/{id}")]
        public async Task<CommonResponse<Employee>> DeleteEmployeeRegistrationAsync([FromRoute] long id)
        {
            var selectedEmployeeRegistration = await _employeeService.GetEmployeeByIdAsync(id);
            if (selectedEmployeeRegistration == null)
            {
                _logger.LogInformation($"Start GetSingleAppliedLeave null");
                //no salutions found
                return this.CreateResponse<Employee>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");
            }
            _logger.LogInformation($"Start DeleteAppliedLeave");
            try
            {
                await _employeeService.DeleteEmployeeAsync(id);

                // Successful deletion
                _logger.LogInformation($"End DeleteAppliedLeave");
                return this.CreateResponse<Employee>(Microsoft.AspNetCore.Http.StatusCodes.Status204NoContent, "Success");
            }
            catch (Exception ex)
            {
                // Error occurred during deletion
                _logger.LogError(ex, "An error occurred while deleting the applied leave");
                return this.CreateResponse<Employee>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("EmployeeLoginAsync")]
        public async Task<CommonResponse<object>> EmployeeLoginAsync([FromBody] EmployeeLoginDto employee)
        {
            var selectedEmployee = await _employeeService.EmployeeLoginAsync(employee);
            if (selectedEmployee == null)
            {
                _logger.LogInformation($"Start EmployeeLoginAsync null");
                //no salutions found
                return this.CreateResponse<object>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");
            }
            _logger.LogInformation($"Start DeleteAppliedLeave");
            try
            {
        
                // Successful deletion
                _logger.LogInformation($"End DeleteAppliedLeave");
                return this.CreateResponse<object>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", selectedEmployee);
            }
            catch (Exception ex)
            {
                // Error occurred during deletion
                _logger.LogError(ex, "An error occurred while deleting the applied leave");
                return this.CreateResponse<object>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        [HttpPost("UpdateEmployeePassword")]
        public async Task<CommonResponse<ActionResult<Employee>>> UpdateEmployeePasswordAsync([FromBody] EmployeeLoginDto employee)
        {
            _logger.LogInformation($"Start UpdateEmployeeByIdAsync");


            //return Ok(result);
            try
            {

                var updatedEmployee = await _employeeService.UpdateEmployeePasswordAsync(employee.employeeId,employee);

                _logger.LogInformation($"End UpdateEmployeePasswordAsync");
                //Salutions found
                return this.CreateResponse<ActionResult<Employee>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", updatedEmployee);
                // return this.CreateResponse<IEnumerable<Employee>>(StatusCode.Status200K, "Success", employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while creating new leave request");
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

                return this.CreateResponse<ActionResult<Employee>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, errorMessage);
            }
        }

        [Authorize]
        [HttpGet("GetEmployeesByReportingIdAsync/{employeeId}")]
        public async Task<CommonResponse<IEnumerable<Employee>>> GetEmployeesByReportingPersonIdAsync(long employeeId)
        {
            _logger.LogInformation($"Start GetEmployeesByReportingPersonIdAsync");
            try
            {
                Expression<Func<Employee, bool>> filter = emp => emp.ReportingPersonId == employeeId;
                var employees = await _employeeService.GetEmployeesAsync(filter);

            
                if (employees == null)
                {
                    _logger.LogInformation($"Start GetEmployeesAsync null");
                    //no salutions found
                    return this.CreateResponse<IEnumerable<Employee>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No Employee found.");

                    //this.CreateResponse<Employee> (,)
                }
                _logger.LogInformation($"Get the values of GetEmployeesAsync");
                _logger.LogInformation($"End GetEmployeesAsync");
                //Salutions found

                return this.CreateResponse<IEnumerable<Employee>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", employees);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<IEnumerable<Employee>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet("GetEmployeesWithReportingId")]
        public async Task<CommonResponse<IEnumerable<Employee>>> GetEmployeesWithReportingId()
        {
            _logger.LogInformation($"Start GetEmployeesByReportingPersonIdAsync");
            try
            {
                Expression<Func<Employee, bool>> filter = emp => emp.ReportingPersonId != null;
                var employees = await _employeeService.GetEmployeesAsync(filter);


                if (employees == null)
                {
                    _logger.LogInformation($"Start GetEmployeesAsync null");
                    //no salutions found
                    return this.CreateResponse<IEnumerable<Employee>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No Employee found.");

                    //this.CreateResponse<Employee> (,)
                }
                _logger.LogInformation($"Get the values of GetEmployeesAsync");
                _logger.LogInformation($"End GetEmployeesAsync");
                //Salutions found

                return this.CreateResponse<IEnumerable<Employee>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", employees);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<IEnumerable<Employee>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        //including that employee itself
        //[HttpGet("GetEmployeesByReportingPersonIdAsync/{employeeId}")]
        //public async Task<CommonResponse<IEnumerable<Employee>>> GetEmployeesByReportingPersonIdAsync(long employeeId)
        //{
        //    _logger.LogInformation("Start GetEmployeesByReportingPersonIdAsync");
        //    try
        //    {
        //        // Retrieve the selected employee
        //        var selectedEmployee = await _employeeService.GetEmployeeByIdAsync(employeeId);

        //        if (selectedEmployee == null)
        //        {
        //            _logger.LogInformation("Start GetEmployeesByReportingPersonIdAsync - Selected employee not found");
        //            return this.CreateResponse(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "Selected employee not found.", null);
        //        }

        //        // Retrieve employees by reportingPersonId
        //        Expression<Func<Employee, bool>> filter = emp => emp.ReportingPersonId == employeeId;
        //        var employees = await _employeeService.GetEmployeesAsync(filter);

        //        _logger.LogInformation("Get the values of GetEmployeesByReportingPersonIdAsync");
        //        _logger.LogInformation("End GetEmployeesByReportingPersonIdAsync");

        //        // Combine the selected employee and related employees into a single list
        //        var result = new List<Employee> { selectedEmployee };
        //        result.AddRange(employees);

        //        return this.CreateResponse(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", result);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while retrieving employees by reportingPersonId");
        //        return this.CreateResponse(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message, null);
        //    }
        //}


        /*
        //controller for leaves
        [HttpGet("GetAll
        ")]
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
        [HttpPost("CreateAppliedLeave")]
        public async Task<CommonResponse<ActionResult<AppliedLeave>>> CreateAppliedLeave(AppliedLeave leave)
        {
            _logger.LogInformation($"Start CreateAppliedLeave");

            try
            {
                var newAppliedLeaveCreated =  await _leaveService.CreateAppliedLeave(leave);
                //var newAppliedLeaveCreated = CreatedAtAction(nameof(GetEmployeeById), new { id = employee.employeeId }, employee);
                if (newAppliedLeaveCreated == null)
                {
                    _logger.LogInformation($"Start AddAppliedLeave null");
                    //no salutions found
                    return this.CreateResponse<ActionResult<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                }
                _logger.LogInformation($"Get the values of AddAppliedLeave");
                _logger.LogInformation($"End CreateAppliedLeave");
                //Salutions found

                return this.CreateResponse<ActionResult<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", newAppliedLeaveCreated);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while creating new leave request");
                return this.CreateResponse<ActionResult<AppliedLeave>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        //update leave
        [HttpPut("UpdateAppliedLeave{id}")]
        public async Task<CommonResponse<ActionResult<AppliedLeave>>> UpdateAppliedLeave(int id, AppliedLeave leave)
        {
            _logger.LogInformation($"Start UpdateAppliedLeave");
            
            
            //return Ok(result);
            try
            {
                var updatedLeave = await _leaveService.UpdateAppliedLeave(id, leave);
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


        //getSingleLeave

        [HttpGet("GetSingleAppliedLeave{id}")]
        public async Task<CommonResponse<AppliedLeave>> GetSingleAppliedLeave(long id)
        {
            _logger.LogInformation($"Start GetSingleAppliedLeave");
            try {
                var singleAppliedLeave = await _leaveService.GetSingleAppliedLeave(id);
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
            } catch (Exception ex) {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        //delete leave
        
        [HttpDelete("DeleteAppliedLeave/{id}")]
        public async Task<CommonResponse<AppliedLeave>> DeleteAppliedLeave([FromRoute]long id)
        {
            var selectedAppliedLeave = await _leaveService.GetSingleAppliedLeave(id);
            if (selectedAppliedLeave == null)
            {
                _logger.LogInformation($"Start GetSingleAppliedLeave null");
                //no salutions found
                return this.CreateResponse<AppliedLeave>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");
            }
            _logger.LogInformation($"Start DeleteAppliedLeave");
            try
            {
                await _leaveService.DeleteAppliedLeave(id);

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
        
        [HttpGet("GetAllEmployeesLeaves")]
        public async Task<CommonResponse<IEnumerable<EmployeeLeave>>> GetAllEmployeesLeaves()
        {
            _logger.LogInformation($"Start GetAllEmployeesLeaves");
            try
            {
                var employeesLeaves = await _employeeLeaveService.GetAllEmployeesLeaveAsync();
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
        
        
        [HttpPost]
        public async Task<CommonResponse<ActionResult<Employee>>> CreateEmployee(Employee employee)
        {
            _logger.LogInformation($"Start CreateEmployee");
            
            try {
                await _employeeService.CreateEmployeeAsync(employee);
                var newEmployeeCreated = CreatedAtAction(nameof(GetEmployeeById), new { id = employee.employeeId }, employee);
                if(newEmployeeCreated == null)
                {
                    _logger.LogInformation($"Start CreateEmployee null");
                    //no salutions found
                    return this.CreateResponse<ActionResult<Employee>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                }
                _logger.LogInformation($"Get the values of CreateEmployeeAsync");
                _logger.LogInformation($"End CreateEmployeeAsync");
                //Salutions found

                return this.CreateResponse<ActionResult<Employee>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", newEmployeeCreated);


            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while creating new employee");
                return this.CreateResponse<ActionResult<Employee>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        [HttpPut("{id}")]
        public async Task<CommonResponse<Employee>> UpdateEmployee(long id, Employee employee)
        {
            _logger.LogInformation($"Start UpdateEmployee");
            try
            {
                if (id != employee.employeeId)
                {
                    _logger.LogInformation($"Start UpdateEmployee null");
                    //no salutions found
                    return this.CreateResponse<Employee>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                }
                await _employeeService.UpdateEmployeeAsync(employee);
                _logger.LogInformation($"Get the values of UpdateEmployeeAsync");
                _logger.LogInformation($"End UpdateEmployeeAsync");
                //Salutions found

                return this.CreateResponse<Employee>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success");
                //return NoContent();
                //ask though navitage to other page


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while updating employee");
                return this.CreateResponse<Employee>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(long id)
        {

            await _employeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }
        */

    }

    }


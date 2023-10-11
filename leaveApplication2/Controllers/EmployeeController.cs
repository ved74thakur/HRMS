﻿using leaveApplication2.Models;
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
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


namespace leaveApplication2.Controllers     
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IAppliedLeaveService _leaveService;
        private readonly IConfiguration _configuration;

        private readonly ILogger<EmployeeController> _logger;

        //private readonly IEmployeeLeaveService _employeeLeaveService;
        public EmployeeController(IEmployeeService employeeService, IAppliedLeaveService leaveService, ILogger<EmployeeController> logger, IConfiguration configuration)
        {

            _employeeService = employeeService;
            _leaveService = leaveService;
            _logger  = logger;
            //_employeeLeaveService = employeeLeaveService;

        }
        //Employees 
        //Getting all employees
        [HttpGet]
        public async Task<CommonResponse<IEnumerable<Employee>>> GetAllEmployees()
        {
            _logger.LogInformation($"Start GetAllEmployees");
            try
            {
                var employees = await _employeeService.GetAllEmployeesAsync();
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
        
        [HttpGet("GetSingeEmployee{id}")]
        public async Task<CommonResponse<Employee>> GetSingeEmployee(long id)
        {
            _logger.LogInformation($"Start GetEmployeeByIdAsync");
            try
            {
                var singleEmployee = await _employeeService.GetEmployeeByIdAsync(id);
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

        //create employee
        [HttpPost("CreateNewEmployee")]
        public async Task<CommonResponse<ActionResult<Employee>>> CreateNewEmployee(Employee request)
        {
            _logger.LogInformation($"Start CreateNewEmployee");

            try
            {
                var newEmployeeCreated = await _employeeService.CreateEmployeeAsync(request);
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

                return this.CreateResponse<ActionResult<Employee>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", newEmployeeCreated);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while creating new leave request");
                
                return this.CreateResponse<ActionResult<Employee>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /*
        //verifypassword
        [HttpPost("VerifyPasswordAsync")]
        public async Task<CommonResponse<bool>> VerifyPasswordAsync([FromBody] Employee request)
        {
            _logger.LogInformation($"Start VerifyPasswordAsync");
            try
            {
                var isPasswordValid = await _employeeService.VerifyPasswordAsync(request.employeeId, request.passwordHash);
                if (isPasswordValid == false)
                {
                    _logger.LogInformation($"Start GetAllEmployees null");
                    //no salutions found
                    return this.CreateResponse<bool>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                    //this.CreateResponse<Employee> (,)
                }
                _logger.LogInformation($"Get the values of GetAllEmployeeAsync");
                _logger.LogInformation($"End GetAllEmployeeAsync");
                //Salutions found

                return this.CreateResponse<bool>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success: User is verfied", isPasswordValid);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<bool>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        */

        [HttpPut("UpdateEmployeeByIdAsync/{id}")]
        public async Task<CommonResponse<ActionResult<Employee>>> UpdateEmployeeRegistrationById(long id, Employee request)
        {
            _logger.LogInformation($"Start UpdateEmployeeRegistrationById");


            //return Ok(result);
            try
            {
                var updateEmployeeRegistration = await _employeeService.UpdateEmployeeRegistrationById(id, request);
                if (updateEmployeeRegistration == null)
                {
                    _logger.LogInformation($"Start UpdateAppliedLeave null");
                    //no salutions found

                    return this.CreateResponse<ActionResult<Employee>>(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "No salutions found.");

                }
                _logger.LogInformation($"Get the values of GetEmployeeByIdAsync");
                _logger.LogInformation($"End GetEmployeeByIdAsync");
                //Salutions found
                return this.CreateResponse<ActionResult<Employee>>(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "Success", updateEmployeeRegistration);
                // return this.CreateResponse<IEnumerable<Employee>>(StatusCode.Status200K, "Success", employee);
            }
            catch (Exception ex)
            {
                //error occured
                _logger.LogError(ex, "An error occured while retrieving all salutions");
                return this.CreateResponse<ActionResult<Employee>>(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
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
        
     

        

        /*
        //controller for leaves
        [HttpGet("GetAllAppliedLeaves")]
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

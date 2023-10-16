﻿using leaveApplication2.Dtos;
using leaveApplication2.Models;

namespace leaveApplication2.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        
        Task<Employee> GetEmployeeByIdAsync(long id);
        Task<Employee> CreateEmployeeAsync(Employee employee);
        
        Task<Employee> UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(long id);

        Task<Employee> RegisterEmployeeAsync(Employee employee);
        Task<Employee> EmployeeLoginAsync(EmployeeLoginDto employee);

        Task<Employee> GetEmployeeByEmailAsync(string email);
        //Task<bool> VerifyPasswordAsync(long id, string password);

        //Task<bool> ActivateEmployeeAsync(ActivationRequest activationRequest);
        //Task<bool> SendActivationEmailAsync(long id);
    }
}

﻿using leaveApplication2.Models;

namespace leaveApplication2.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        
        Task<Employee> GetEmployeeByIdAsync(long id);
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task<Employee> UpdateEmployeeRegistrationById(long id, Employee request);
        Task DeleteEmployeeAsync(long id);

        
        //Task<bool> VerifyPasswordAsync(long id, string password);

        //Task<bool> ActivateEmployeeAsync(ActivationRequest activationRequest);
        //Task<bool> SendActivationEmailAsync(long id);
    }
}
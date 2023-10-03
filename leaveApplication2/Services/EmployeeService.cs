
using leaveApplication2.Data;
using leaveApplication2.Models;
using leaveApplication2.Repostories;
using Microsoft.EntityFrameworkCore;

namespace leaveApplication2.Services
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetAllEmployeesAsync();
        }
        

        public async Task<Employee> GetEmployeeByIdAsync(long id)
        {
            var singleEmployee =  await _employeeRepository.GetEmployeeByIdAsync(id);
            return singleEmployee;
        }

       
        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {

            var createdEmployee = await _employeeRepository.CreateEmployeeAsync(employee);
            return createdEmployee;
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            await _employeeRepository.UpdateEmployeeAsync(employee);
        }

        public async Task DeleteEmployeeAsync(long id)
        {
            await _employeeRepository.DeleteEmployeeAsync(id);
        }



    }
}


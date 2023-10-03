using leaveApplication2.Models;

namespace leaveApplication2.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        
        Task<Employee> GetEmployeeByIdAsync(long id);
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(long id);
    }
}

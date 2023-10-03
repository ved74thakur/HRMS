using leaveApplication2.Models;

namespace leaveApplication2.Repostories
{
    public interface IEmployeeRepository
    {
        Task<IReadOnlyCollection<Employee>> GetAllEmployeesAsync();
        
        Task<Employee> GetEmployeeByIdAsync(long id);
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(long id);
    }
}

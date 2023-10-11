using leaveApplication2.Models;

namespace leaveApplication2.Repostories
{
    public interface IEmployeeRepository
    {
        Task<IReadOnlyCollection<Employee>> GetAllEmployeesAsync();
        
        Task<Employee> GetEmployeeByIdAsync(long id);
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task<Employee> UpdateEmployeeRegistrationById(long id, Employee request);
        Task<Employee> GetEmployeeByEmailAsync(string email);
        
        Task DeleteEmployeeAsync(long id);
    }
}

using leaveApplication2.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace leaveApplication2.Repostories
{
    public interface IEmployeeRepository
    {
        Task<IReadOnlyCollection<Employee>> GetEmployeesAsync();
        
        Task<Employee> GetEmployeeByIdAsync(long id);
        Task<Employee> CreateEmployeeAsync(Employee employee);
        
        Task<Employee> UpdateEmployeeAsync(Employee employee);
        Task<Employee> GetEmployeeByEmailAsync(string email);
        Task<Employee> RegisterEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(long id);
        Task<string> VerifyEmployeeEmailAsync(string employeeEmail);
        Task<Employee> EmployeeLoginAsync(Employee employee);
        Task<EmployeeReporting> CreateEmployeeReportingAsync(long employeeId, long reportingPersonId);


        // Add a method for starting a transaction
        IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel);
    }
}

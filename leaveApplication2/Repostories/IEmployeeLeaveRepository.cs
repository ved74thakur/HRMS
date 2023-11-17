using leaveApplication2.Models;
using System.Linq.Expressions;

namespace leaveApplication2.Repostories
{
    public interface IEmployeeLeaveRepository
    {
        Task<IReadOnlyCollection<EmployeeLeave>> GetAllEmployeesLeaveAsync();
        Task<EmployeeLeave> CreateEmployeeLeaveAsync(EmployeeLeave leave);
        Task<EmployeeLeave> GetEmployeeLeaveByIdAsync(long id);
        Task<EmployeeLeave> UpdateEmployeeLeaveAsync(long id, EmployeeLeave employeeLeave);
        Task<EmployeeLeave> UpdateEmployeeLeaveAsync(EmployeeLeave employeeLeave);
        Task<List<EmployeeLeave>> GetEmployeeLeaveByEmployeeId(long employeeId);
        Task<EmployeeLeave> GetEmployeeLeaveByEmployee(long employeeId, int leaveTypeId);
    }
}

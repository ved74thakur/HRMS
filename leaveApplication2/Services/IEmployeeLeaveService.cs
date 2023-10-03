using leaveApplication2.Models;
using System.Threading.Tasks;

namespace leaveApplication2.Services
{
    public interface IEmployeeLeaveService
    {
        Task<IEnumerable<EmployeeLeave>> GetAllEmployeesLeaveAsync();
        Task<EmployeeLeave> CreateEmployeeLeaveAsync(EmployeeLeave leave);

        Task<EmployeeLeave> GetEmployeeLeaveByIdAsync(long id);
        Task<EmployeeLeave> UpdateEmployeeLeaveAsync(long id, EmployeeLeave employeeLeave);
        Task<IReadOnlyCollection<EmployeeLeave>> GetEmployeeLeaveByEmployeeId(long employeeId);
    }
}

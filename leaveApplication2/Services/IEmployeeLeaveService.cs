using leaveApplication2.Dtos;
using leaveApplication2.Models;
using System.Threading.Tasks;

namespace leaveApplication2.Services
{
    public interface IEmployeeLeaveService
    {
        Task<IEnumerable<EmployeeLeave>> GetAllEmployeesLeaveAsync();
        Task<EmployeeLeave> CreateEmployeeLeaveAsync(EmployeeLeave leave);
        Task<EmployeeLeave> GetEmpLeaveByEmpIdAsync(long id);
        Task<EmployeeLeave> GetEmployeeLeaveByIdAsync(long id);

        Task<EmployeeLeave> UpdateEmployeeLeaveAsync(EmployeeLeaveUpdate employeeLeaveUpdate);
        Task<EmployeeLeave> SetEmployeeLeaveToFalseAsync(long id);
        Task<IReadOnlyCollection<EmployeeLeave>> GetEmployeeLeaveByEmployeeId(long employeeId);
    }
}

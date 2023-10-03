using leaveApplication2.Models;

namespace leaveApplication2.Repostories
{
    public interface IEmployeeLeaveRepository
    {
        Task<IReadOnlyCollection<EmployeeLeave>> GetAllEmployeesLeaveAsync();
        Task<EmployeeLeave> CreateEmployeeLeaveAsync(EmployeeLeave leave);
        Task<EmployeeLeave> GetEmployeeLeaveByIdAsync(long id);

        Task<IReadOnlyCollection<EmployeeLeave>> GetEmployeeLeaveByEmployeeId(long employeeId);
    }
}

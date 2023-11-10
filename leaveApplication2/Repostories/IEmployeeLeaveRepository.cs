using leaveApplication2.Models;

namespace leaveApplication2.Repostories
{
    public interface IEmployeeLeaveRepository
    {
        Task<IReadOnlyCollection<EmployeeLeave>> GetAllEmployeesLeaveAsync();
        Task<EmployeeLeave> CreateEmployeeLeaveAsync(EmployeeLeave leave);
        Task<EmployeeLeave> GetEmployeeLeaveByIdAsync(long id);
        Task<EmployeeLeave> UpdateEmployeeLeaveAsync(long id, EmployeeLeave employeeLeave);
        Task<EmployeeLeave> SetEmployeeLeaveToFalseAsync(long id);
        Task<List<EmployeeLeave>> GetEmployeeLeaveByEmployeeId(long employeeId);
        Task<EmployeeLeave> GetEmployeeLeaveByEmployee(long employeeId, int leaveTypeId);
    }
}

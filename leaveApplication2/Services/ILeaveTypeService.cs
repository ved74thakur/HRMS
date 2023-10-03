using leaveApplication2.Models;

namespace leaveApplication2.Services
{
    public interface ILeaveTypeService
    {
        Task<IReadOnlyCollection<LeaveType>> GetAllLeaveTypesAsync();
        Task<LeaveType> GetLeaveTypeByIdAsync(int id);
        Task CreateLeaveTypeAsync(LeaveType leaveType);
        Task UpdateLeaveTypeAsync(LeaveType leaveType);
        Task DeleteLeaveTypeAsync(int id);
    }
}

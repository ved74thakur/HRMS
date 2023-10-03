using leaveApplication2.Models;

namespace leaveApplication2.Repostories
{
    public interface ILeaveTypeRepository
    {
        Task<IReadOnlyCollection<LeaveType>> GetAllLeaveTypesAsync();
        Task<LeaveType> GetLeaveTypeByIdAsync(int id);
        Task CreateLeaveTypeAsync(LeaveType leaveType);
        Task UpdateLeaveTypeAsync(LeaveType leaveType);
        Task DeleteLeaveTypeAsync(int id);
    }
}

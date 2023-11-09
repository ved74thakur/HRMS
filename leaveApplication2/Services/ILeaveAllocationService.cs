using leaveApplication2.Models;

namespace leaveApplication2.Services
{
    public interface ILeaveAllocationService
    {
        Task<IReadOnlyCollection<LeaveAllocation>> GetLeaveAllocationsAsync();
        Task<IReadOnlyCollection<LeaveAllocation>> GetLeaveAllocationsAsync(Func<LeaveAllocation, bool> filter);
        Task<LeaveAllocation> GetLeaveAllocationAsync(long id);
        Task<LeaveAllocation> CreateLeaveAllocationAsync(LeaveAllocation leaveAllocation);
        Task<LeaveAllocation> DeleteLeaveAllocationAsync(int leaveAlloctionId);
        Task<LeaveAllocation> UpdateLeaveAllocationAsync(int leaveAlloctionId);
    }
}

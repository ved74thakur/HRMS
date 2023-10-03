using leaveApplication2.Models;

namespace leaveApplication2.Services
{
    public interface IAppliedLeaveService
    {
        Task<IEnumerable<AppliedLeave>> GetAppliedLeavesAsync();
        Task<AppliedLeave> CreateAppliedLeave(AppliedLeave leave);
        Task<AppliedLeave> GetAppliedLeaveByIdAsync(long id);
        Task<AppliedLeave> UpdateAppliedLeaveAsync(long id, AppliedLeave leave);
        Task DeleteAppliedLeaveByIdAsync(long id);
    }
}

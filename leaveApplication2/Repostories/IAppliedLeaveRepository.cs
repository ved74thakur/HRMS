using leaveApplication2.Models;

namespace leaveApplication2.Repostories
{
    public interface IAppliedLeaveRepository
    {
        Task<IReadOnlyCollection<AppliedLeave>> GetAppliedLeavesAsync();
        Task<AppliedLeave> CreateAppliedLeave(AppliedLeave leave);
        Task<AppliedLeave> GetAppliedLeaveByIdAsync(long id);
        Task<AppliedLeave> UpdateAppliedLeaveAsync(long id, AppliedLeave leave);
        Task<AppliedLeave> DeleteAppliedLeaveByIdAsync(long id);


    }
}

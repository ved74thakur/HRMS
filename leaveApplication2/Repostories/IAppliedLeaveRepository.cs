using leaveApplication2.Models;
using System.Linq.Expressions;

namespace leaveApplication2.Repostories
{
    public interface IAppliedLeaveRepository
    {
        Task<IReadOnlyCollection<AppliedLeave>> GetAppliedLeavesAsync();
        Task<IReadOnlyCollection<AppliedLeave>> GetAppliedLeavesAsync(Expression<Func<AppliedLeave, bool>> filter);

        Task<AppliedLeave> CreateAppliedLeave(AppliedLeave leave);
        Task<AppliedLeave> GetAppliedLeaveByIdAsync(long id);
        Task<AppliedLeave> UpdateAppliedLeaveAsync(long id, AppliedLeave leave);
        Task<AppliedLeave> UpdateAppliedLeaveAsync(AppliedLeave leave);
        Task<AppliedLeave> DeleteAppliedLeaveByIdAsync(long id);
        Task<AppliedLeave> CancelAppliedLeaveByIdAsync(long id);

        Task<IReadOnlyCollection<AppliedLeave>> GetFilteredLeavesAsync(
           Expression<Func<AppliedLeave, bool>> filter = null,
           Expression<Func<AppliedLeave, object>> orderBy = null);


        Task<IReadOnlyCollection<AppliedLeave>> GetUnApprovedAppliedLeavesAsync(AppliedLeave appliedLeave);

    }
}

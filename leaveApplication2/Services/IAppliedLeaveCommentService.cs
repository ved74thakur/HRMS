using leaveApplication2.Models;

namespace leaveApplication2.Services
{
    public interface IAppliedLeaveCommentService
    {
        Task<IReadOnlyCollection<AppliedLeaveComment>> GetAppliedLeavesCommentAsync();
        Task<AppliedLeaveComment> CreateAppliedLeaveComment(AppliedLeaveComment comment);
        Task<IEnumerable<AppliedLeaveComment>> GetAppliedLeavesCommentAsync(long appliedLeaveTypeId, int LeaveStatusId);
    }
}

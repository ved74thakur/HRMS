using leaveApplication2.Models;

namespace leaveApplication2.Repostories
{
    public interface IAppliedLeaveCommentRepository
    {
        Task<IReadOnlyCollection<AppliedLeaveComment>> GetAppliedLeavesCommentAsync();
        Task<AppliedLeaveComment> CreateAppliedLeaveComment(AppliedLeaveComment comment);
    }
}

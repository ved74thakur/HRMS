using leaveApplication2.Models;
using System.Linq.Expressions;

namespace leaveApplication2.Repostories
{
    public interface IAppliedLeaveCommentRepository
    {
        Task<IReadOnlyCollection<AppliedLeaveComment>> GetAppliedLeavesCommentAsync();
        Task<AppliedLeaveComment> CreateAppliedLeaveComment(AppliedLeaveComment comment);
        Task<IEnumerable<AppliedLeaveComment>> GetAppliedLeavesCommentAsync(Expression<Func<AppliedLeaveComment, bool>> filter);
    }
}

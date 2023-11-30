using leaveApplication2.Models;
using leaveApplication2.Repostories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace leaveApplication2.Services
{
    public class AppliedLeaveCommentService : IAppliedLeaveCommentService
    {
        private readonly IAppliedLeaveCommentRepository _appliedLeaveCommentRepository;

        public AppliedLeaveCommentService(IAppliedLeaveCommentRepository appliedLeaveCommentRepository)
        {
            _appliedLeaveCommentRepository = appliedLeaveCommentRepository;
        }
        public async Task<IReadOnlyCollection<AppliedLeaveComment>> GetAppliedLeavesCommentAsync()
        {
            return await _appliedLeaveCommentRepository.GetAppliedLeavesCommentAsync();

        }

        public async Task<AppliedLeaveComment> CreateAppliedLeaveComment(AppliedLeaveComment comment)
        {
            try
            {
                var appliedLeaveComment = await _appliedLeaveCommentRepository.CreateAppliedLeaveComment(comment);
                return appliedLeaveComment;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<IEnumerable<AppliedLeaveComment>> GetAppliedLeavesCommentAsync(long appliedLeaveTypeId, int LeaveStatusId)
        {
            Expression<Func<AppliedLeaveComment, bool>> filter;
            filter = la => la.appliedLeaveTypeId == appliedLeaveTypeId && la.LeaveStatusId == LeaveStatusId;
            var appliedLeavesComment = await _appliedLeaveCommentRepository.GetAppliedLeavesCommentAsync(filter);

            return appliedLeavesComment;

        }
       

    }
}

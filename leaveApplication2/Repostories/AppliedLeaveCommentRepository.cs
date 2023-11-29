using leaveApplication2.Data;
using leaveApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace leaveApplication2.Repostories
{
    public class AppliedLeaveCommentRepository : IAppliedLeaveCommentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppliedLeaveCommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<AppliedLeaveComment>> GetAppliedLeavesCommentAsync()
        {
            return await _context.AppliedLeaveComments.ToListAsync();

        }

        public async Task<AppliedLeaveComment> CreateAppliedLeaveComment(AppliedLeaveComment comment)
        {
            try
            {
                _context.AppliedLeaveComments.Add(comment);
                await _context.SaveChangesAsync();
                return comment;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}

using leaveApplication2.Data;
using leaveApplication2.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

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

        public async Task<IEnumerable<AppliedLeaveComment>> GetAppliedLeavesCommentAsync(Expression<Func<AppliedLeaveComment, bool>> filter)
        {
            return await _context.AppliedLeaveComments.Where(filter).ToListAsync();
        }


        


    }
}

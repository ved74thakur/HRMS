using leaveApplication2.Models;
using System.Linq.Expressions;

namespace leaveApplication2.Repostories
{
    public interface ILeaveStatusRepository
    {
        public Task<IReadOnlyCollection<LeaveStatus>> GetLeaveStatusesAsync();
        public Task<IReadOnlyCollection<LeaveStatus>> GetLeaveStatusesAsync(Expression<Func<LeaveStatus, bool>> filter);
        public Task<LeaveStatus> GetLeaveStatusAsync(Expression<Func<LeaveStatus, bool>> filter);

    }
}

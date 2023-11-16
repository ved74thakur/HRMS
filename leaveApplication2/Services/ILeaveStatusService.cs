

using leaveApplication2.Models;
using System.Linq.Expressions;

namespace leaveApplication2.Services
{
    public interface ILeaveStatusService
    {
        Task<IReadOnlyCollection<LeaveStatus>> GetLeaveStatusesAsync();
        Task<LeaveStatus> GetLeaveStatusByCodeAsync(string statusCode);
        //public Task<IReadOnlyCollection<LeaveStatus>> GetLeaveStatusesAsync(Expression<Func<LeaveStatus, bool>> filter);
        // public Task<LeaveStatus> GetLeaveStatusAsync(Expression<Func<LeaveStatus, bool>> filter);
    }
}

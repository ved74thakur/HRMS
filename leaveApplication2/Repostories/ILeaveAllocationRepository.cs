using leaveApplication2.Models;
using System.Linq.Expressions;

namespace leaveApplication2.Repostories
{
    public interface ILeaveAllocationRepository
    {
        Task<IReadOnlyCollection<LeaveAllocation>> GetLeaveAllocationsAsync();
        Task<IReadOnlyCollection<LeaveAllocation>> GetLeaveAllocationsAsync(Expression<Func<LeaveAllocation, bool>> filter);
        Task<LeaveAllocation> GetLeaveAllocationAsync(long id);
        Task<LeaveAllocation> GetLeaveAllocationAsync(Expression<Func<LeaveAllocation, bool>> filter);

        Task<LeaveAllocation> CreateLeaveAllocationAsync(LeaveAllocation leaveAllocation);

        Task<LeaveAllocation> DeleteLeaveAllocationAsync(int leaveAlloctionId);

        Task<LeaveAllocation> UpdateLeaveAllocationAsync(int leaveAlloctionId);

        Task RemoveLeaveAllocationsForFinancialYearAsync(int financialYearId);
    }
}

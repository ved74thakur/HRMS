using leaveApplication2.Dtos;
using leaveApplication2.Models;
using System.Linq.Expressions;

namespace leaveApplication2.Services
{
    public interface IAppliedLeaveService
    {
        Task<IEnumerable<AppliedLeave>> GetAppliedLeavesAsync();
        //Task<IEnumerable<AppliedLeave>> GetAppliedLeavesAsync(Expression<Func<AppliedLeave, bool>> filter);
        Task<IEnumerable<AppliedLeaveDTO>> GetAppliedLeavesAsync(Expression<Func<AppliedLeave, bool>> filter);
        Task<AppliedLeave> CreateAppliedLeave(AppliedLeave leave);
        Task<AppliedLeave> GetAppliedLeaveByIdAsync(long id);
        Task<AppliedLeave> UpdateAppliedLeaveAsync(long id, AppliedLeave leave);
        //Task<AppliedLeave> UpdateLeaveStatusAsync(long appliedLeaveTypeId, int leaveStatusId);
        Task DeleteAppliedLeaveByIdAsync(long id);
        Task<AppliedLeave> CancelAppliedLeaveByIdAsync(long id);

        Task<AppliedLeave> UpdateIsRejectedAsync(long id, bool isRejected);
        Task<AppliedLeave> UpdateIsApprovedAsync(long id, bool isApproved);
        Task<AppliedLeave> UpdateIsApprovedCancelAsync(long id, bool isApproved);

        Task<IReadOnlyCollection<AppliedLeave>> GetPreviousAppliedLeavesAsync2(

       long employeeId = 0,
       int leaveTypeId = 0, bool isApproved = false, bool isHalfDay = false,
       Expression<Func<AppliedLeave, object>> orderBy = null);


        Task<IReadOnlyCollection<AppliedLeave>> GetUnApprovedAppliedLeavesAsync(AppliedLeave appliedLeave);


    }
}

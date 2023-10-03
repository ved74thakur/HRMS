using leaveApplication2.Models;

namespace leaveApplication2.Repostories
{
    public interface ILeaveStatusRepository
    {
        Task<IReadOnlyCollection<LeaveStatus>> GetLeaveStatusesAsync();
        Task<LeaveStatus> GetLeaveStatusByIdAsync(int leaveStatusId);

        Task<LeaveStatus> CreateLeaveStatusAsync(LeaveStatus leaveStatus);


    }
}

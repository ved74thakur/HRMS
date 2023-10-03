using leaveApplication2.Models;
using leaveApplication2.Repostories;

namespace leaveApplication2.Services
{
    public class LeaveStatusService : ILeaveStatusService
    {
        private readonly ILeaveStatusRepository _leaveStatusRepository;

        public LeaveStatusService(ILeaveStatusRepository leaveStatusRepository)
        {
            _leaveStatusRepository = leaveStatusRepository;
        }

        public async Task<IEnumerable<LeaveStatus>> GetLeaveStatusesAsync()
        {
            return await _leaveStatusRepository.GetLeaveStatusesAsync();
            
        }

       



        public async Task<LeaveStatus> GetLeaveStatusByIdAsync(int leaveStatusId)
        {
            return await _leaveStatusRepository.GetLeaveStatusByIdAsync(leaveStatusId);
        }

        public async Task<LeaveStatus> CreateLeaveStatusAsync(LeaveStatus leaveStatus)
        {
            var createdLeaveStatus = await _leaveStatusRepository.CreateLeaveStatusAsync(leaveStatus);
            return createdLeaveStatus;
        }
    }
}

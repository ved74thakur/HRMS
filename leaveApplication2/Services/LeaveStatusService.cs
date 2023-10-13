
using leaveApplication2.Models;
using leaveApplication2.Repostories;
using Microsoft.EntityFrameworkCore;

namespace leaveApplication2.Services
{
    public class LeaveStatusService : ILeaveStatusService
    {
        private readonly ILeaveStatusRepository _leaveStatusRepository;

        public LeaveStatusService(ILeaveStatusRepository leaveStatusRepository)
        {
            _leaveStatusRepository = leaveStatusRepository;
        }

        public async Task<IEnumerable<Models.LeaveStatus>> GetLeaveStatusesAsync()
        {
            return await _leaveStatusRepository.GetLeaveStatusesAsync();
            
        }

        public async Task<Models.LeaveStatus> GetLeaveStatusByCodeAsync(string leaveStatusNameCode)
        {
            var leaveStatusByCode = await _leaveStatusRepository.GetLeaveStatusByCodeAsync(leaveStatusNameCode);
            return leaveStatusByCode;
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
        public async Task<LeaveStatus> UpdateLeaveStatusAsync(LeaveStatus leaveStatus)
        {
            var leaveStatusUpdated = await _leaveStatusRepository.UpdateLeaveStatusAsync(leaveStatus);
            return leaveStatusUpdated;
        }




    }
}

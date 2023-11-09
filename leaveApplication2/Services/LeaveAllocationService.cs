using leaveApplication2.Models;
using leaveApplication2.Repostories;

namespace leaveApplication2.Services
{
    public class LeaveAllocationService : ILeaveAllocationService
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        public LeaveAllocationService(ILeaveAllocationRepository leaveAllocationRepository)
        {
            _leaveAllocationRepository = leaveAllocationRepository; 
        }

        public async Task<IReadOnlyCollection<LeaveAllocation>> GetLeaveAllocationsAsync()
        {
            return await _leaveAllocationRepository.GetLeaveAllocationsAsync();
        }

        Task<LeaveAllocation> ILeaveAllocationService.GetLeaveAllocationAsync(long id)
        {
            throw new NotImplementedException();
        }

      

        Task<IReadOnlyCollection<LeaveAllocation>> ILeaveAllocationService.GetLeaveAllocationsAsync(Func<LeaveAllocation, bool> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<LeaveAllocation> CreateLeaveAllocationAsync(LeaveAllocation leaveAllocation)
        {
            return await _leaveAllocationRepository.CreateLeaveAllocationAsync(leaveAllocation);
        }

        public async Task<LeaveAllocation> DeleteLeaveAllocationAsync(int leaveAlloctionId)
        {
            return await _leaveAllocationRepository.DeleteLeaveAllocationAsync(leaveAlloctionId);
        }

        public async Task<LeaveAllocation> UpdateLeaveAllocationAsync(int leaveAlloctionId)
        {
            return await _leaveAllocationRepository.UpdateLeaveAllocationAsync(leaveAlloctionId);
        }


    }
}

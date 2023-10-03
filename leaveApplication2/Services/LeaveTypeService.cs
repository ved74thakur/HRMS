using leaveApplication2.Models;
using leaveApplication2.Repostories;

namespace leaveApplication2.Services
{
    public class LeaveTypeService: ILeaveTypeService
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public LeaveTypeService(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<IReadOnlyCollection<LeaveType>> GetAllLeaveTypesAsync()
        {
            return await _leaveTypeRepository.GetAllLeaveTypesAsync();
        }

        public async Task<LeaveType> GetLeaveTypeByIdAsync(int id)
        {
            return await _leaveTypeRepository.GetLeaveTypeByIdAsync(id);
        }

        public async Task CreateLeaveTypeAsync(LeaveType leaveType)
        {
            await _leaveTypeRepository.CreateLeaveTypeAsync(leaveType);
        }

        public async Task UpdateLeaveTypeAsync(LeaveType leaveType)
        {
            await _leaveTypeRepository.UpdateLeaveTypeAsync(leaveType);
        }

        public async Task DeleteLeaveTypeAsync(int id)
        {
            await _leaveTypeRepository.DeleteLeaveTypeAsync(id);
        }
    }
}

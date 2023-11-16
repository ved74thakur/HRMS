using leaveApplication2.Models;
using leaveApplication2.Repostories;
using System.Linq.Expressions;

namespace leaveApplication2.Services
{
    public class LeaveStatusService : ILeaveStatusService
    {
        private readonly ILeaveStatusRepository _leaveStatusRepository = null;
        public LeaveStatusService(ILeaveStatusRepository leaveStatusRepository)
        {
            _leaveStatusRepository = leaveStatusRepository;
        }

        public async Task<LeaveStatus> GetLeaveStatusByCodeAsync(string statusCode)
        {

            try
            {
                Expression<Func<LeaveStatus, bool>> filter = urm => urm.LeaveStatusCode == statusCode;
                var leaveStatus = await _leaveStatusRepository.GetLeaveStatusAsync(filter);

                return leaveStatus;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IReadOnlyCollection<LeaveStatus>> GetLeaveStatusesAsync()
        {
          return await   _leaveStatusRepository.GetLeaveStatusesAsync();
        }
    }
}

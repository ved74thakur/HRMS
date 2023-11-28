using leaveApplication2.Models;
using leaveApplication2.Repostories;

namespace leaveApplication2.Services
{
    public class LeaveReportService : ILeaveReportService
    {
        private readonly IAppliedLeaveRepository _leaveRepository;
        public LeaveReportService(IAppliedLeaveRepository leaveRepository)
        {
            _leaveRepository = leaveRepository;
        }



    }


}

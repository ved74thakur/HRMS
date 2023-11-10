using leaveApplication2.Models;

namespace leaveApplication2.Services
{
    public interface IFinancialYearSetupService
    {
        Task<string> CreateUpdatedEmployeeLeaveAsync();
    }
}

using leaveApplication2.Dtos;
using leaveApplication2.Models;
using Microsoft.AspNetCore.Mvc;

namespace leaveApplication2.Services
{
    public interface ILeaveReportService
    {
        Task<IEnumerable<AppliedLeave>> GetLeavesReportAsync([FromBody] LeaveReport leaveReport);
    }
}

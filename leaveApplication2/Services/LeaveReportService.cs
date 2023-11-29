using leaveApplication2.Dtos;
using leaveApplication2.Models;
using leaveApplication2.Repostories;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace leaveApplication2.Services
{
    public class LeaveReportService : ILeaveReportService
    {
        private readonly IAppliedLeaveRepository _leaveRepository;

        public LeaveReportService(IAppliedLeaveRepository leaveRepository)
        {
            _leaveRepository = leaveRepository;
        }



        public async Task<IEnumerable<AppliedLeave>> GetLeavesReportAsync([FromBody] LeaveReport leaveReport)
        {
            if (leaveReport.startDate > leaveReport.endDate)
            {
                throw new InvalidOperationException("Start date cannot be greater than end date.");
            }
            Expression<Func<AppliedLeave, bool>> filter;
            filter = la =>
    (leaveReport.employeeId == 0 || la.employeeId == leaveReport.employeeId) &&
    (leaveReport.LeaveStatusId == 0 || la.LeaveStatusId == leaveReport.LeaveStatusId) &&
    la.StartDate >= leaveReport.startDate &&
    la.EndDate <= leaveReport.endDate;

            //Expression<Func<AppliedLeave, bool>> filter;
            //filter = la => la.StartDate >= leaveReport.startDate
            //&& la.EndDate <= leaveReport.endDate;

            //if (leaveReport.employeeId != 0)
            //{
            //    filter = la => la.employeeId == leaveReport.employeeId;
            //}
            //else if (leaveReport.LeaveStatusId != 0)
            //{
            //    filter = la => la.LeaveStatusId == leaveReport.LeaveStatusId;
            //}

            var leavesReport = await _leaveRepository.GetAppliedLeavesAsync(filter);
            var orderedLeavesReport = leavesReport.OrderByDescending(la => la.StartDate).ThenBy(la => la.Employee.firstName, StringComparer.OrdinalIgnoreCase);

            return orderedLeavesReport;
        }


    }


}

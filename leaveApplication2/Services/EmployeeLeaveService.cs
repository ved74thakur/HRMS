using leaveApplication2.Models;
using leaveApplication2.Repostories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace leaveApplication2.Services
{
    public class EmployeeLeaveService : IEmployeeLeaveService
    {

        private readonly IEmployeeLeaveRepository _employeeLeaveRepository;
        private readonly IAppliedLeaveRepository _leaveRepository;
        //private readonly ILeaveStatusRepository _leaveStatusRepository;
        private readonly IAppliedLeaveService _leaveService;
        private readonly ILeaveStatusService _leaveStatusService;
        public EmployeeLeaveService(IEmployeeLeaveRepository employeeLeaveRepository, IAppliedLeaveService leaveService, IAppliedLeaveRepository leaveRepository, ILeaveStatusService leaveStatusService)
        {
            _employeeLeaveRepository = employeeLeaveRepository;
            _leaveService = leaveService;
            _leaveRepository = leaveRepository;
            _leaveStatusService = leaveStatusService;
        }



        public async Task<IEnumerable<EmployeeLeave>> GetAllEmployeesLeaveAsync()
        {
            return await _employeeLeaveRepository.GetAllEmployeesLeaveAsync();
        }

        public async Task<EmployeeLeave> CreateEmployeeLeaveAsync(EmployeeLeave leave)
        {

            var createdLeave = await _employeeLeaveRepository.CreateEmployeeLeaveAsync(leave);
            return createdLeave;
        }

        public async Task<EmployeeLeave> GetEmployeeLeaveByIdAsync(long id)
        {
            var singleEmployeeLeave = await _employeeLeaveRepository.GetEmployeeLeaveByIdAsync(id);
            return singleEmployeeLeave;
        }

        public async Task<IReadOnlyCollection<EmployeeLeave>> GetEmployeeLeaveByEmployeeId(long employeeId)
        {

            /*
               Expression<Func<EmployeeLeave, bool>> filter = x =>
                  x.employeeId == existingLeave.employeeId &&
                  x.leaveTypeId == existingLeave.leaveTypeId &&
                  x.leaveAllocationId == appliedLeaveUpdateStatus.leaveAllocationId;

            Task<IReadOnlyCollection<AppliedLeave>> GetAppliedLeavesAsync(Expression<Func<AppliedLeave, bool>> filter);

              var leaveStatus = await _leaveStatusService.GetLeaveStatusByCodeAsync("APP");


             */


            //var previousAppliedLeaves = await _leaveRepository.GetUnApprovedAppliedLeavesAsync(new AppliedLeave() { IsApproved = false, employeeId = employeeId, IsRejected  =false });

            var leaveStatus = await _leaveStatusService.GetLeaveStatusByCodeAsync("APP");

            Expression<Func<AppliedLeave, bool>> filter = x =>
                 x.LeaveStatusId == leaveStatus.LeaveStatusId;

            var previousAppliedLeaves = await _leaveRepository.GetAppliedLeavesAsync(filter);


           List<EmployeeLeave> currentLeave  =  await _employeeLeaveRepository.GetEmployeeLeaveByEmployeeId(employeeId);
          
            foreach (var appliedLeave in previousAppliedLeaves)
            {
                // Find the corresponding leave type in currentLeave based on leave type ID
                var matchingLeaveType = currentLeave.FirstOrDefault(lt => lt.leaveTypeId == appliedLeave.leaveTypeId);
                if (matchingLeaveType != null)
                {
                  

                    matchingLeaveType.consumedLeaves += appliedLeave.applyLeaveDay;
                    matchingLeaveType.balanceLeaves -= appliedLeave.applyLeaveDay;

                }

            }


            return currentLeave;
        }

        public async Task<EmployeeLeave> UpdateEmployeeLeaveAsync(long id, EmployeeLeave employeeLeave)
        {

            var updateEmployeeLeave = await _employeeLeaveRepository.UpdateEmployeeLeaveAsync(id, employeeLeave);
            return updateEmployeeLeave;

        }
        //UPDATE EMPLOYEE LEAVE BASED ON STATUSCODE FOR APPROVE

        //public async Task<EmployeeLeave> UpdateEmployeeLeaveIfAPV(long id, string leaveStatusNameCode, EmployeeLeave employeeLeave)
        //{
        //    var leaveStatusCode = await _leaveStatusRepository.GetLeaveStatusByCodeAsync(leaveStatusNameCode);
        //    if (leaveStatusCode == null)
        //    {
        //        return null;
        //    }

        //    else if (leaveStatusCode.leaveStatusNameCode == "APV")
        //    {
        //        return await UpdateEmployeeLeaveAsync(id, employeeLeave);
        //    }

        //    return null;
        //}
        


    }



}

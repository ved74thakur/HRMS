using leaveApplication2.Models;
using leaveApplication2.Repostories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace leaveApplication2.Services
{
    public class EmployeeLeaveService : IEmployeeLeaveService
    {

        private readonly IEmployeeLeaveRepository _employeeLeaveRepository;
        private readonly IAppliedLeaveRepository _leaveRepository;
        //private readonly ILeaveStatusRepository _leaveStatusRepository;
        private readonly IAppliedLeaveService _leaveService;
        public EmployeeLeaveService(IEmployeeLeaveRepository employeeLeaveRepository, IAppliedLeaveService leaveService, IAppliedLeaveRepository leaveRepository)
        {
            _employeeLeaveRepository = employeeLeaveRepository;
            _leaveService = leaveService;
            _leaveRepository = leaveRepository;
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
           

            var previousAppliedLeaves = await _leaveRepository.GetUnApprovedAppliedLeavesAsync(new AppliedLeave() { IsApproved = false, employeeId = employeeId });

            // return await _employeeLeaveRepository.GetEmployeeLeaveByEmployeeId(employeeId);
           List<EmployeeLeave> currentLeave  =  await _employeeLeaveRepository.GetEmployeeLeaveByEmployeeId(employeeId);


            foreach (var appliedLeave in previousAppliedLeaves)
            {
                // Find the corresponding leave type in currentLeave based on leave type ID
                var matchingLeaveType = currentLeave.FirstOrDefault(lt => lt.leaveTypeId == appliedLeave.leaveTypeId);
                if (matchingLeaveType != null)
                {
                    // Subtract the leaveCount, balance leave, and apply leave
                    // matchingLeaveType.LeaveCount -= appliedLeave.LeaveCount;
                    //  matchingLeaveType.balanceLeaves -= appliedLeave.remaingLeave; // You may want to use a different field if 'balance leave' is different from this
                    // matchingLeaveType.consumedLeaves -= appliedLeave.remaingLeave; // You may want to use a different field if 'apply leave' is different from this

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

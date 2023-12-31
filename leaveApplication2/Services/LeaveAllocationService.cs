﻿using leaveApplication2.Models;
using leaveApplication2.Models.leaveApplication2.Models;
using leaveApplication2.Repostories;
using System.Linq.Expressions;

namespace leaveApplication2.Services
{
    public class LeaveAllocationService : ILeaveAllocationService
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IFinancialYearRepository _financialYearRepository;
        private readonly ILeaveTypeService _leaveTypeService;
        public LeaveAllocationService(ILeaveAllocationRepository leaveAllocationRepository, IFinancialYearRepository financialYearRepository, ILeaveTypeService leaveTypeService)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _financialYearRepository = financialYearRepository;
            _leaveTypeService = leaveTypeService;
        }

        public async Task<IReadOnlyCollection<LeaveAllocation>> GetLeaveAllocationsAsync()
        {
            return await _leaveAllocationRepository.GetLeaveAllocationsAsync();
        }

       //public async Task<LeaveAllocation> GetLeaveAllocationAsync(Expression<Func<LeaveAllocation, bool>> filter)
       // {

       //     return await _leaveAllocationRepository.GetLeaveAllocationAsync(filter);


       //     throw new NotImplementedException();

       // }

        public async Task<LeaveAllocation> GetLeaveAllocationAsync(Expression<Func<LeaveAllocation, bool>> filter) 
        {
            return await _leaveAllocationRepository.GetLeaveAllocationAsync(filter);
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


        public async Task<IReadOnlyCollection<LeaveAllocation>> CreateLeaveAllocationForAllLeaveTypes(FinancialYear financialYear, Dictionary<int, int> leaveTypeCounts)
        {
            //created new financialYear
            var newFinancialYearCreated = await _financialYearRepository.CreateFinancialYearAsync(financialYear);
            //setting isActiveYear to false
            var financialYears = await _financialYearRepository.GetFinancialYearsAsync();
            foreach (var fy in financialYears)
            {
                if(fy.financialYearId != newFinancialYearCreated.financialYearId)
                {
                    await _financialYearRepository.UpdateFinancialYearAsync(fy.financialYearId);
                }
                
            }
            //var inactiveFinancialYearIds = financialYears.Where(fy => !fy.ActiveYear).Select(fy => fy.financialYearId);
            //var removeLeaveAllocationsTasks = inactiveFinancialYearIds.Select(id => _leaveAllocationRepository.RemoveLeaveAllocationsForFinancialYearAsync(id));
            //await Task.WhenAll(removeLeaveAllocationsTasks);
            
            //got all the leave types available
            var leaveTypes = await _leaveTypeService.GetAllLeaveTypesAsync();
            foreach (var leaveType in leaveTypes)
            {
                if (leaveTypeCounts.TryGetValue(leaveType.leaveTypeId, out int userLeaveCount))
                {
                    var leaveAllocation = new LeaveAllocation
                    {
                        financialYearId = newFinancialYearCreated.financialYearId,
                        leaveTypeId = leaveType.leaveTypeId,
                        leaveCount = userLeaveCount
                    };

                    // Create leave allocation
                    await _leaveAllocationRepository.CreateLeaveAllocationAsync(leaveAllocation);

                }
                else
                {
                    throw new ArgumentException($"Leave count not provided for Leave Type ID {leaveType.leaveTypeId}");
                }
            }
            var leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationsAsync();
            return leaveAllocations;

        }

        public async Task<LeaveAllocation> GetLeaveAllocationAsync(long id)
        {
            var leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationAsync(id);

            return leaveAllocations;
        }
    }

}

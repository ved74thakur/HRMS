using leaveApplication2.Models;
using leaveApplication2.Models.leaveApplication2.Models;
using leaveApplication2.Repostories;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Linq.Expressions;

namespace leaveApplication2.Services
{
    public class FinancialYearSetupService : IFinancialYearSetupService
    {
        //Hear you have to call that 
        private readonly ILeaveAllocationService _leaveAllocationService;
        private readonly IEmployeeLeaveService _employeeLeaveService;
        private readonly IEmployeeService _employeeService;
        private readonly IFinancialYearService _financialYearService;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        
        

        
        public FinancialYearSetupService(ILeaveAllocationService leaveAllocationService, IEmployeeLeaveService employeeLeaveService, IEmployeeService employeeService, IFinancialYearService financialYearService, ILeaveAllocationRepository leaveAllocationRepository)
        {

            _leaveAllocationService = leaveAllocationService;
            _employeeLeaveService = employeeLeaveService;
            _employeeService = employeeService;
            _financialYearService = financialYearService;
            _leaveAllocationRepository = leaveAllocationRepository;
            

        }

        public async Task<string> CreateUpdatedEmployeeLeaveAsync()
        {
            var employeeLeaves = await _employeeLeaveService.GetAllEmployeesLeaveAsync();
            
            foreach( var empLv in employeeLeaves)
            {
                await _employeeLeaveService.SetEmployeeLeaveToFalseAsync(empLv.employeeLeaveId);
            }

            Expression<Func<FinancialYear, bool>> financialYearFilter = la => la.ActiveYear == true;
            var activeFinancialYear = await _financialYearService.GetActiveFinancialYearsAsync(financialYearFilter);
            var financialYearId = activeFinancialYear.First().financialYearId;

            Expression<Func<LeaveAllocation, bool>> filter = la => la.financialYearId == financialYearId;
            IReadOnlyCollection<LeaveAllocation> leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationsAsync(filter);
            //do not loop through all leave allocations
            //IReadOnlyCollection<LeaveAllocation> leaveAllocations = await _leaveAllocationService.GetLeaveAllocationsAsync();
            var employees = await _employeeService.GetEmployeesAsync();
            foreach (var leaveAllocation in leaveAllocations)
            {
                
                foreach (var employee in employees)
                {
                    var leave = new EmployeeLeave
                    {
                        employeeId = employee.employeeId, // Set the appropriate employeeId
                        leaveTypeId = leaveAllocation.leaveTypeId,
                        leaveCount = leaveAllocation.leaveCount,
                        consumedLeaves = 0, // Initialize consumedLeaves as required
                        balanceLeaves = leaveAllocation.leaveCount, // Initialize balanceLeaves as the leaveCount from LeaveAllocation
                        isActive = true, // Set isActive as required
                        leaveAllocationId = leaveAllocation.leaveAllocationId
                    };

                    // Insert the created EmployeeLeave object into your repository
                    var createdLeave = await _employeeLeaveService.CreateEmployeeLeaveAsync(leave);

                }

            }

            return "Success";
           

        }
        
    }
}
